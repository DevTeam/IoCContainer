namespace IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an abstract IoC dependency.
    /// </summary>
    [PublicAPI]
    public interface IDependency
    {
        /// <summary>
        /// Builds an expression for dependency based on the current build context and specified lifetime.
        /// </summary>
        /// <param name="buildContext">The build context.</param>
        /// <param name="lifetime">The target lifetime.</param>
        /// <param name="expression">The resulting expression for the current dependency.</param>
        /// <param name="error">The error if something goes wrong.</param>
        /// <returns><c>True</c> if successful and an expression was provided.</returns>
        bool TryBuildExpression([NotNull] IBuildContext buildContext, [CanBeNull] ILifetime lifetime, out Expression expression, out Exception error);
    }
}
