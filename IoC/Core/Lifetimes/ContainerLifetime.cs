namespace IoC.Core.Lifetimes
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;

    internal sealed class ContainerLifetime: ILifetime, IDisposable
    {
        private readonly ConcurrentDictionary<IContainer, ILifetime> _lifetimes = new ConcurrentDictionary<IContainer, ILifetime>();

        public T GetOrCreate<T>(Key key, IContainer container, object[] args, Resolver<T> resolver)
        {
            var lifetime = _lifetimes.GetOrAdd(container, container.Tag(Lifetime.Singletone).Get<ILifetime>());
            return lifetime.GetOrCreate(key, container, args, resolver);
        }

        public void Dispose()
        {
            var items = _lifetimes.Values.ToList();
            _lifetimes.Clear();
            Disposable.Create(items.OfType<IDisposable>()).Dispose();
        }
    }
}
