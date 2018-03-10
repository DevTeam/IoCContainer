namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;
    using Extensibility;

    internal class ExpressionCompilerDefault : IExpressionCompiler
    {
        public static readonly IExpressionCompiler Shared = new ExpressionCompilerDefault();

        private ExpressionCompilerDefault()
        {
        }

        public bool IsSupportingCompextTypeConstant => true;

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