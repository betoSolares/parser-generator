namespace Regex
{
    class Node
    {
        public string Value { get; private set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
        public Node Parent { get; private set; }

        /// <summary>Constructor</summary>
        /// <param name="value">The new value</param>
        public Node(string value)
        {
            Value = value;
            LeftChild = null;
            RightChild = null;
        }

        /// <summary>Constructor</summary>
        /// <param name="value">The new value</param>
        /// <param name="parent">The parent node</param>
        public Node(string value, Node parent)
        {
            Value = value;
            LeftChild = null;
            RightChild = null;
            Parent = parent;
        }
    }
}