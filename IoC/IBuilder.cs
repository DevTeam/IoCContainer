namespace IoC
{
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a builder for an instance.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Builds the expression.
        /// </summary>
        /// <param name="context">Current context for building.</param>
        /// <param name="bodyExpression">The expression body to get an instance.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] IBuildContext context, [NotNull] Expression bodyExpression);
    }
}
