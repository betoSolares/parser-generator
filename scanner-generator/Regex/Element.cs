namespace Regex
{
    class Element
    {
        public char Value { get; private set; }
        public int Column { get; private set; }
        public int Row { get; private set; }

        /// <summary>Constructor</summary>
        /// <param name="value">The character to evaluate</param>
        /// <param name="column">The column of the character</param>
        /// <param name="row">The row of the character</param>
        public Element(char value, int column, int row)
        {
            Value = value;
            Column = column;
            Row = row;
        }
    }
}