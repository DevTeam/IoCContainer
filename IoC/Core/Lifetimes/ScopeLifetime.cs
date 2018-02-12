namespace IoC.Core.Lifetimes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    internal sealed class ScopeLifetime: ILifetime, IDisposable
    {
        private readonly Dictionary<object, SingletoneLifetime> _lifetimes = new Dictionary<object, SingletoneLifetime>();

        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            SingletoneLifetime lifetime;
            var scopeKey = Scope.Current.ScopeKey;
            lock (_lifetimes)
            {
                if (!_lifetimes.TryGetValue(scopeKey, out lifetime))
                {
                    lifetime = new SingletoneLifetime();
                    _lifetimes.Add(scopeKey, lifetime);
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
            return new ScopeLifetime();
        }

        public override string ToString()
        {
            return Lifetime.Scope.ToString();
        }
    }
}
