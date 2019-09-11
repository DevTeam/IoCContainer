namespace IoC.Issues
{
    /// <summary>
    /// Resolves the scenario when cannot parse a lifetime from a text.
    /// </summary>
    [PublicAPI]
    public interface ICannotParseLifetime
    {
        /// <summary>
        /// Resolves the scenario when cannot parse a lifetime from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a lifetime metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="lifetimeName">The text with a lifetime metadata.</param>
        /// <returns></returns>
        Lifetime Resolve([NotNull] string statementText, int statementLineNumber, int statementPosition, [NotNull] string lifetimeName);
    }
}
