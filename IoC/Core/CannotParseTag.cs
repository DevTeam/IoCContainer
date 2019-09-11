namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotParseTag : ICannotParseTag
    {
        public static readonly ICannotParseTag Shared = new CannotParseTag();

        private CannotParseTag() { }

        public object Resolve(string statementText, int statementLineNumber, int statementPosition, string tag)
        {
            throw new InvalidOperationException($"Cannot parse the tag {tag} in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }
    }
}