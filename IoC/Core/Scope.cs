namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    [DebuggerDisplay("{" + nameof(ToString) + "()} with {" + nameof(ResourceCount) + "} resources")]
    internal sealed class Scope: IScope
    {
        private static long _currentScopeKey;
        [NotNull] private static readonly Scope Default = new Scope(new LockObject(), true);
        [CanBeNull] [ThreadStatic] private static Scope _current;
        internal readonly long ScopeKey;
        private readonly int _scopeHashCode;
        [NotNull] private readonly ILockObject _lockObject;
        private readonly bool _isDefault;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [CanBeNull] private Scope _prevScope;

        [NotNull] internal static Scope Current => _current ?? Default;

        // For tests only
        internal Scope([NotNull] ILockObject lockObject, long key)
        {
            ScopeKey = key;
            _scopeHashCode = ScopeKey.GetHashCode();
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(lockObject));
        }

        public Scope([NotNull] ILockObject lockObject, bool isDefault = false)
        {
            ScopeKey = Interlocked.Increment(ref _currentScopeKey);
            _scopeHashCode = ScopeKey.GetHashCode();
            _lockObject = lockObject ?? throw new ArgumentNullException(nameof(lockObject));
            _isDefault = isDefault;
        }

        public IDisposable Activate()
        {
            if (ReferenceEquals(this, Current))
            {
                return Disposable.Empty;
            }

            _prevScope = Current;
            _current = this;
            return Disposable.Create(() => { _current = _prevScope; });
        }

        public void Dispose()
        {
            lock (_lockObject)
            {
                foreach (var resource in _resources)
                {
                    resource.Dispose();
                }

                _resources.Clear();
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            // ReSharper disable once PossibleNullReferenceException
            if (obj.GetType() != typeof(Scope)) return false;
            return ScopeKey == ((Scope)obj).ScopeKey;
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

        public override int GetHashCode() => _scopeHashCode;

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
