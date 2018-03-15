namespace IoC.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class NullContainer : IContainer
    {
        public IContainer Parent => throw new NotSupportedException();

        public bool TryRegister(IEnumerable<Key> keys, IDependency dependency, ILifetime lifetime, out IDisposable registrationToken)
            => throw new NotSupportedException();

        public bool TryGetDependency(Key key, out IDependency dependency, out ILifetime lifetime)
        {
            dependency = default(IDependency);
            lifetime = default(ILifetime);
            return false;
        }

        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, IContainer container = null)
        {
            resolver = default(Resolver<T>);
            return false;
        }

        public bool TryGetResolver<T>(Type type, out Resolver<T> resolver, IContainer container = null)
        {
            resolver = default(Resolver<T>);
            return false;
        }

        public void Dispose()
        {
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<IEnumerable<Key>> GetEnumerator() => Enumerable.Empty<IEnumerable<Key>>().GetEnumerator();

        public IDisposable Subscribe(IObserver<ContainerEvent> observer) => Disposable.Empty;

        public override string ToString() => string.Empty;
    }
}