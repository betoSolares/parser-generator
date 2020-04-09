using System;
using System.Collections.Generic;
using System.Linq;

namespace RegularExpression
{
    public class Regex
    {
        /// <summary>Attributes of the class</summary>
        public string Expression { get; private set; }
        public Node Tree { get; private set; }
        public Dictionary<Tuple<int, string>, Tuple<List<int>[], bool>> FirstLastTable { get; private set; }
        public Dictionary<Tuple<int, string>, List<int>> FollowsTable { get; private set; }
        public Dictionary<Tuple<string, List<int>>, Dictionary<string, List<int>>> Transitions { get; private set; }

        private readonly Tokenizer tokenizer = new Tokenizer();
        private readonly Utils utils = new Utils();
        private int count = 0;

        /// <summary>Constructor</summary>
        /// <param name="regex">The regular expression to use</param>
        public Regex(string regex)
        {
            Expression = regex;
            CreateTree(regex);
            Tree = AddTerminal(Tree);
            FirstLastTable = new Dictionary<Tuple<int, string>, Tuple<List<int>[], bool>>();
            CreateFirstlastsTable(Tree);
            FollowsTable = utils.CreateDictionary(Tree);
            CreateFollowsTable(Tree);
            Transitions = new Dictionary<Tuple<string, List<int>>, Dictionary<string, List<int>>>();
            CreateTransitions();
        }

        //TODO: Find an efficient way to evaluate the text using the tree.
        /// <summary>Check if the text follows the rules by the regex</summary>
        /// <param name="text">The text to evaluate</param>
        /// <returns>True if the text is correct, otherwise false</returns>
        public bool Evaluate(string text)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^(((SETS)((\t|\s)*(\n))+((\t|\s)*[A-Z]+(\t|\s)*=(\t|\s)*((('.')|(CHR\([0-9]+\)))((\.\.)(('.')|(CHR\([0-9]+\))))?(\+(('.')|(CHR\([0-9]+\)))((\.\.)(('.')|(CHR\([0-9]+\))))?)*)((\t|\s)*(\n))+)+)?)((\n)*(TOKENS)((\t|\s)*(\n))+((\t|\s)*(TOKEN)(\t|\s)+[0-9]+(\t|\s)*=(\t|\s)*(\s|\*|\+|\?|\(|\)|\||'.'|[A-Z]+|{|})+((\t|\s)*(\n))+)+)((\n)*(ACTIONS)((\t|\s)*(\n))+((\t|\s)*(RESERVADAS\(\))((\t|\s)*(\n))+{((\t|\s)*(\n))+((\t|\s)*[0-9]+(\t|\s)*=(\t|\s)*'[A-Z]+'((\t|\s)*(\n))+)+}((\t|\s)*(\n))+)((\t|\s)*[A-Z]+\(\)((\t|\s)*(\n))+{((\t|\s)*(\n))+((\t|\s)*[0-9]+(\t|\s)*=(\t|\s)*'[A-Z]+'((\t|\s)*(\n))+)+}((\t|\s)*(\n))+)*)((([A-Z]*ERROR(\t|\s)*=(\t|\s)*[0-9]+)((\n[A-Z]*ERROR(\t|\s)*=(\t|\s)*[0-9]+)*)))");
            System.Text.RegularExpressions.Match match = regex.Match(text);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Add the terminal node to the tree</summary>
        /// <param name="node">The root node</param>
        /// <returns>A new tree</returns>
        private Node AddTerminal(Node node)
        {
            Node temp = new Node("·", count + 2);
            Node rightChild = new Node("#", count + 1)
            {
                Parent = temp
            };
            temp.RightChild = rightChild;
            Node leftChild = node;
            leftChild.Parent = temp;
            temp.LeftChild = leftChild;
            count += 2;
            return temp;
        }

