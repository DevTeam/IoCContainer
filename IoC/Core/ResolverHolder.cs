namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal struct ResolverHolder<T>: IDisposable
    {
        [NotNull] public readonly Resolver<T> Resolve;
        [NotNull] public readonly ISet<Key> Dependencies;
        [NotNull] private readonly IDisposable _resource;

        public ResolverHolder(
            [NotNull] Resolver<T> resolver, 
            [NotNull] IDisposable resource,
            [NotNull] IEnumerable<Key> dependencies)
        {
            Resolve = resolver ?? throw new ArgumentNullException(nameof(resolver));
            _resource = resource ?? throw new ArgumentNullException(nameof(resource));
            if (dependencies == null) throw new ArgumentNullException(nameof(dependencies));
            Dependencies = new HashSet<Key>(dependencies);
        }

        public void Dispose()
        {
            _resource.Dispose();
        }
    }
}
