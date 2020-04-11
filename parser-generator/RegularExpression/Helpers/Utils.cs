using System;
using System.Collections.Generic;

namespace RegularExpression
{
    public class Utils
    {
        /// <summary>Check the precedence of the token</summary>
        /// <param name="token">The token to check the preference</param>
        /// <param name="lastOperator">The operator to compare</param>
        /// <param name="operators">The list of operators</param>
        /// <returns>True if the token precedence is less or equals than the operator precedence</returns>
        public bool CheckPrecedence(string token, string lastOperator, List<string> operators)
        {
            int operatorIndex = operators.FindIndex(x => x.Equals(lastOperator));
            int tokenIndex = operators.FindIndex(x => x.Equals(token));
            return tokenIndex >= operatorIndex;
        }

        /// <summary>Create the dictionary for the follows table</summary>
        /// <param name="node">The root node of the tree</param>
        /// <returns>A dictionary with all the keys</returns>
        public Dictionary<Tuple<int, string>, List<int>> CreateDictionary(Node node)
        {
            List<Tuple<int, string>> list = new List<Tuple<int, string>>();
            PopulateDictionary(node, ref list);
            Dictionary<Tuple<int, string>, List<int>> dictionary = new Dictionary<Tuple<int, string>, List<int>>();
            foreach(Tuple<int, string> element in list)
            {
                dictionary.Add(element, new List<int>());
            }
            return dictionary;
        }

        /// <summary>Get all the terminals symbols on the tree</summary>
        /// <param name="node">The node of the tree</param>
        /// <param name="list">The list with all the terminals symbols</param>
        private void PopulateDictionary(Node node, ref List<Tuple<int, string>> list)
        {
            if (node != null)
            {
                PopulateDictionary(node.LeftChild, ref list);
                PopulateDictionary(node.RightChild, ref list);

                if (node.LeftChild == null && node.RightChild == null)
                {
                    Tuple<int, string> identifier = new Tuple<int, string>(node.Identifier, node.Value);
                    list.Add(identifier);
                }
            }
        }
    }
}