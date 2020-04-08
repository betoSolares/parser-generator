using RegularExpression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class MainView : Form
    {
        /// <summary>Parts of the regular expression</summary>
        private const string SETS = @"((S·E·T·S)·((\t|\s)*·(\n))+·((\t|\s)*·[A-Z]+·(\t|\s)*·=·(\t|\s)*·((('·.·')|(C·H·R·\(·[0-9]+·\)))·((\.·\.)·(('·.·')|(C·H·R·\(·[0-9]+·\))))?·(\+·(('·.·')|(C·H·R·\(·[0-9]+·\)))·((\.·\.)·(('·.·')|(C·H·R·\(·[0-9]+·\))))?)*)·((\t|\s)*·(\n))+)+)?";
        private const string TOKENS = @"(\n)*·(T·O·K·E·N·S)·((\t|\s)*·(\n))+·((\t|\s)*·(T·O·K·E·N)·(\t|\s)+·[0-9]+·(\t|\s)*·=·(\t|\s)*·(\s|\*|\+|\?|\(|\)|\||'·.·'|[A-Z]+|{|})+·((\t|\s)*·(\n))+)+";
        private const string ACTIONS = @"(\n)*·(A·C·T·I·O·N·S)·((\t|\s)*·(\n))+·((\t|\s)*·(R·E·S·E·R·V·A·D·A·S·\(·\))·((\t|\s)*·(\n))+·{·((\t|\s)*·(\n))+·((\t|\s)*·[0-9]+·(\t|\s)*·=·(\t|\s)*·'·[A-Z]+·'·((\t|\s)*·(\n))+)+·}·((\t|\s)*·(\n))+)·((\t|\s)*·[A-Z]+·\(·\)·((\t|\s)*·(\n))+·{·((\t|\s)*·(\n))+·((\t|\s)*·[0-9]+·(\t|\s)*·=·(\t|\s)*·'·[A-Z]+·'·((\t|\s)*·(\n))+)+·}·((\t|\s)*·(\n))+)*";
        private const string ERRORS = @"([A-Z]*·(E·R·R·O·R)·(\t|\s)*·=·(\t|\s)*·[0-9]+)·(((\n)·[A-Z]*·(E·R·R·O·R)·(\t|\s)*·=·(\t|\s)*·[0-9]+)*)";

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
                catch (BadExpressionException ex)
                {
                    message.ForeColor = Color.Maroon;
                    message.Text = "The regular expression has some errors: " + ex.Message;
                    message.Visible = true;
                    machine_btn.Visible = false;
                }

                if(regex != null)
                {
                    // Lexical analysis
                    if (regex.Evaluate(text))
                    {
                        // Syntactic analysis
                        try
                        {
                            if (SyntacticValidation(text))
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
            }
        }

        /// <summary>Transform an array into a dictionary</summary>
        /// <param name="elements">The array to transform</param>
        /// <returns>A new dictionary</returns>
        private Dictionary<string, string> MakeDictionary(string[] elements)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string element in elements)
            {
                if (!element.Equals(string.Empty) && !string.IsNullOrWhiteSpace(element))
                {
                    string[] parts = element.Split(new[] { '=' }, 2);
                    result.Add(parts[0].Trim(new[] { '\t', ' ' }), parts[1].Trim(new[] { '\t', ' ' }));
                }
            }
            return result;
        }

        /// <summary>Get all the elements under the SETS section</summary>
        /// <param name="text">The text to parse</param>
        /// <returns>A dictionary with all the SETS</returns>
        private Dictionary<string, string> GetSets(string text)
        {
            if (text.Contains("SETS"))
            {
                int indexFrom = text.IndexOf("SETS") + "SETS".Length;
                int indexTo = text.LastIndexOf("TOKENS");
                string result = text.Substring(indexFrom, indexTo - indexFrom);
                string[] sets = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                return MakeDictionary(sets);
            }
            return null;
        }

        /// <summary>Get all the elements under the TOKENS section</summary>
        /// <param name="text">The text to parse</param>
        /// <returns>A dictionary with all the TOKENS</returns>
        private Dictionary<string, string> GetTokens(string text)
        {
            int indexFrom = text.IndexOf("TOKENS") + "TOKENS".Length;
            int indexTo = text.LastIndexOf("ACTIONS");
            string result = text.Substring(indexFrom, indexTo - indexFrom);
            string[] tokens = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return NormalizeDictionary(MakeDictionary(tokens));
        }

        /// <summary>Delete the call functions from the TOKENS</summary>
        /// <param name="dictionary">The original dictionary to normalize</param>
        /// <returns>A new dictionary</returns>
        private Dictionary<string, string> NormalizeDictionary(Dictionary<string, string> dictionary)
        {
            Dictionary<string, string> newDictionary = new Dictionary<string, string>(dictionary);
            foreach(KeyValuePair<string, string> element in dictionary)
            {
                int indexFrom = element.Value.IndexOf("{");
                int indexTo = element.Value.LastIndexOf("}");
                if (indexFrom != -1 && indexTo != -1)
                {
                    string newValue = element.Value.Remove(indexFrom, indexTo - indexFrom + 1);
                    newDictionary[element.Key] = newValue.Trim(new[] { '\t', ' ' });
                }
            }
            return newDictionary;
        }

        /// <summary>Validate that the text is syntactically correct</summary>
        /// <param name="text">The text to validate</param>
        /// <returns>True if the text is correct, otherwise false</returns>
        private bool SyntacticValidation(string text)
        {
            Dictionary<string, string> sets = GetSets(text);
            Dictionary<string, string> tokens = GetTokens(text);
            bool valid = true;
            foreach (KeyValuePair<string, string> token in tokens)
            {
                string[] element = token.Value.Split(' ');
                foreach (string part in element)
                {
                    List<string> opertors = new List<string>
                    {
                        "(",
                        ")",
                        "*",
                        "+",
                        "?",
                        "·",
                        "|"
                    };
                    if (part.Length > 1)
                    {
                        string evaluatedText = part;
                        int cont = 0;
                        while (cont < part.Length)
                        {
                            if (evaluatedText.Length >= 3 && evaluatedText[0].ToString().Equals("'") && evaluatedText[2].ToString().Equals("'"))
                            {
                                evaluatedText = evaluatedText.Remove(0, 3);
                                cont += 3;
                            }
                            else if (opertors.Contains(evaluatedText[0].ToString()))
                            {
                                evaluatedText = evaluatedText.Remove(0, 1);
                                cont += 1;
                            }
                            else
                            {
                                if (!sets.ContainsKey(evaluatedText))
                                {
                                    valid = false;
                                }
                                cont = evaluatedText.Length;
                            }
                        }
                    }
                    else
                    {
                        if (!opertors.Contains(part))
                        {
                            valid = false;
                        }
                    }
                }
            }
            return valid;
        }

        /// <summary>Change to the state machine view</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void ChangeView(object sender, EventArgs e)
        {
            Hide();
            MachineView machineView = new MachineView(GetTokens(text));
            machineView.ShowDialog();
            Close();
        }
    }
}