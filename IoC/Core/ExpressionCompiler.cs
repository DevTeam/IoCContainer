namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using Extensibility;

    internal class ExpressionCompiler : IExpressionCompiler
    {
        public static readonly IExpressionCompiler Shared = new ExpressionCompiler();

        private ExpressionCompiler() { }

        public Delegate Compile(LambdaExpression resolverExpression)
        {
            if (resolverExpression == null) throw new ArgumentNullException(nameof(resolverExpression));
            if (resolverExpression.CanReduce)
            {
                resolverExpression = (LambdaExpression)resolverExpression.Reduce();
            }

            return resolverExpression.Compile();
        }
    }
}