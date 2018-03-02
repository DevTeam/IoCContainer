namespace IoC.Lifetimes
{
    using System;
    using System.Linq;
    using Core;
    using Core.Collections;

    /// <summary>
    /// Represents the abstaction for singleton based lifetimes.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    [PublicAPI]
    public abstract class SingletonBasedLifetime<TKey>: ILifetime, IDisposable
    {
        private readonly Func<ILifetime> _singletonLifetimeFactory;
        private Table<TKey, ILifetime> _lifetimes = Table<TKey, ILifetime>.Empty;

        /// <inheritdoc />
        protected SingletonBasedLifetime(Func<ILifetime> singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        /// <inheritdoc />
        public abstract ILifetime Clone();

        /// <summary>
        /// Creates key for singleton.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="args">The arfuments.</param>
        /// <returns>The created key.</returns>
        protected abstract TKey CreateKey(IContainer container, object[] args);
    }
}
