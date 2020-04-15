namespace IoC
{
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an abstract builder for an instance.
    /// </summary>
    public interface IBuilder
    {
        /// <summary>
        /// Builds the expression based on a build context.
        /// </summary>
        /// <param name="context">Current build context.</param>
        /// <param name="bodyExpression">The expression body to build an instance resolver.</param>
        /// <returns>The new expression.</returns>
        [NotNull] Expression Build([NotNull] IBuildContext context, [NotNull] Expression bodyExpression);
    }
}
