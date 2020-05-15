namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal sealed class DefaultCompiler : ICompiler
    {
        public static readonly ICompiler Shared = new DefaultCompiler();

        private DefaultCompiler() { }

        public bool TryCompile(IBuildContext context, LambdaExpression lambdaExpression, out Delegate lambdaCompiled, out Exception error)
        {
            if (lambdaExpression == null) throw new ArgumentNullException(nameof(lambdaExpression));
            try
            {
                lambdaCompiled = lambdaExpression.Compile();
                error = default(Exception);
                return true;
            }
            catch (Exception ex)
            {
                error = ex;
                lambdaCompiled = default(Delegate);
                return false;
            }
        }
    }
}