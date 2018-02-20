namespace IoC.Extensibility
{
    using System.Linq.Expressions;

    /// <summary>
    /// Allows to build expresion for lifetimes.
    /// </summary>
    [PublicAPI]
    public interface IExpressionBuilder
    {
        /// <summary>
        /// Builds the expression.
        /// </summary>
        /// <param name="expression">The base expression to get an instance.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([CanBeNull] Expression expression);
    }
}
