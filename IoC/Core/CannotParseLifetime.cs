namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotParseLifetime : ICannotParseLifetime
    {
        public static readonly ICannotParseLifetime Shared = new CannotParseLifetime();

        private CannotParseLifetime() { }

        public Lifetime Resolve(string statementText, int statementLineNumber, int statementPosition, string lifetimeName) => 
            throw new InvalidOperationException($"Cannot parse the lifetime {lifetimeName} in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
    }
}