using RegularExpression;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class View : Form
    {
        /// <summary>Parts of the regular expression</summary>
        private const string SETS = @"((S·E·T·S)·((\t|\s)*·(\n))+·((\t|\s)*·[A-Z]+·(\t|\s)*·=·(\t|\s)*·((('·.·')|(C·H·R·\(·[0-9]+·\)))·((\.·\.)·(('·.·')|(C·H·R·\(·[0-9]+·\))))?·(\+·(('·.·')|(C·H·R·\(·[0-9]+·\)))·((\.·\.)·(('·.·')|(C·H·R·\(·[0-9]+·\))))?)*)·((\t|\s)*·(\n))+)+)?";
        private const string TOKENS = @"(\n)*·(T·O·K·E·N·S)·((\t|\s)*·(\n))+·((\t|\s)*·(T·O·K·E·N)·(\t|\s)+·[0-9]+·(\t|\s)*·=·(\t|\s)*·(\s|\*|\+|\?|\(|\)|\||'·.·'|[A-Z]+|{|})+·((\t|\s)*·(\n))+)+";
        private const string ACTIONS = @"(\n)*·(A·C·T·I·O·N·S)·((\t|\s)*·(\n))+·((\t|\s)*·(R·E·S·E·R·V·A·D·A·S·\(·\))·((\t|\s)*·(\n))+·{·((\t|\s)*·(\n))+·((\t|\s)*·[0-9]+·(\t|\s)*·=·(\t|\s)*·'·[A-Z]+·'·((\t|\s)*·(\n))+)+·}·((\t|\s)*·(\n))+)·((\t|\s)*·[A-Z]+·\(·\)·((\t|\s)*·(\n))+·{·((\t|\s)*·(\n))+·((\t|\s)*·[0-9]+·(\t|\s)*·=·(\t|\s)*·'·[A-Z]+·'·((\t|\s)*·(\n))+)+·}·((\t|\s)*·(\n))+)*";
        private const string ERRORS = @"([A-Z]*·(E·R·R·O·R)·(\t|\s)*·=·(\t|\s)*·[0-9]+)·(((\n)·[A-Z]*·(E·R·R·O·R)·(\t|\s)*·=·(\t|\s)*·[0-9]+)*)";

        /// <summary>Constructor</summary>
        public View()
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
            string text = string.Empty;
            try
            {
                text = File.ReadAllText(file_path.Text);
            }
            catch (Exception ex)
            {
                message.ForeColor = Color.Maroon;
                message.Text = ex.Message;
                message.Visible = true;
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
                }

                if(regex != null)
                {
                    // Lexical analysis
                    if (regex.Evaluate(text))
                    {
                        message.ForeColor = Color.White;
                        message.Text = "The text is correct";
                        message.Visible = true;
                    }
                    else
                    {
                        message.ForeColor = Color.White;
                        message.Text = "The text has some errors";
                        message.Visible = true;
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
                if (!element.Equals(string.Empty))
                {
                    string[] parts = element.Split(new[] { '=' });
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
    }
}