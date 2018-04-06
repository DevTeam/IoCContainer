namespace IoC.Extensibility
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a expression compiler.
    /// </summary>
    [PublicAPI]
    public interface IExpressionCompiler
    {
        /// <summary>
        /// True if a refernce constant is supported.
        /// </summary>
        bool IsReferenceConstantSupported { get; }

        /// <summary>
        /// Compiles an expression to a delegate.
        /// </summary>
        /// <param name="resolverExpression">The lambda expression.</param>
        /// <returns>The resulting delegate.</returns>
        [NotNull] Delegate Compile([NotNull] LambdaExpression resolverExpression);
    }
}
