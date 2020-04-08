using System;
using System.Collections.Generic;

namespace RegularExpression
{
    public class Regex
    {
        /// <summary>Attributes of the class</summary>
        public string Expression { get; private set; }
        public Node Tree { get; private set; }

        private readonly Tokenizer tokenizer = new Tokenizer();
        private readonly Utils utils = new Utils();

        /// <summary>Constructor</summary>
        /// <param name="regex">The regular expression to use</param>
        public Regex(string regex)
        {
            Expression = regex;
            CreateTree(regex);
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

        /// <summary>Creates the abstract syntax tree for the analysis</summary>
        /// <param name="regex">The regular expression for the tree</param>
        private void CreateTree(string regex)
        {
            try
            {
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
                        S.Push(new Node(token));
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
                                Node temp = new Node(T.Pop());
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
                            Node temp = new Node(token);
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
                                Node temp = new Node(T.Pop());
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
                        Node temp = new Node(T.Pop());
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
    }
}