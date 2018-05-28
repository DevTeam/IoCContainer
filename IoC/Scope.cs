namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;

    /// <summary>
    /// Represents the scope which could be used with <c>Lifetime.ScopeSingleton</c>
    /// </summary>
    [PublicAPI]
    public sealed class Scope: IDisposable
    {
        [NotNull] private static readonly Scope Default = new Scope(DefaultScopeKey.Shared);
        [CanBeNull] [ThreadStatic] private static Scope _current;
        [NotNull] internal readonly object ScopeKey;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();
        [CanBeNull] private Scope _prevScope;

        /// <summary>
        /// The current scope.
        /// </summary>
        [NotNull]
        public static Scope Current => _current ?? Default;

        /// <summary>
        /// Creates the instance of a new scope.
        /// </summary>
        /// <param name="scopeKey">The key of scope.</param>
        public Scope([NotNull] object scopeKey) => ScopeKey = scopeKey ?? throw new ArgumentNullException(nameof(scopeKey));

        /// <summary>
        /// Begins scope.
        /// </summary>
        /// <returns>The scope token to end the scope.</returns>
        public IDisposable Begin()
        {
            _prevScope = Current;
            _current = this;
            return Disposable.Create(() => { _current = _prevScope ?? throw new NotSupportedException(); });
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (var resource in _resources.ToList())
            {
                resource.Dispose();
            }
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return ScopeKey.Equals(((Scope) obj).ScopeKey);
        }

        /// <inheritdoc />
        public override int GetHashCode() => ScopeKey.GetHashCode();

        internal void AddResource(IDisposable resource) => _resources.Add(resource);

        internal void RemoveResource(IDisposable resource) => _resources.Remove(resource);

        private class DefaultScopeKey
        {
            public static readonly object Shared = new DefaultScopeKey();

            private DefaultScopeKey()
            {
            }

            public override string ToString() => "Default Resolving Scope Key";
        }
    }
}
