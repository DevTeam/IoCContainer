namespace IoC.Core
{
    using System;
    using System.Linq.Expressions;

    internal interface IExpressionCompiler
    {
        [NotNull] Delegate Compile([NotNull] LambdaExpression resolverExpression);
    }
}
