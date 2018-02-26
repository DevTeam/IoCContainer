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
        /// <param name="key">The key.</param>
        /// <param name="container">The resolving container.</param>
        /// <param name="context">The expression build context.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] Expression expression, Key key, [NotNull] IContainer container, [CanBeNull] TContext context = default(TContext));
    }
}
