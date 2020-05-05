namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal sealed class DefaultCompiler : ICompiler
    {
        public static readonly ICompiler Shared = new DefaultCompiler();

        private DefaultCompiler() { }

        public bool TryCompileResolver<T>(IBuildContext context, LambdaExpression expression, out Resolver<T> resolver)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            resolver = (Resolver<T>)expression.Compile();
            return true;
        }
    }
}