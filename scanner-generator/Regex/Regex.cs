using System;
using System.Collections.Generic;

namespace Regex
{
    class Regex
    {
        public string Expression { get; private set; }
        public Node Tree { get; private set; }

        /// <summary>Constructor</summary>
        /// <param name="regex">The regular expression to use</param>
        public Regex(string regex)
        {
            Expression = regex;
            CreateTree(regex);
        }

        /// <summary>Creates the abstract syntax tree for the analysis</summary>
        /// <param name="regex">The regular expression for the tree</param>
        private void CreateTree(string regex)
        {
            try
            {
                List<string> tokens = new Helpers().TokenizeExpression(regex);
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
                                Node temp = new Node(T.Pop())
                                {
                                    RightChild = S.Pop(),
                                    LeftChild = S.Pop()
                                };
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
                                temp.LeftChild = S.Pop();
                                S.Push(temp);
                            }
                        }
                        else if (T.Count > 0 && !T.Peek().Equals("(") && new Helpers().CheckPrecedence(token, T.Peek(), opertors))
                        {
                            if (S.Count >= 2)
                            {
                                Node temp = new Node(T.Pop())
                                {
                                    RightChild = S.Pop(),
                                    LeftChild = S.Pop()
                                };
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
                        Node temp = new Node(T.Pop())
                        {
                            RightChild = S.Pop(),
                            LeftChild = S.Pop()
                        };
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