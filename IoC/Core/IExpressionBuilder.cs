namespace IoC.Core
{
    using System.Linq.Expressions;

    /// <summary>
    /// Allows to build expresion for lifetimes.
    /// </summary>
    [PublicAPI]
    internal interface IExpressionBuilder<in TContext>
    {
        /// <summary>
        /// Builds the expression.
        /// </summary>
        /// <param name="bodyExpression">The expression body to get an instance.</param>
        /// <param name="buildContext">The build context.</param>
        /// <param name="context">The expression build context.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] Expression bodyExpression, [NotNull] IBuildContext buildContext, [CanBeNull] TContext context = default(TContext));
    }
}
