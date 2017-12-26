namespace IoC.Internal
{
    using System;
    using System.Collections.Generic;

    internal struct Resolving: IContainer, IInstanceStore
    {
        [NotNull] internal readonly IContainer Container;
        [CanBeNull] internal readonly object Tag;
        private IInstanceStore _instanceStore;

        public Resolving([NotNull] IContainer container, [CanBeNull] object tag)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _instanceStore = Container as IInstanceStore ?? throw new ArgumentException($"The {nameof(container)} must implement the interface {typeof(IInstanceStore).Name}");
            Tag = tag;
        }

        public IContainer Parent => Container.Parent;

        public object GetOrAdd(IInstanceKey key, Context context, IFactory factory)
        {
            return _instanceStore.GetOrAdd(key, context, factory);
        }

        public bool TryRegister(IEnumerable<Key> keys, IFactory factory, ILifetime lifetime, out IDisposable registrationToken)
        {
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return Container.TryRegister(keys, factory, lifetime, out registrationToken);
        }

        public bool TryGetResolver(Key key, out IResolver resolver)
        {
            return Container.TryGetResolver(key, out resolver);
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        public override string ToString()
        {
            return Container.ToString();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            switch (obj)
            {
                case Resolving resolving:
                    return Equals(resolving.Container, Container);

                case IContainer container:
                    return Equals(container, Container);

                default:
                    return false;
            }
        }

        public override int GetHashCode()
        {
            return Container.GetHashCode();
        }
    }
}
