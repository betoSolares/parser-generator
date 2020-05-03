using System.Collections.Generic;

namespace Helpers
{
    public class Expression
    {
        /// <summary>Create the regular expressions for the tokens</summary>
        /// <param name="tokens">The dictionary with the tokens</param>
        /// <returns>The regular expression contructed</returns>
        public string MakeExpression(Dictionary<string, string> tokens)
        {
            string regex = string.Empty;
            foreach (KeyValuePair<string, string> token in tokens)
            {
                regex += "|(";
                string[] elements = token.Value.Split(' ');
                foreach (string part in elements)
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
                        string evaluating = part;
                        int cont = 0;
                        while (cont < part.Length)
                        {
                            if (evaluating.Length >= 3 && evaluating[0].ToString().Equals("'")
                                && evaluating[2].ToString().Equals("'"))
                            {
                                regex += evaluating.Substring(0, 3) + "·";
                                evaluating = evaluating.Remove(0, 3);
                                cont += 3;
                            }
                            else if (opertors.Contains(evaluating[0].ToString()))
                            {
                                if (regex[regex.Length - 1].Equals('·') && !evaluating[0].Equals('('))
                                {
                                    regex = regex.Remove(regex.Length - 1, 1);
                                }

                                if (evaluating[0].Equals('*')
                                    || evaluating[0].Equals('+')
                                    || evaluating[0].Equals('?'))
                                {
                                    regex += evaluating.Substring(0, 1) + "·";
                                }
                                else
                                {
                                    regex += evaluating.Substring(0, 1);
                                }

                                evaluating = evaluating.Remove(0, 1);
                                cont += 1;
                            }
                            else
                            {
                                regex += evaluating + "·";
                                cont = evaluating.Length;
                            }
                        }
                    }
                    else
                    {
                        if (opertors.Contains(part))
                        {
                            if (regex[regex.Length - 1].Equals('·') && !part.Equals("("))
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
