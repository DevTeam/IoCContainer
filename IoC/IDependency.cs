namespace IoC
{
    using System;
    using System.Linq.Expressions;
    using Extensibility;

    /// <summary>
    /// Represents a IoC dependency.
    /// </summary>
    [PublicAPI]
    public interface IDependency
    {
        /// <summary>
        /// Builds an expression.
        /// </summary>
        /// <param name="buildContext">The build context,</param>
        /// <param name="lifetime">The target lifetime,</param>
        /// <param name="baseExpression">The resulting expression.</param>
        /// <param name="error">The error.</param>
        /// <returns>True if success.</returns>
        bool TryBuildExpression([NotNull] IBuildContext buildContext, [CanBeNull] ILifetime lifetime, out Expression baseExpression, out Exception error);
    }
}
