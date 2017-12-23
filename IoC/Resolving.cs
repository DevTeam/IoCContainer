namespace IoC
{
    using System;
    using System.Collections.Generic;
    using Internal;

    [PublicAPI]
    public struct Resolving: IContainer, IInstanceStore
    {
        [NotNull] internal readonly IContainer Container;
        [CanBeNull] internal readonly object Tag;
        private IInstanceStore _instanceStore;

        public Resolving([NotNull] IContainer container, [CanBeNull] object tag)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _instanceStore = Container as IInstanceStore;
            Tag = tag;
        }

        public IContainer Parent => Container.Parent;

        IDictionary<IInstanceKey, object> IInstanceStore.GetInstances()
        {
            return ((IInstanceStore) Container).GetInstances();
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
