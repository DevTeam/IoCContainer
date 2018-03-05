namespace IoC.Extensibility
{
    using System.Linq.Expressions;

    /// <summary>
    /// Allows to build expresion for lifetimes.
    /// </summary>
    [PublicAPI]
    public interface IExpressionBuilder<in TContext>
    {
        /// <summary>
        /// Builds the expression.
        /// </summary>
        /// <param name="expression">The base expression to get an instance.</param>
        /// <param name="buildContext">The build context.</param>
        /// <param name="context">The expression build context.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] Expression expression, [NotNull] BuildContext buildContext, [CanBeNull] TContext context = default(TContext));
    }
}
