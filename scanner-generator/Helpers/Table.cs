using RegularExpression;
using System.Collections.Generic;

namespace Helpers
{
    public class Table
    {
        /// <summary>Create a string with the elements on the list</summary>
        /// <param name="list">The list with the elements</param>
        /// <returns>A string with each element separated by a comma</returns>
        public string GetList(List<int> list)
        {
            string text = string.Empty;
            foreach(int number in list)
            {
                text += number + ", ";
            }
            
            if (text.Length == 0)
            {
                return " --- ";
            }
            else
            {
                return text.Remove(text.Length - 2, 2);
            }
        }

        /// <summary>Get a list with all the terminals nodes</summary>
        /// <param name="node">The current node</param>
        /// <param name="list">The list with the terminal nodes</param>
        public void GetTerminals(Node node, ref List<string> list, int identifierTerminal)
        {
            if (node != null)
            {
                GetTerminals(node.LeftChild, ref list, identifierTerminal);
                GetTerminals(node.RightChild, ref list, identifierTerminal);

                if (node.LeftChild == null && node.RightChild == null)
                {
                    if (node.Identifier != identifierTerminal)
                    {
                        if (!list.Contains(node.Value))
                        {
                            list.Add(node.Value);
                        }
                    }
                }
            }
        }
    }
}
