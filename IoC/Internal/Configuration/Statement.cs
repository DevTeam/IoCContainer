namespace IoC.Internal.Configuration
{
    internal struct Statement
    {
        public readonly string Text;
        public readonly int LineNumber;
        public readonly int Position;

        public Statement(string text, int lineNumber, int position)
        {
            Text = text;
            LineNumber = lineNumber;
            Position = position;
        }
    }
}
