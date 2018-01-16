namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal sealed class RegistrationEntry : IDisposable
    {
        [NotNull] private static readonly ISet<Key> EmptyDependencies = new HashSet<Key>();
        [NotNull] private readonly IResolverGenerator _resolverGenerator;
        [NotNull] internal readonly IDependency Dependency;
        [CanBeNull] private readonly ILifetime _lifetime;
        [NotNull] private readonly IDisposable _resource;
        [CanBeNull] private object _resolver;
        [NotNull] private ISet<Key> _dependencies = EmptyDependencies;
        [CanBeNull] private IDisposable _holder;

        public RegistrationEntry(
            [NotNull] IResolverGenerator resolverGenerator,
            [NotNull] IDependency dependency,
            [CanBeNull] ILifetime lifetime,
            [NotNull] IDisposable resource)
        {
            _resolverGenerator = resolverGenerator ?? throw new ArgumentNullException(nameof(resolverGenerator));
            Dependency = dependency ?? throw new ArgumentNullException(nameof(dependency));
            _lifetime = lifetime;
            _resource = resource ?? throw new ArgumentNullException(nameof(resource));
        }

        public ISet<Key> Dependencies => _dependencies;

        public Resolver<T> CreateResolver<T>(Key key, [NotNull] IContainer container)
        {
            if (_resolver != null)
            {
                return (Resolver<T>) _resolver;
            }

            var holder = _resolverGenerator.Generate<T>(key, container, Dependency, _lifetime);
            _resolver = holder.Resolve;
            _dependencies = holder.Dependencies;
            _holder = holder;
            return holder.Resolve;
        }

        public void Reset()
        {
            _resolver = null;
            _dependencies = EmptyDependencies;
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
