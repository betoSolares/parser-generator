using Regex;

namespace scanner_generator.Regex
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
    }
}