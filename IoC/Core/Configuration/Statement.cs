namespace IoC.Core.Configuration
{
    using System;

    internal struct Statement
    {
        [NotNull] public readonly string Text;
        public readonly int LineNumber;
        public readonly int Position;

        public Statement([NotNull] string text, int lineNumber, int position)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            if (lineNumber < 0) throw new ArgumentOutOfRangeException(nameof(lineNumber));
            if (position < 0) throw new ArgumentOutOfRangeException(nameof(position));
            LineNumber = lineNumber;
            Position = position;
        }
    }
}
