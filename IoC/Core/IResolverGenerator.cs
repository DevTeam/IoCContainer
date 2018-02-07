namespace IoC.Core
{
    internal interface IResolverGenerator
    {
        IResolverHolder<T> Generate<T>([NotNull] Key key, [NotNull] IContainer container, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime = null);
    }
}