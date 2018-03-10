namespace IoC.Extensibility
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a expression compiler.
    /// </summary>
    public interface IExpressionCompiler
    {
        /// <summary>
        /// True if supports a compext type constant.
        /// </summary>
        bool IsSupportingCompextTypeConstant { get; }

        /// <summary>
        /// Compiles an expression to a delegate.
        /// </summary>
        /// <param name="resolverExpression">The lambda expression.</param>
        /// <returns>The resulting delegate.</returns>
        [NotNull] Delegate Compile([NotNull] LambdaExpression resolverExpression);
    }
}
