namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the scope which could be used with <c>Lifetime.ScopeSingleton</c>
    /// </summary>
    [PublicAPI]
    public class Scope: IDisposable
    {
        [NotNull] internal readonly object ScopeKey;
        [CanBeNull] private readonly Scope _prevScope;
        [NotNull] private readonly List<IDisposable> _resources = new List<IDisposable>();

        /// <summary>
        /// The current scope.
        /// </summary>
        [NotNull] public static Scope Current { get; private set; } = new Scope(DefaultScopeKey.Shared);

        /// <summary>
        /// Creates the instance of a new scope.
        /// </summary>
        /// <param name="scopeKey">The key of scope.</param>
        public Scope([NotNull] object scopeKey)
        {
            ScopeKey = scopeKey ?? throw new ArgumentNullException(nameof(scopeKey));
            _prevScope = Current;
            Current = this;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (var resource in _resources.ToList())
            {
                resource.Dispose();
            }

            Current = _prevScope ?? throw new NotSupportedException();
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
        public override int GetHashCode()
        {
            return ScopeKey.GetHashCode();
        }

        internal void AddResource(IDisposable resource)
        {
            _resources.Add(resource);
        }

        private class DefaultScopeKey
        {
            public static readonly object Shared = new DefaultScopeKey();

            private DefaultScopeKey()
            {
            }

            public override string ToString()
            {
                return "Default Resolving Scope Key";
            }
        }
    }
}
