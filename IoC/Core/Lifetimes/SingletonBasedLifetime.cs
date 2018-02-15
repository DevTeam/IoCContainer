namespace IoC.Core.Lifetimes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Collections;
    using Core;

    internal abstract class SingletonBasedLifetime<TKey>: ILifetime, IDisposable
    {
        private readonly Func<ILifetime> _singletonLifetimeFactory;
        private HashTable<TKey, ILifetime> _lifetimes = HashTable<TKey, ILifetime>.Empty;

        protected SingletonBasedLifetime(Func<ILifetime> singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory;
        }

        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            ILifetime lifetime;
            var key = CreateKey(container, args);
            lock (_lifetimes)
            {
                lifetime = _lifetimes.Find(key);
                if (lifetime == null)
                {
                    lifetime = _singletonLifetimeFactory();
                    _lifetimes = _lifetimes.Add(key, lifetime);
                }
            }

            return lifetime.GetOrCreate(container, args, resolver);
        }

        public void Dispose()
        {
            System.Collections.Generic.List<ILifetime> items;
            lock (_lifetimes)
            { 
                items = _lifetimes.Enumerate().Select(i => i.Value).ToList();
                _lifetimes = HashTable<TKey, ILifetime>.Empty;
            }

            Disposable.Create(items.OfType<IDisposable>()).Dispose();
        }

        public abstract ILifetime Clone();
        
        protected abstract TKey CreateKey(IContainer container, object[] args);
    }
}
