namespace IoC.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    [DebuggerDisplay("{" + nameof(ToString) + "()}")]
    internal sealed class Scope: IScope, IContainer
    {
        private static long _currentScopeKey;
        internal readonly long ScopeKey;
        private readonly int _scopeHashCode;
        [NotNull] private readonly IScopeManager _scopeManager;
        [NotNull] private readonly ILockObject _lockObject;
        private readonly IContainer _container;
        private readonly IList<IDisposable> _resources = new List<IDisposable>();
        private readonly bool _isDefault;

        public Scope([NotNull] IScopeManager scopeManager, [NotNull] ILockObject lockObject, IContainer container, bool isDefault = false)
        {
            ScopeKey = Interlocked.Increment(ref _currentScopeKey);
            _scopeHashCode = ScopeKey.GetHashCode();
            _scopeManager = scopeManager;
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(lockObject));
            _container = container;
            _isDefault = isDefault;
        }

        public IContainer Container => this;

        public IDisposable Activate() => _scopeManager.Activate(this);

        public void Dispose()
        {
            lock (_lockObject)
            {
                foreach (var disposable in _resources)
                {
                    disposable.Dispose();
                }

                _resources.Clear();
            }
        }

        public IEnumerator<IEnumerable<Key>> GetEnumerator() => _container.GetEnumerator();

        public IDisposable Subscribe(IObserver<ContainerEvent> observer) => _container.GetEnumerator();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            // ReSharper disable once PossibleNullReferenceException
            if (obj.GetType() != typeof(Scope)) return false;
            return ScopeKey == ((Scope)obj).ScopeKey;
        }

        public override int GetHashCode() => _scopeHashCode;

        public override string ToString() => $"Scope #{ScopeKey}";

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void RegisterResource(IDisposable resource)
        {
            lock (_lockObject)
            {
                _resources.Add(resource);
            }
        }

        public bool UnregisterResource(IDisposable resource)
        {
            lock (_lockObject)
            {
                return _resources.Remove(resource);
            }
        }

        public IContainer Parent => _container.Parent;

        public bool TryGetDependency(Key key, out IDependency dependency, out ILifetime lifetime) => 
            _container.TryGetDependency(key, out dependency, out lifetime);

        public bool TryGetResolver<T>(Type type, object tag, out Resolver<T> resolver, out Exception error, IContainer resolvingContainer = null) => 
            _container.TryGetResolver(type, tag, out resolver, out error);
    }
}