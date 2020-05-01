using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Helpers
{
    public class Code
    {
        private readonly Dictionary<string, string> tokens;
        private readonly Dictionary<string, string> sets;
        private readonly Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> Transitions;

        /// <summary>Constructor</summary>
        /// <param name="t">The dictionary with the tokens</param>
        /// <param name="s">The dictionary with the sets</param>
        public Code(Dictionary<string, string> t, Dictionary<string, string> s,
                    Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> trans)
        {
            tokens = t;
            sets = s;
            Transitions = trans;
        }

        /// <summary>Write the transitions for the evaluator</summary>
        /// <param name="path">The path of the file</param>
        public void WriteAutomata(string path)
         {
            Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> Transitionsnew = new Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>>()
            {
                { new Tuple<string, List<int>, bool>("A", new List<int>(){ 1, 2, }, true), new Dictionary<string, List<int>>(){ { "LETRA", new List<int>(){ 1, 2, 3, } }, } },
            };
            string automata = "Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> Transitions ";
            automata += "= new Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>>()\n\t\t\t{\n";
            foreach (KeyValuePair<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> item in Transitions)
            {
                automata += "\t\t\t\t{ new Tuple<string, List<int>, bool>(\"" + item.Key.Item1 + "\", new List<int>(){";
                foreach (int number in item.Key.Item2)
                {
                    automata += " " + number + ",";
                }

                if (item.Key.Item3)
                {
                    automata += " }, true), new Dictionary<string, List<int>>(){";
                }
                else
                {
                    automata += " }, false), new Dictionary<string, List<int>>(){";
                }

                foreach (KeyValuePair<string, List<int>> element in item.Value)
                {
                    string elementWrite = element.Key;
                    if (elementWrite[0].ToString().Equals("'") && elementWrite[elementWrite.Length - 1].ToString().Equals("'"))
                    {
                        elementWrite = elementWrite.Remove(0, 1);
                        elementWrite = elementWrite.Remove(elementWrite.Length - 1, 1);
                    }

                    if (elementWrite.Equals("\""))
                    {
                        automata += " { @\"\"\"\", new List<int>(){";
                    }
                    else
                    {
                        automata += " { @\"" + elementWrite + "\", new List<int>(){";
                    }

                    foreach (int n in element.Value)
                    {
                        automata += " " + n + ",";
                    }
                    automata += " } },";
                }
                automata += " } },\n";
            }
            automata += "\t\t\t};\n";
            string fileText = File.ReadAllText(path + "\\Evaluator.cs");
            fileText = fileText.Replace("/* MY AUTOMATA */", automata);
            File.WriteAllText(path + "\\Evaluator.cs", fileText);
        }

        /// <summary>Write the evaluator</summary>
        /// <param name="path">The path of the file</param>
        public void WriteEvaluator(string path)
        {
            string code = string.Empty;
            foreach (KeyValuePair<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> item in Transitions)
            {
                code += "\t\t\t\t\t\tcase \"" + item.Key.Item1 + "\":\n\t\t\t\t\t\t\t";
                if (item.Value.Count > 0)
                {
                    foreach (KeyValuePair<string, List<int>> element in item.Value)
                    {
                        code += "if (";
                        string value = element.Key;
                        if (sets.ContainsKey(value))
                        {
                            code += "SETS.ContainsKey(\"" + value + "\") && SETS[\"" + value + "\"].Contains(actual)";
                        }
                        else
                        {
                            value = value.Remove(value.Length - 1, 1).Remove(0, 1);
                            code += "actual.Equals(@\"";
                            if (value.Equals("\""))
                            {
                                code += "\"\"";
                            }
                            else
                            {
                                code += value;
                            }
                            code += "\")";
                        }
                        code += ")\n\t\t\t\t\t\t\t{\n\t\t\t\t\t\t\t\tList<int> newValues = state.Value[@\"";
                        if (value.Equals("\""))
                        {
                            code += "\"\"";
                        }
                        else
                        {
                            code += value;
                        }
                        code += "\"];\n\t\t\t\t\t\t\t\tstate = Transitions.First(x => x.Key.Item2.SequenceEqual(newValues));\n";
                        code += "\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\telse ";
                    }
                    code += "\n\t\t\t\t\t\t\t{\n\t\t\t\t\t\t\t\terror = true;\n\t\t\t\t\t\t\t}\n";
                }
                else
                {
                    code += "if (i < text.Length - 1 || tokens.Count != 0)\n\t\t\t\t\t\t\t{\n\t\t\t\t\t\t\t\t";
                    code += "error = true;\n\t\t\t\t\t\t\t}\n";
                }
                code += "\t\t\t\t\t\t\tbreak;\n\n";
            }
            string fileText = File.ReadAllText(path + "\\Evaluator.cs");
            fileText = fileText.Replace("/* MY CODE */", code.Remove(0, 6));
            File.WriteAllText(path + "\\Evaluator.cs", fileText);
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

        /// <summary>Write the list of the sets</summary>
        /// <param name="path">The path of the file</param>
        public void WriteSets(string path)
        {
            string text = string.Empty;
            if (sets.Count > 0)
            {
                text = "Dictionary<string, List<string>> SETS = new Dictionary<string, List<string>>()\n\t\t\t{\n";
                foreach (KeyValuePair<string, string> item in sets)
                {
                    text += "\t\t\t\t{ \"" + item.Key + "\", new List<string>(){";
                    string[] elements = item.Value.Split('+');
                    foreach (string subelement in elements)
                    {
                        string[] interval = subelement.Split(new string[] { ".." }, StringSplitOptions.None);
                        if (interval.Length > 1)
                        {
                            double start;
                            double finish;
                            if (interval[0].Contains("CHR"))
                            {
                                start = int.Parse(interval[0].Remove(interval[0].Length - 1, 1).Remove(0, 4));
                                finish = int.Parse(interval[1].Remove(interval[1].Length - 1, 1).Remove(0, 4));
                            }
                            else
                            {
                                start = interval[0].Remove(interval[0].Length - 1, 1).Remove(0, 1)[0];
                                finish = interval[1].Remove(interval[1].Length - 1, 1).Remove(0, 1)[0];
                            }

                            for (int i = Convert.ToInt32(start); i <= Convert.ToInt32(finish); i++)
                            {
                                char character = Convert.ToChar(i);
                                if (!string.IsNullOrEmpty(character.ToString()))
                                {
                                    text += " @\"";
                                    if (character.Equals('\"'))
                                    {
                                        text += "\"\"";
                                    }
                                    else
                                    {
                                        text += character;
                                    }
                                    text += "\",";
                                }
                            }
                        }
                        else
                        {
                            if (interval[0].Contains("CHR"))
                            {
                                int value = int.Parse(interval[0].Remove(interval[0].Length - 1, 1).Remove(0, 4));
                                char character = Convert.ToChar(value);
                                if (!string.IsNullOrEmpty(character.ToString()))
                                {
                                    text += " @\"";
                                    if (character.Equals('\"'))
                                    {
                                        text += "\"\"";
                                    }
                                    else
                                    {
                                        text += character;
                                    }
                                    text += "\",";
                                }
                            }
                            else
                            {
                                char character = interval[0].Remove(interval[0].Length - 1, 1).Remove(0, 1)[0];
                                if (!string.IsNullOrEmpty(character.ToString()))
                                {
                                    text += " @\"";
                                    if (character.Equals('\"'))
                                    {
                                        text += "\"\"";
                                    }
                                    else
                                    {
                                        text += character;
                                    }
                                    text += "\",";
                                }
                            }
                        }
                    }
                    text += " } },\n";
                }
                text += "\t\t\t};\n";
            }
            string fileText = File.ReadAllText(path + "\\Evaluator.cs");
            fileText = fileText.Replace("/* MY SETS */", text);
            File.WriteAllText(path + "\\Evaluator.cs", fileText);
        }
    }
}
