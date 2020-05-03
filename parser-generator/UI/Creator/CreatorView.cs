using SolutionGenerator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace parser_generator.UI
{
    public partial class CreatorView : Form
    {
        private readonly Dictionary<string, string> _sets;
        private readonly Dictionary<string, string> _tokens;
        private readonly Dictionary<string, string> _actions;
        private readonly Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> Transitions;

        /// <summary>Constructor</summary>
        public CreatorView(Dictionary<string, string> sets,
                           Dictionary<string, string> tokens,
                           Dictionary<string, string> actions,
                           Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> trans)
        {
            InitializeComponent();
            _sets = sets;
            _tokens = tokens;
            _actions = actions;
            Transitions = trans;
        }

        /// <summary>Let the user select the folder.</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void ChooseFile(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                file_path.Text = dialog.SelectedPath;
            }
            dialog.Dispose();
        }

        /// <summary>Generate a new VS project in the specific path</summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object that is being handled</param>
        private void GenerateProject(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(file_path.Text) || string.IsNullOrWhiteSpace(file_path.Text))
            {
                message.Text = "Please select a directory for the solution";
                message.ForeColor = Color.White;
            }
            else
            {
                if (VerifyName(name.Text))
                {
                    try
                    {
                        Generator generator = new Generator(name.Text,
                                                            file_path.Text,
                                                            _tokens,
                                                            _actions,
                                                            _sets,
                                                            Transitions);
                        generator.GenerateSolution();
                        message.Text = "Solution generated";
                        message.ForeColor = Color.White;
                    }
                    catch (Exception ex)
                    {
                        message.Text = "An error ocurred: " + ex.Message;
                        message.ForeColor = Color.Maroon;
                    }
                }
                else
                {
                    message.Text = "The name can only contain letters";
                    message.ForeColor = Color.White;
                }
            }
        }

        /// <summary>Check that the name is correct</summary>
        /// <param name="name">The name to check</param>
        /// <returns>True if the name is correct, otherwise false</returns>
        private bool VerifyName(string name)
        {
            List<string> characters = new List<string>()
            {
                " ",
                "!",
                "\"",
                "#",
                "$",
                "%",
                "&",
                "/",
                "(",
                ")",
                "=",
                "?",
                "\\",
                "¡",
                "¿",
                "¨",
                "´",
                "+",
                "*",
                "~",
                "[",
                "{",
                "^",
                "}",
                "]",
                "`",
                "<",
                "'",
                ">",
                "|",
                "°",
                "¬",
                "ñ",
                "1",
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "0",
                ";",
                ",",
                ".",
                ":",
                "-",
                "_",
                "\t",
                "\r",
                "\n"
            };
            return !characters.Any(x => name.Contains(x));
        }
    }
}
