using System.Collections.Generic;

namespace Helpers
{
    public class Syntactic
    {
        private readonly TextManipulation textManipulation = new TextManipulation();

        /// <summary>Validate that the text is syntactically correct</summary>
        /// <param name="text">The text to validate</param>
        /// <returns>True if the text is correct, otherwise false</returns>
        public bool Validate(string text)
        {
            Dictionary<string, string> sets = textManipulation.GetSets(text);
            Dictionary<string, string> tokens = textManipulation.GetTokens(text);
            bool valid = true;
            foreach (KeyValuePair<string, string> token in tokens)
            {
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
                    if (!string.IsNullOrWhiteSpace(part) && !string.IsNullOrEmpty(part))
                    {
                        if (part.Length > 1)
                        {
                            string evaluating = part;
                            int cont = 0;
                            while (cont < part.Length)
                            {
                                if (evaluating.Length >= 3 && evaluating[0].ToString().Equals("'")
                                    && evaluating[2].ToString().Equals("'"))
                                {
                                    evaluating = evaluating.Remove(0, 3);
                                    cont += 3;
                                }
                                else if (opertors.Contains(evaluating[0].ToString()))
                                {
                                    evaluating = evaluating.Remove(0, 1);
                                    cont += 1;
                                }
                                else
                                {
                                    try
                                    {
                                        if (!sets.ContainsKey(evaluating))
                                        {
                                            valid = false;
                                        }
                                    }
                                    catch
                                    {
                                        valid = false;
                                    }
                                    cont = evaluating.Length;
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
            }
            return valid;
        }
    }
}
