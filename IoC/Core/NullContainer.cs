namespace IoC.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class NullContainer : IContainer
    {
        public static readonly IContainer Shared = new NullContainer();
        private static readonly NotSupportedException NotSupportedException = new NotSupportedException();

        private NullContainer() { }

        public IContainer Parent => throw new NotSupportedException();

        public bool TryRegisterDependency(IEnumerable<Key> keys, IDependency dependency, ILifetime lifetime, out IDisposable dependencyToken)
            => throw NotSupportedException;

        public bool TryGetDependency(Key key, out IDependency dependency, out ILifetime lifetime)
        {
            dependency = default(IDependency);
            lifetime = default(ILifetime);
            return false;
        }

        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null)
        {
            resolver = default(Resolver<T>);
            error = NotSupportedException;
            return false;
        }


        public void RegisterResource(IDisposable resource) { }

        public void UnregisterResource(IDisposable resource) { }

        public void Dispose() { }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<IEnumerable<Key>> GetEnumerator() => Enumerable.Empty<IEnumerable<Key>>().GetEnumerator();

        public IDisposable Subscribe(IObserver<ContainerEvent> observer) => Disposable.Empty;

        public override string ToString() => string.Empty;
    }
}