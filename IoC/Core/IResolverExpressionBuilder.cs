namespace IoC.Core
{
    using System.Linq.Expressions;

    internal interface IResolverExpressionBuilder
    {
        bool TryBuild<T>(Key key, [NotNull] IContainer container, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out Expression<Resolver<T>> resolverExpression);
    }
}