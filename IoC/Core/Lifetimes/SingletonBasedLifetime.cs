namespace IoC.Core.Lifetimes
{
    using System;
    using System.Linq;
    using Collections;
    using Core;

    internal abstract class SingletonBasedLifetime<TKey>: ILifetime, IDisposable
    {
        private readonly Func<ILifetime> _singletonLifetimeFactory;
        private Table<TKey, ILifetime> _lifetimes = Table<TKey, ILifetime>.Empty;

        protected SingletonBasedLifetime(Func<ILifetime> singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory;
        }

        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            ILifetime lifetime;
            var key = CreateKey(container, args);
            var hashCode = key.GetHashCode();
            lock (_lifetimes)
            {
                if (!_lifetimes.TryGet(hashCode, key, out lifetime))
                {
                    lifetime = _singletonLifetimeFactory();
                    _lifetimes = _lifetimes.Set(hashCode, key, lifetime);
                }
            }

            return lifetime.GetOrCreate(container, args, resolver);
        }

        public void Dispose()
        {
            System.Collections.Generic.List<ILifetime> items;
            lock (_lifetimes)
            { 
                items = _lifetimes.Select(i => i.Value).ToList();
                _lifetimes = Table<TKey, ILifetime>.Empty;
            }

            Disposable.Create(items.OfType<IDisposable>()).Dispose();
        }

        public abstract ILifetime Clone();
        
        protected abstract TKey CreateKey(IContainer container, object[] args);
    }
}
