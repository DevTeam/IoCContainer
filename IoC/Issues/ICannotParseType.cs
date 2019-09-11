namespace IoC.Issues
{
    using System;

    /// <summary>
    /// Resolves the scenario when cannot parse a type from a text.
    /// </summary>
    [PublicAPI]
    public interface ICannotParseType
    {
        /// <summary>
        /// Resolves the scenario when cannot parse a type from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a type metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="typeName">The text with a type metadata.</param>
        /// <returns></returns>
        [NotNull] Type Resolve([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string typeName);
    }
}
