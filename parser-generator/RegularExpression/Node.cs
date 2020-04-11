namespace RegularExpression
{
    public class Node
    {
        /// <summary>Attributes of the class</summary>
        public string Value { get; private set; }
        public int Identifier { get; private set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
        public Node Parent { get; set; }

        /// <summary>Constructor</summary>
        /// <param name="value">The new value</param>
        /// <param name="identifier">The id for the node</param>
        public Node(string value, int identifier)
        {
            Value = value;
            Identifier = identifier;
            LeftChild = null;
            RightChild = null;
        }
    }
}