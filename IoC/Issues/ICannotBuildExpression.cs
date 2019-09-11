namespace IoC.Issues
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Resolves the scenario when cannot build expression.
    /// </summary>
    [PublicAPI]
    public interface ICannotBuildExpression
    {
        /// <summary>
        /// Resolves the scenario when cannot build expression.
        /// </summary>
        /// <param name="buildContext">The build context.</param>
        /// <param name="dependency">The dependency.</param>
        /// <param name="lifetime">The lifetime.</param>
        /// <param name="error">The error.</param>
        /// <returns>The resulting expression.</returns>
        [NotNull] Expression Resolve([NotNull] IBuildContext buildContext, [NotNull] IDependency dependency, ILifetime lifetime, Exception error);
    }
}
