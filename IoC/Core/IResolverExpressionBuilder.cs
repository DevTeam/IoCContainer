namespace IoC.Core
{
    using System.Linq.Expressions;
    using Extensibility;

    internal interface IResolverExpressionBuilder
    {
        bool TryBuild([NotNull] BuildContext buildContext, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out LambdaExpression resolverExpression);
    }
}