        /// <summary>Creates the abstract syntax tree for the analysis</summary>
        /// <param name="regex">The regular expression for the tree</param>
        private void CreateTree(string regex)
        {
            try
            {
                int id = 0;
                List<string> tokens = tokenizer.TokenizeExpression(regex);
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
                Stack<string> T = new Stack<string>();
                Stack<Node> S = new Stack<Node>();
                foreach (string token in tokens)
                {
                    if (!opertors.Contains(token))
                    {
                        id++;
                        count = id;
                        S.Push(new Node(token, id));
                    }
                    else if (token.Equals("("))
                    {
                        T.Push(token);
                    }
                    else if (token.Equals(")"))
                    {
                        while (T.Count > 0 && !T.Peek().Equals("("))
                        {
                            if (T.Count == 0)
                            {
                                throw new BadExpressionException("Missing operators");
                            }
                            else if (S.Count < 2)
                            {
                                throw new BadExpressionException("Missing operators");
                            }
                            else
                            {
                                id++;
                                count = id;
                                Node temp = new Node(T.Pop(), id);
                                Node rightChild = S.Pop();
                                rightChild.Parent = temp;
                                temp.RightChild = rightChild;
                                Node leftChild = S.Pop();
                                leftChild.Parent = temp;
                                temp.LeftChild = leftChild;
                                S.Push(temp);
                            }
                        }
                        T.Pop();
                    }
                    else if (opertors.Contains(token))
                    {
                        if (token.Equals("*") || token.Equals("+") || token.Equals("?"))
                        {
                            id++;
                            count = id;
                            Node temp = new Node(token, id);
                            if (S.Count < 1)
                            {
                                throw new BadExpressionException("Missing operators");
                            }
                            else
                            {
                                Node leftChild = S.Pop();
                                leftChild.Parent = temp;
                                temp.LeftChild = leftChild;
                                S.Push(temp);
                            }
                        }
                        else if (T.Count > 0 && !T.Peek().Equals("(") && utils.CheckPrecedence(token, T.Peek(), opertors))
                        {
                            if (S.Count >= 2)
                            {
                                id++;
                                count = id;
                                Node temp = new Node(T.Pop(), id);
                                Node rightChild = S.Pop();
                                rightChild.Parent = temp;
                                temp.RightChild = rightChild;
                                Node leftChild = S.Pop();
                                leftChild.Parent = temp;
                                temp.LeftChild = leftChild;
                                S.Push(temp);
                            }
                            else
                            {
                                throw new BadExpressionException("Missing operators");
                            }
                        }

                        if (!token.Equals("*") && !token.Equals("+") && !token.Equals("?"))
                        {
                            T.Push(token);
                        }
                    }
                    else
                    {
                        throw new BadExpressionException("Missing operators");
                    }
                }

                while (T.Count > 0)
                {
                    if (!T.Peek().Equals("(") && S.Count >= 2)
                    {
                        id++;
                        count = id;
                        Node temp = new Node(T.Pop(), id);
                        Node rightChild = S.Pop();
                        rightChild.Parent = temp;
                        temp.RightChild = rightChild;
                        Node leftChild = S.Pop();
                        leftChild.Parent = temp;
                        temp.LeftChild = leftChild;
                        S.Push(temp);
                    }
                    else
                    {
                        throw new BadExpressionException("Missing operators");
                    }
                }

                if (S.Count == 1)
                {
                    Tree = S.Pop();
                }
                else
                {
                    throw new BadExpressionException("Missing operators");
                }
            }
            catch (Exception e)
            {
                throw new BadExpressionException(e.Message);
            }
        }

        /// <summary>Transverse the tree and create the first and lasts</summary>
        /// <param name="node">The current node</param>
        private void CreateFirstlastsTable(Node node)
        {
            if (node != null)
            {
                CreateFirstlastsTable(node.LeftChild);
                CreateFirstlastsTable(node.RightChild);

                if (node.LeftChild == null && node.RightChild == null)
                {
                    List<int> firsts = new List<int> { node.Identifier };
                    List<int> lasts = new List<int> { node.Identifier };
                    List<int>[] values = new List<int>[2] { firsts, lasts };
                    Tuple<int, string> identifier = new Tuple<int, string>(node.Identifier, node.Value);
                    Tuple<List<int>[], bool> data = new Tuple<List<int>[], bool>(values, false);
                    FirstLastTable.Add(identifier, data);
                }
                else
                {
                    List<int> firsts = new List<int>();
                    List<int> lasts = new List<int>();
                    bool nullable = false;
                    Tuple<int, string> key = new Tuple<int, string>(node.LeftChild.Identifier, node.LeftChild.Value);
                    Tuple<List<int>[], bool> c1 = FirstLastTable[key];

                    if (node.Value.Equals("|"))
                    {
                        key = new Tuple<int, string>(node.RightChild.Identifier, node.RightChild.Value);
                        Tuple<List<int>[], bool> c2 = FirstLastTable[key];
                        firsts.AddRange(c1.Item1[0]);
                        firsts.AddRange(c2.Item1[0]);
                        lasts.AddRange(c1.Item1[1]);
                        lasts.AddRange(c2.Item1[1]);
                        if (c1.Item2 || c2.Item2)
                            nullable = true;

                    }
                    else if (node.Value.Equals("·"))
                    {
                        key = new Tuple<int, string>(node.RightChild.Identifier, node.RightChild.Value);
                        Tuple<List<int>[], bool> c2 = FirstLastTable[key];

                        if (c1.Item2)
                        {
                            firsts.AddRange(c1.Item1[0]);
                            firsts.AddRange(c2.Item1[0]);
                        }
                        else
                        {
                            firsts.AddRange(c1.Item1[0]);
                        }

                        if (c2.Item2)
                        {
                            lasts.AddRange(c1.Item1[1]);
                            lasts.AddRange(c2.Item1[1]);
                        }
                        else
                        {
                            lasts.AddRange(c2.Item1[1]);
                        }

                        if (c1.Item2 && c2.Item2)
                            nullable = true;
                    }
                    else if (node.Value.Equals("*"))
                    {
                        firsts.AddRange(c1.Item1[0]);
                        lasts.AddRange(c1.Item1[1]);
                        nullable = true;
                    }
                    else if (node.Value.Equals("+"))
                    {
                        firsts.AddRange(c1.Item1[0]);
                        lasts.AddRange(c1.Item1[1]);
                    }
                    else
                    {
                        firsts.AddRange(c1.Item1[0]);
                        lasts.AddRange(c1.Item1[1]);
                        nullable = true;
                    }

                    List<int>[] values = new List<int>[2] { firsts, lasts };
                    Tuple<int, string> identifier = new Tuple<int, string>(node.Identifier, node.Value);
                    Tuple<List<int>[], bool> data = new Tuple<List<int>[], bool>(values, nullable);
                    FirstLastTable.Add(identifier, data);
                }
            }
        }

