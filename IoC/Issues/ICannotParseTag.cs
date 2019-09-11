namespace IoC.Issues
{
    /// <summary>
    /// Resolves the scenario when cannot parse a tag from a text.
    /// </summary>
    [PublicAPI]
    public interface ICannotParseTag
    {
        /// <summary>
        /// Resolves the scenario when cannot parse a tag from a text.
        /// </summary>
        /// <param name="statementText">The statement containing a tag metadata.</param>
        /// <param name="statementLineNumber">The line number in the source data.</param>
        /// <param name="statementPosition">The position at the line of the source data.</param>
        /// <param name="tag">The text with a tag metadata.</param>
        /// <returns></returns>
        [CanBeNull] object Resolve(string statementText, int statementLineNumber, int statementPosition, [NotNull] string tag);
    }
}
