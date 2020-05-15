namespace IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an abstract expression compiler.
    /// </summary>
    [PublicAPI]
    public interface ICompiler
    {
        /// <summary>
        /// Compiles a lambda expression to delegate.
        /// </summary>
        /// <param name="context">Current context for building.</param>
        /// <param name="lambdaExpression">The lambda expression to compile.</param>
        /// <param name="lambdaCompiled">The compiled lambda.</param>
        /// <param name="error">Compilation error.</param>
        /// <returns>True if success.</returns>
        bool TryCompile([NotNull] IBuildContext context, [NotNull] LambdaExpression lambdaExpression, out Delegate lambdaCompiled, out Exception error);
    }
}
