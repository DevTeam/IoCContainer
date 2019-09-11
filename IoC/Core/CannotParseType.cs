namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotParseType : ICannotParseType
    {
        public static readonly ICannotParseType Shared = new CannotParseType();

        private CannotParseType() { }

        public Type Resolve(string statementText, int statementLineNumber, int statementPosition, string typeName)
        {
            throw new InvalidOperationException($"Cannot parse the type {typeName} in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }
    }
}