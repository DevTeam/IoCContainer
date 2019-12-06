namespace IoC
{
    using System;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents a expression compiler.
    /// </summary>
    [PublicAPI]
    public interface ICompiler
    {
        /// <summary>
        /// Compiles an expression to a delegate.
        /// </summary>
        /// <param name="context">Current context for building.</param>
        /// <param name="expression">The lambda expression to compile.</param>
        /// <param name="resolver">The compiled resolver delegate.</param>
        /// <returns>True if success.</returns>
        bool TryCompile([NotNull] IBuildContext context, [NotNull] LambdaExpression expression, out Delegate resolver);
    }
}
