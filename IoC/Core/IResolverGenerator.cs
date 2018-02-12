namespace IoC.Core
{
    internal interface IResolverGenerator
    {
        bool TryGenerate<T>([NotNull] Key key, [NotNull] IContainer container, [NotNull] IDependency dependency, [CanBeNull] ILifetime lifetime, out IResolverHolder<T> resolverHolder);
    }
}