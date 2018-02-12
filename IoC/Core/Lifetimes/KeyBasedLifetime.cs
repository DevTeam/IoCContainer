namespace IoC.Core.Lifetimes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    internal abstract class KeyBasedLifetime<TKey>: ILifetime, IDisposable
    {
        private readonly Func<ILifetime> _singletonLifetimeFactory;
        private readonly Dictionary<TKey, ILifetime> _lifetimes = new Dictionary<TKey, ILifetime>();

        protected KeyBasedLifetime(Func<ILifetime> singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory;
        }

        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            ILifetime lifetime;
            var key = CreateKey(container, args);
            lock (_lifetimes)
            {
                if (!_lifetimes.TryGetValue(key, out lifetime))
                {
                    lifetime = _singletonLifetimeFactory();
                    _lifetimes.Add(key, lifetime);
                }
            }

            return lifetime.GetOrCreate(container, args, resolver);
        }

        public void Dispose()
        {
            List<ILifetime> items;
            lock (_lifetimes)
            { 
                items = _lifetimes.Values.ToList();
                _lifetimes.Clear();
            }

            Disposable.Create(items.OfType<IDisposable>()).Dispose();
        }

        public abstract ILifetime Clone();
        
        protected abstract TKey CreateKey(IContainer container, object[] args);
    }
}
