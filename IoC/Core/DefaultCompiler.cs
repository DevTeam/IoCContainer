namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal sealed class DefaultCompiler : ICompiler
    {
        public static readonly ICompiler Shared = new DefaultCompiler();

        private DefaultCompiler() { }

        public bool TryCompile(IBuildContext context, LambdaExpression expression, out Delegate resolver, out Exception error)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            try
            {
                resolver = expression.Compile();
                error = default(Exception);
                return true;
            }
            catch (Exception ex)
            {
                error = ex;
                resolver = default(Delegate);
                return false;
            }
        }
    }
}