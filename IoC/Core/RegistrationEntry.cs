namespace IoC.Core
{
    using System;

    internal sealed class RegistrationEntry : IDisposable
    {
        [NotNull] private readonly IResolverGenerator _resolverGenerator;
        [NotNull] internal readonly IDependency Dependency;
        [CanBeNull] internal readonly ILifetime Lifetime;
        [NotNull] private readonly IDisposable _resource;
        [CanBeNull] private object _resolver;
        [CanBeNull] private IDisposable _holder;

        public RegistrationEntry(
            [NotNull] IResolverGenerator resolverGenerator,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource)
        {
            _resolverGenerator = resolverGenerator ?? throw new ArgumentNullException(nameof(resolverGenerator));
            Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
            Lifetime = lifetime;
            _resource = resource ?? throw new ArgumentNullException(nameof(resource));
        }

        public Resolver<T> CreateResolver<T>(Key key, [NotNull] IContainer container)
        {
            if (_resolver != null)
            {
                return (Resolver<T>) _resolver;
            }

            var holder = _resolverGenerator.Generate<T>(key, container, Dependency, Lifetime);
            _resolver = holder.Resolve;
            _holder = holder;
            return holder.Resolve;
        }

        public void Reset()
        {
            _resolver = null;
            _holder?.Dispose();
            _holder = null;
        }

        public void Dispose()
        {
            Reset();
            _resource.Dispose();
        }
    }
}
