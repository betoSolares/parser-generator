namespace RegularExpression
{
    public class Node
    {
        /// <summary>Attributes of the class</summary>
        public string Value { get; private set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
        public Node Parent { get; set; }

        /// <summary>Constructor</summary>
        /// <param name="value">The new value</param>
        public Node(string value)
        {
            Value = value;
            LeftChild = null;
            RightChild = null;
        }
    }
}