using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SolutionGenerator
{
    public class Helpers
    {
        /// <summary>Write all the file</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the file</param>
        /// <param name="tokens">The tokens of the text</param>
        /// <param name="actions">The actions of the text</param>
        /// <param name="sets">The sets of the text</param>
        /// <param name="transitions">The state machine generated</param>
        public void WriteFiles(string name,
                               string path,
                               Dictionary<string, string> tokens,
                               Dictionary<string, string> actions,
                               Dictionary<string, string> sets,
                               Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> transitions)
        {
            WriteTokenizer(name, path);
            WriteList(path, tokens);
            WriteEvaluator(name, path);
            WriteSets(path, sets);
            WriteAutomata(path, transitions);
            WriteEvaluatorCode(path, sets, transitions);
            WriteLexemas(path, tokens, actions);
        }

        /// <summary>Write the transitions for the evaluator</summary>
        /// <param name="path">The path of the file</param>
        /// <param name="Transitions">The state machine generated</param>
        public void WriteAutomata(string path,
                                 Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> Transitions)
        {
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
                    if (elementWrite[0].ToString().Equals("'") &&
                        elementWrite[elementWrite.Length - 1].ToString().Equals("'"))
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
            string fileText = File.ReadAllText(Path.Combine(path, "Evaluator.cs"));
            fileText = fileText.Replace("/* MY AUTOMATA */", automata);
            File.WriteAllText(Path.Combine(path, "Evaluator.cs"), fileText);
        }

        /// <summary>Write the evaluator file</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the file</param>
        private void WriteEvaluator(string name, string path)
        {
            string text = @"using System;
using System.Collections.Generic;
using System.Linq;

namespace " + name +@"
{
    public class Evaluator
    {
        private readonly List<Tuple<string, string>> lexemeTuple = new List<Tuple<string, string>>();
        private readonly Queue<string> Tokens;

        /// <summary>Constructor</summary>
        /// <param name=""tokens"">The tokens to evaluate</param>
        public Evaluator(Queue<string> tokens)
        {
            Tokens = tokens;
        }

        /// <summary>Evaluate the tokens using the automata</summary>
        /// <param name=""tokens"">The queue with all the elements to evaluate</param>
        /// <returns>True if the text is correct, otherwise false</returns>
        public bool Evaluate()
        {
            /* MY SETS */
            /* MY AUTOMATA */
            bool error = false;
            KeyValuePair<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> state = Transitions.ElementAt(0);
            Queue<string> tokens = new Queue<string>(Tokens);

            while (tokens.Count > 0 && !error)
            {
                state = Transitions.ElementAt(0);
                string token = string.Empty;
                string text = tokens.Dequeue();
                for (int i = 0; i < text.Length; i++)
                {
                    string actual = text[i].ToString();
                    switch (state.Key.Item1)
                    {
                        /* MY CODE */
                    }
                }
                lexemeTuple.Add(new Tuple<string, string>(text, token));
            }

            if (!error && state.Key.Item3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Get all the lexemes and his token number</summary>
        /// <returns>A list with all the lexemes and his token number</returns>
        public List<Tuple<string, int>> GetLexemes()
        {
            /* MY LEXEMES */
            List<Tuple<string, int>> lexemes = new List<Tuple<string, int>>();
            foreach (string element in Tokens)
            {
                int number = 0;
                try
                {
                    number = lex.First(x => x.Item1.Equals(element)).Item2;
                }
                catch
                {
                    string container = lexemeTuple.First(x => x.Item1.Equals(element)).Item2;
                    number = lex.First(x => x.Item1.Contains(container)).Item2;
                }
                lexemes.Add(new Tuple<string, int>(element, number));
            }
            return lexemes;
        }
    }
}
";
            File.WriteAllText(Path.Combine(path, "Evaluator.cs"), text);
        }

        /// <summary>Write the evaluator</summary>
        /// <param name="path">The path of the file</param>
        /// <param name="Transitions">The machine state generated</param>
        public void WriteEvaluatorCode(string path,
                                       Dictionary<string, string> sets,
                                       Dictionary<Tuple<string, List<int>, bool>, Dictionary<string, List<int>>> Transitions)
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
                        code += "\t\t\t\t\t\t\t\ttoken = @\"";
                        if (value.Equals("\""))
                        {
                            code += "\"\"";
                        }
                        else
                        {
                            code += value;
                        }
                        code += "\";\n\t\t\t\t\t\t\t}\n\t\t\t\t\t\t\telse ";
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
            string fileText = File.ReadAllText(Path.Combine(path, "Evaluator.cs"));
            fileText = fileText.Replace("/* MY CODE */", code.Remove(0, 6));
            File.WriteAllText(Path.Combine(path, "Evaluator.cs"), fileText);
        }

        /// <summary>Write the lexemas</summary>
        /// <param name="path">The path of the file</param>
        /// <param name="actions">The actions of the text</param>
        /// <param name="tokens">The tokens of the text</param>
        public void WriteLexemas(string path, Dictionary<string, string> tokens, Dictionary<string, string> actions)
        {
            string code = "List<Tuple<string, int>> lex = new List<Tuple<string, int>>()\n\t\t\t{\n";

            foreach (KeyValuePair<string, string> element in tokens)
            {
                string value = element.Value;
                string[] elements = value.Split(' ');
                if (elements.Length == 1)
                {
                    string evaluating = elements[0];
                    string part = elements[0];
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
                            text += evaluating[0];

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
                                value = text;
                            }
                        }
                        else
                        {
                            cont = part.Length;
                            text += " " + evaluating;
                            value = text;
                            insert = false;
                        }
                    }
                    if (insert)
                    {
                        value = text;
                    }
                }
                string number = element.Key.Remove(0, 6);
                code += "\t\t\t\tnew Tuple<string, int>(@\"";
                if (value.Equals("\""))
                {
                    code += "\"\"";
                }
                else
                {
                    code += value;
                }
                code += "\", " + number + "),\n";
            }

            foreach (KeyValuePair<string, string> element in actions)
            {
                string value = element.Value.Remove(element.Value.Length - 1, 1).Remove(0, 1);
                code += "\t\t\t\tnew Tuple<string, int>(@\"";
                if (value.Equals("\""))
                {
                    code += "\"\"";
                }
                else
                {
                    code += value;
                }
                code += "\", " + element.Key + "),\n";
            }
            code += "\t\t\t};\n";
            string fileText = File.ReadAllText(Path.Combine(path, "Evaluator.cs"));
            fileText = fileText.Replace("/* MY LEXEMES */", code);
            File.WriteAllText(Path.Combine(path, "Evaluator.cs"), fileText);
        }

        /// <summary>Write the list for the tokenization</summary>
        /// <param name="path">The path for the file</param>
        /// <param name="tokens">The tokens of the text</param>
        public void WriteList(string path, Dictionary<string, string> tokens)
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
            code += "\t\t};\n";
            string fileText = File.ReadAllText(path + "\\Tokenizer.cs");
            fileText = fileText.Replace("/* MY LIST */", code);
            File.WriteAllText(Path.Combine(path, "Tokenizer.cs"), fileText);
        }

        /// <summary>Write the list of the sets</summary>
        /// <param name="path">The path of the file</param>
        /// <param name="sets">The sets of the text</param>
        public void WriteSets(string path, Dictionary<string, string> sets)
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
            string fileText = File.ReadAllText(Path.Combine(path, "Evaluator.cs"));
            fileText = fileText.Replace("/* MY SETS */", text);
            File.WriteAllText(Path.Combine(path, "Evaluator.cs"), fileText);
        }

        /// <summary>Write the tokenizer file</summary>
        /// <param name="name">The name of the solution</param>
        /// <param name="path">The path for the file</param>
        private void WriteTokenizer(string name, string path)
        {
            string text = @"using System;
using System.Collections.Generic;

namespace " + name + @"
{
    public class Tokenizer
    {
        /// <summary>Special characters to parse</summary>
        /* MY LIST */
        /// <summary>Parse the text for evaluation</summary>
        /// <param name=""text"">The text to parse</param>
        /// <returns>A queue with all the tokens</returns>
        public Queue<string> TokenizeText(string text)
        {
            Queue<string> tokens = new Queue<string>();
            string[] elements = text.Split(new[] { ""\r\n"", ""\r"", ""\n"", ""\t"", "" "" }, StringSplitOptions.None);
            foreach (string element in elements)
            {
                if (!string.IsNullOrWhiteSpace(element) && !string.IsNullOrEmpty(element))
                {
                    string find = characters.Find(x => element.Contains(x));
                    if (string.IsNullOrEmpty(find))
                    {
                        tokens.Enqueue(element);
                    }
                    else
                    {
                        int index = element.IndexOf(find);
                        string newElement = string.Empty;
                        if (index == 0)
                        {
                            tokens.Enqueue(element.Substring(0, find.Length));
                            newElement = element.Remove(0, find.Length);
                        }
                        else
                        {
                            tokens.Enqueue(element.Substring(0, index));
                            newElement = element.Remove(0, index);
                        }
                        while (newElement.Length > 0)
                        {
                            find = characters.Find(x => newElement.Contains(x));
                            if (string.IsNullOrEmpty(find))
                            {
                                tokens.Enqueue(newElement);
                                newElement = string.Empty;
                            }
                            else
                            {
                                index = newElement.IndexOf(find);
                                if (index == 0)
                                {
                                    tokens.Enqueue(newElement.Substring(0, find.Length));
                                    newElement = newElement.Remove(0, find.Length);
                                }
                                else
                                {
                                    tokens.Enqueue(newElement.Substring(0, index));
                                    newElement = newElement.Remove(0, index);
                                }
                            }
                        }
                    }
                }
            }
            return tokens;
        }
    }
}
";
            File.WriteAllText(Path.Combine(path, "Tokenizer.cs"), text);
        }
    }
}
