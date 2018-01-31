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
        {
            throw new NotSupportedException();
        }

        public bool TryGetDependency(Key key, out IDependency dependency, out ILifetime lifetime)
        {
            dependency = default(IDependency);
            lifetime = default(ILifetime);
            return false;
        }

        public bool TryGetResolver<T>(Key key, out Resolver<T> resolver, IContainer container = null)
        {
            resolver = default(Resolver<T>);
            return false;
        }

        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, IContainer container = null)
        {
            resolver = default(Resolver<T>);
            return false;
        }

        public bool TryGet(Type type, object tag, out object instance, params object[] args)
        {
            instance = default(object);
            return false;
        }

        public bool TryGet<T>(object tag, out T instance, params object[] args)
        {
            instance = default(T);
            return false;
        }

        public void Dispose()
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<Key> GetEnumerator()
        {
            return Enumerable.Empty<Key>().GetEnumerator();
        }

        public IDisposable Subscribe(IObserver<ContainerEvent> observer)
        {
            return Disposable.Empty;
        }

        public override string ToString()
        {
            return string.Empty;
        }
    }
}