using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Helpers
{
    public class Code
    {
        private readonly Dictionary<string, string> tokens;
        private readonly Dictionary<string, string> sets;

        /// <summary>Constructor</summary>
        /// <param name="t">The dictionary with the tokens</param>
        /// <param name="s">The dictionary with the sets</param>
        public Code(Dictionary<string, string> t, Dictionary<string, string> s)
        {
            tokens = t;
            sets = s;
        }

        /// <summary>Write the list for the tokenization</summary>
        /// <param name="path">The path of the file</param>
        public void WriteList(string path)
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> token in tokens)
            {
                string[] elements = token.Value.Split(' ');
                foreach (string part in elements)
                {
                    if (part.Length > 1)
                    {
                        string evaluating = part;
                        int cont = 0;
                        bool insert = false;
                        string text = string.Empty;
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
                        while (cont < part.Length)
                        {
                            if (evaluating.Length >= 3 && evaluating[0].ToString().Equals("'")
                                && evaluating[2].ToString().Equals("'"))
                            {
                                text += evaluating.Substring(1, 1);
                                evaluating = evaluating.Remove(0, 3);
                                cont += 3;
                                insert = true;
                            }
                            else if (opertors.Contains(evaluating[0].ToString()))
                            {
                                if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
                                {
                                    list.Add(text);
                                    text = string.Empty;
                                    insert = false;
                                }

                                if (evaluating.Length >= 4 && evaluating[1].ToString().Equals("'")
                                && evaluating[3].ToString().Equals("'"))
                                {
                                    text += evaluating.Substring(2, 1);
                                    evaluating = evaluating.Remove(0, 4);
                                    cont += 4;
                                    insert = true;
                                }
                                else
                                {
                                    cont = part.Length;
                                }
                            }
                            else
                            {
                                cont = part.Length;
                                if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
                                {
                                    list.Add(text);
                                }
                                insert = false;
                            }
                        }
                        if (insert)
                        {
                            list.Add(text);
                        }
                    }
                }
            }
            list = list.Distinct().ToList();
            list = list.OrderByDescending(x => x.Length).ToList();
            string code = "List<string> characters = new List<string>()\n\t\t{\n";
            foreach (string item in list)
            {
                if (item.Equals("\""))
                {
                    code += "\t\t\t@\"\"" + item + "\",\n";
                }
                else
                {
                    code += "\t\t\t@\"" + item + "\",\n";
                }
            }
            code += "\t\t};";
            string fileText = File.ReadAllText(path + "\\Tokenizer.cs");
            fileText = fileText.Replace("/* MY LIST */", code);
            File.WriteAllText(path + "\\Tokenizer.cs", fileText);
        }
    }
}
