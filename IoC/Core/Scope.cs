namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    [DebuggerDisplay("{" + nameof(ToString) + "()} with {" + nameof(ResourceCount) + "} resources")]
    internal sealed class Scope: IScope, IResourceRegistry
    {
        private static long _currentScopeKey;
        [NotNull] private static readonly Scope Default = new Scope(new LockObject());
        [CanBeNull] [ThreadStatic] private static Scope _current;
        internal readonly long ScopeKey;
        [NotNull] private readonly ILockObject _lockObject;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [CanBeNull] private Scope _prevScope;

        [NotNull] internal static Scope Current => _current ?? Default;

        public Scope([NotNull] ILockObject lockObject, long key)
        {
            ScopeKey = key;
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(lockObject));
        }

        public Scope([NotNull] ILockObject lockObject)
        {
            ScopeKey = Interlocked.Increment(ref _currentScopeKey);
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(lockObject));
        }

        public IDisposable Activate()
        {
            if (ReferenceEquals(this, Current))
            {
                return Disposable.Empty;
            }

            _prevScope = Current;
            _current = this;
            return Disposable.Create(() => { _current = _prevScope ?? throw new NotSupportedException(); });
        }

        public void Dispose()
        {
            List<IDisposable> resources;
            lock (_lockObject)
            {
                 resources = _resources.ToList();
                 _resources.Clear();
            }

            foreach (var resource in resources)
            {
                resource.Dispose();
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return ScopeKey.Equals(((Scope) obj).ScopeKey);
        }

        public void RegisterResource(IDisposable resource)
        {
            lock (_lockObject)
            {
                _resources.Add(resource ?? throw new ArgumentNullException(nameof(resource)));
            }
        }

        public bool UnregisterResource(IDisposable resource)
        {
            lock (_lockObject)
            {
                return _resources.Remove(resource ?? throw new ArgumentNullException(nameof(resource)));
            }
        }

        public override int GetHashCode() => ScopeKey.GetHashCode();

        public override string ToString() => $"#{ScopeKey} Scope";

        internal int ResourceCount
        {
            get
            {
                lock (_lockObject)
                {
                    return _resources.Count;
                }
            }
        }
    }
}
