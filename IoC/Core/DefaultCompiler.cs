namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal class DefaultCompiler : ICompiler
    {
        public static readonly ICompiler Shared = new DefaultCompiler();

        private DefaultCompiler() { }

        public bool TryCompile(IBuildContext context, LambdaExpression expression, out Delegate resolver)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (expression.CanReduce)
            {
                expression = (LambdaExpression)expression.Reduce();
            }

            resolver = expression.Compile();
            return true;
        }
    }
}