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
        /// <param name="bodyExpression">The expression body to get an instance.</param>
        /// <param name="buildContext">The build context.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] Expression bodyExpression, [NotNull] IBuildContext buildContext);
    }
}
