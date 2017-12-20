namespace IoC
{
    using System;
    using System.Collections.Generic;

    [PublicAPI]
    public struct Resolving: IContainer
    {
        [NotNull] private readonly IContainer _container;
        [CanBeNull] internal readonly object Tag;

        public Resolving([NotNull] IContainer container, [CanBeNull] object tag)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            Tag = tag;
        }

        public IDisposable Register(IEnumerable<Key> keys, IFactory factory, ILifetime lifetime = null)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return _container.Register(keys, factory, lifetime);
        }

        public bool TryGetResolver(Key key, out IResolver resolver)
        {
            return _container.TryGetResolver(key, out resolver);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
