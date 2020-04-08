using System.Collections.Generic;
using System.Windows.Forms;

namespace scanner_generator.UI
{
    public partial class MachineView : Form
    {
        private readonly string regex = string.Empty;
        
        /// <summary>Constructor</summary>
        /// <param name="tokens">The dictionary with the tokens</param>
        public MachineView(Dictionary<string, string> tokens)
        {
            InitializeComponent();
        }

        /// <summary>Create the regular expressions for the tokens</summary>
        /// <param name="tokens">The dictionary with the tokens</param>
        /// <returns>The regular expression contructed</returns>
        private string MakeExpression(Dictionary<string, string> tokens)
        {
            string regex = string.Empty;
            foreach (KeyValuePair<string, string> token in tokens)
            {
                regex += "|(";
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
                                regex += evaluatedText.Substring(0, 3) + "·";
                                evaluatedText = evaluatedText.Remove(0, 3);
                                cont += 3;
                            }
                            else if (opertors.Contains(evaluatedText[0].ToString()))
                            {
                                if (regex[regex.Length - 1].Equals('·'))
                                {
                                    regex = regex.Remove(regex.Length - 1, 1);
                                }

                                if (evaluatedText[0].Equals('*') || evaluatedText[0].Equals('+') || evaluatedText[0].Equals('?'))
                                {
                                    regex += evaluatedText.Substring(0, 1) + "·";
                                }
                                else
                                {
                                    regex += evaluatedText.Substring(0, 1);
                                }

                                evaluatedText = evaluatedText.Remove(0, 1);
                                cont += 1;
                            }
                            else
                            {
                                regex += evaluatedText + "·";
                                cont = evaluatedText.Length;
                            }
                        }
                    }
                    else
                    {
                        if (opertors.Contains(part))
                        {
                            if (regex[regex.Length - 1].Equals('·'))
                            {
                                regex = regex.Remove(regex.Length - 1, 1);
                            }

                            if (part.Equals('*') || part.Equals('+') || part.Equals('?'))
                            {
                                regex += part + "·";
                            }
                            else
                            {
                                regex += part;
                            }
                        }
                    }
                }

                if (regex[regex.Length - 1].Equals('·'))
                {
                    regex = regex.Remove(regex.Length - 1, 1);
                }
                regex += ")";
            }
            return regex.Remove(0, 1);
        }
    }
}
