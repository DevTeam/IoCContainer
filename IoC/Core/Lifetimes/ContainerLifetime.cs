namespace IoC.Core.Lifetimes
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using Core;

    internal sealed class ContainerLifetime: ILifetime, IDisposable
    {
        private readonly ConcurrentDictionary<IContainer, SingletoneLifetime> _lifetimes = new ConcurrentDictionary<IContainer, SingletoneLifetime>();

        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            var lifetime = _lifetimes.GetOrAdd(container, new SingletoneLifetime());
            return lifetime.GetOrCreate(container, args, resolver);
        }

        public void Dispose()
        {
            var items = _lifetimes.Values.ToList();
            _lifetimes.Clear();
            Disposable.Create(items).Dispose();
        }

        public override string ToString()
        {
            return Lifetime.Container.ToString();
        }
    }
}
