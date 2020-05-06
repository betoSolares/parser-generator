using Helpers;
using RegularExpression;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace parser_generator.UI
{
    public partial class MainView : Form
    {
        /// <summary>Parts of the regular expression</summary>
        private const string SETS = @"((S·E·T·S)·((\t|\s)*·(\n))+·((\t|\s)*·[A-Z]+·(\t|\s)*·=·(\t|\s)*·((('·.·')|(C·H·R·\(·[0-9]+·\)))·((\.·\.)·(('·.·')|(C·H·R·\(·[0-9]+·\))))?·(\+·(('·.·')|(C·H·R·\(·[0-9]+·\)))·((\.·\.)·(('·.·')|(C·H·R·\(·[0-9]+·\))))?)*)·((\t|\s)*·(\n))+)+)?";
        private const string TOKENS = @"(\n)*·(T·O·K·E·N·S)·((\t|\s)*·(\n))+·((\t|\s)*·(T·O·K·E·N)·(\t|\s)+·[0-9]+·(\t|\s)*·=·(\t|\s)*·(\s|\*|\+|\?|\(|\)|\||'·.·'|[A-Z]+|{|})+·((\t|\s)*·(\n))+)+";
        private const string ACTIONS = @"(\n)*·(A·C·T·I·O·N·S)·((\t|\s)*·(\n))+·((\t|\s)*·(R·E·S·E·R·V·A·D·A·S·\(·\))·((\t|\s)*·(\n))+·{·((\t|\s)*·(\n))+·((\t|\s)*·[0-9]+·(\t|\s)*·=·(\t|\s)*·'·[A-Z]+·'·((\t|\s)*·(\n))+)+·}·((\t|\s)*·(\n))+)·((\t|\s)*·[A-Z]+·\(·\)·((\t|\s)*·(\n))+·{·((\t|\s)*·(\n))+·((\t|\s)*·[0-9]+·(\t|\s)*·=·(\t|\s)*·'·[A-Z]+·'·((\t|\s)*·(\n))+)+·}·((\t|\s)*·(\n))+)*";
        private const string ERRORS = @"([A-Z]*·(E·R·R·O·R)·(\t|\s)*·=·(\t|\s)*·[0-9]+)·(((\n)·[A-Z]*·(E·R·R·O·R)·(\t|\s)*·=·(\t|\s)*·[0-9]+)*)";

        private readonly TextManipulation textManipulation = new TextManipulation();
        private readonly Syntactic syntactic = new Syntactic();
        private string text = string.Empty;

        /// <summary>Constructor</summary>
        public MainView()
        {
            InitializeComponent();
        }

        /// <summary>Let the user select the file and put the path in the screen.</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void ChooseFile(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file_path.Text = dialog.FileName;
            }
            dialog.Dispose();
        }

        /// <summary>Analize the file that was choosen.</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void AnalyzeText(object sender, EventArgs e)
        {
            // Read the file
            try
            {
                text = File.ReadAllText(file_path.Text);
            }
            catch (Exception ex)
            {
                message.ForeColor = Color.Maroon;
                message.Text = ex.Message;
                message.Visible = true;
                machine_btn.Visible = false;
            }

            if (!text.Equals(string.Empty))
            {
                // Generate regex
                Regex regex = null;
                try
                {
                    regex = new Regex(@"^·(" + SETS + ")·(" + TOKENS + ")·(" + ACTIONS + ")·(" + ERRORS + ")·$");
                }
                catch (Exception ex)
                {
                    message.ForeColor = Color.Maroon;
                    message.Text = "The regular expression has some errors: " + ex.Message;
                    message.Visible = true;
                    machine_btn.Visible = false;
                }

                if(regex != null)
                {
                    // Lexical analysis
                    Task<bool> task = Task.Run(() => regex.Evaluate(text));
                    if (task.Wait(TimeSpan.FromSeconds(10)))
                    {
                        if (task.Result)
                        {
                            // Syntactic analysis
                            try
                            {
                                if (syntactic.Validate(text))
                                {
                                    message.ForeColor = Color.White;
                                    message.Text = "The text is OK";
                                    message.Visible = true;
                                    machine_btn.Visible = true;
                                }
                                else
                                {
                                    message.ForeColor = Color.White;
                                    message.Text = "The text has some syntactic errors";
                                    message.Visible = true;
                                    machine_btn.Visible = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                message.ForeColor = Color.White;
                                message.Text = "Check that there are no repeated tokens or sets: " + ex.Message;
                                message.Visible = true;
                                machine_btn.Visible = false;
                            }
                        }
                        else
                        {
                            message.ForeColor = Color.White;
                            message.Text = "The text has some lexical errors";
                            message.Visible = true;
                            machine_btn.Visible = false;
                        }
                    }
                    else
                    {
                        message.ForeColor = Color.White;
                        message.Text = "The text has some lexical errors";
                        message.Visible = true;
                        machine_btn.Visible = false;
                    }
                }
            }
        }

        /// <summary>Change to the state machine view</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void ChangeView(object sender, EventArgs e)
        {
            Hide();
            using (MachineView machineView = new MachineView(textManipulation.GetTokens(text),
                                                             textManipulation.GetSets(text),
                                                             textManipulation.GetActions(text)))
            {
                machineView.ShowDialog();
            }
            Close();
        }
    }
}