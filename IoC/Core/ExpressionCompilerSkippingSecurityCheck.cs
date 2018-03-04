namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal class ExpressionCompilerSkippingSecurityCheck : IExpressionCompiler
    {
        public static readonly IExpressionCompiler Shared = new ExpressionCompilerSkippingSecurityCheck();

        private ExpressionCompilerSkippingSecurityCheck()
        {
        }

        public Delegate Compile(LambdaExpression resolverExpression)
        {
            if (resolverExpression == null) throw new ArgumentNullException(nameof(resolverExpression));
            return resolverExpression.Compile();
        }
    }
}