        /// <summary>Transverse the tree and create the follows table</summary>
        /// <param name="node">The current node</param>
        private void CreateFollowsTable(Node node)
        {
            if (node != null)
            {
                CreateFollowsTable(node.LeftChild);
                CreateFollowsTable(node.RightChild);

                if (node.LeftChild != null || node.RightChild != null)
                {
                    if (node.Value.Equals("·"))
                    {
                        Tuple<int, string> key = new Tuple<int, string>(node.LeftChild.Identifier, node.LeftChild.Value);
                        Tuple<List<int>[], bool> c1 = FirstLastTable[key];
                        key = new Tuple<int, string>(node.RightChild.Identifier, node.RightChild.Value);
                        Tuple<List<int>[], bool> c2 = FirstLastTable[key];
                        foreach (int last in c1.Item1[1])
                        {
                            Tuple<int, string> insert = FollowsTable.FirstOrDefault(x => x.Key.Item1 == last).Key;
                            FollowsTable[insert].AddRange(c2.Item1[0]);
                        }
                    }
                    else if (node.Value.Equals("*") || node.Value.Equals("+"))
                    {
                        Tuple<int, string> key = new Tuple<int, string>(node.LeftChild.Identifier, node.LeftChild.Value);
                        Tuple<List<int>[], bool> c1 = FirstLastTable[key];
                        foreach (int last in c1.Item1[1])
                        {
                            Tuple<int, string> insert = FollowsTable.FirstOrDefault(x => x.Key.Item1 == last).Key;
                            FollowsTable[insert].AddRange(c1.Item1[0]);
                        }
                    }
                }
            }
        }

        /// <summary>Create the transitions</summary>
        private void CreateTransitions()
        {
            int cont = 0;
            List<int> initialStateValue = FirstLastTable.FirstOrDefault(x => x.Key.Item1 == Tree.Identifier).Value
                                                        .Item1[0];
            Tuple<string, List<int>> initialState = new Tuple<string, List<int>>("State 0", initialStateValue);
            Queue<Tuple<string, List<int>>> pendingStates = new Queue<Tuple<string, List<int>>>();
            pendingStates.Enqueue(initialState);

            while (pendingStates.Count > 0)
            {
                Dictionary<string, List<int>> values = new Dictionary<string, List<int>>();
                Tuple<string, List<int>> state = pendingStates.Peek();
                foreach (int number in state.Item2)
                {
                    Tuple<int, string> followKey = FollowsTable.FirstOrDefault(x => x.Key.Item1 == number).Key;
                    string transitionWith = followKey.Item2;
                    List<int> transitionValues = FollowsTable[followKey];

                    if (transitionValues.Count > 0)
                    {
                        if (values.ContainsKey(transitionWith))
                        {
                            foreach (int element in transitionValues)
                            {
                                if (!values[transitionWith].Contains(element))
                                {
                                    values[transitionWith].Add(element);
                                }
                            }
                        }
                        else
                        {
                            values.Add(transitionWith, transitionValues);
                        }

                        if (!Transitions.Any(x => x.Key.Item2.SequenceEqual(transitionValues))
                            && !pendingStates.Any(x => x.Item2.SequenceEqual(transitionValues)))
                        {
                            cont++;
                            Tuple<string, List<int>> newState = new Tuple<string, List<int>>("State " + cont, transitionValues);
                            pendingStates.Enqueue(newState);
                        }
                    }
                }
                pendingStates.Dequeue();
                Transitions.Add(state, values);
            }
        }
    }
}