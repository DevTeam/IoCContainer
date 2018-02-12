namespace IoC.Core.Lifetimes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    internal sealed class ContainerLifetime: ILifetime, IDisposable
    {
        private readonly Dictionary<IContainer, SingletoneLifetime> _lifetimes = new Dictionary<IContainer, SingletoneLifetime>();

        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            SingletoneLifetime lifetime;
            lock (_lifetimes)
            {
                if (!_lifetimes.TryGetValue(container, out lifetime))
                {
                    lifetime = new SingletoneLifetime();
                    _lifetimes.Add(container, lifetime);
                }
            }

            return lifetime.GetOrCreate(container, args, resolver);
        }

        public void Dispose()
        {
            var items = _lifetimes.Values.ToList();
            _lifetimes.Clear();
            Disposable.Create(items).Dispose();
        }

        public ILifetime Clone()
        {
            return new ContainerLifetime();
        }

        public override string ToString()
        {
            return Lifetime.Container.ToString();
        }
    }
}
