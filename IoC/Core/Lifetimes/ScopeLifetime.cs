namespace IoC.Core.Lifetimes
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq;
    using Core;

    internal sealed class ScopeLifetime: ILifetime, IDisposable
    {
        private readonly ConcurrentDictionary<object, SingletoneLifetime> _lifetimes = new ConcurrentDictionary<object, SingletoneLifetime>();

        public T GetOrCreate<T>(IContainer container, object[] args, Resolver<T> resolver)
        {
            var lifetime = _lifetimes.GetOrAdd(Scope.Current.ScopeKey, new SingletoneLifetime());
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
