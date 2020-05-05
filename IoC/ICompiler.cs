namespace IoC
{
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an abstract expression compiler.
    /// </summary>
    [PublicAPI]
    public interface ICompiler
    {
        /// <summary>
        /// Compiles an expression to an instance resolver.
        /// </summary>
        /// <param name="context">Current context for building.</param>
        /// <param name="expression">The lambda expression to compile.</param>
        /// <param name="resolver">The compiled resolver delegate.</param>
        /// <returns>True if success.</returns>
        bool TryCompileResolver<T>([NotNull] IBuildContext context, [NotNull] LambdaExpression expression, out Resolver<T> resolver);
    }
}
