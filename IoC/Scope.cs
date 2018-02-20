namespace IoC
{
    using System;

    /// <summary>
    /// Represents the scope which could be used with <c>Lifetime.ScopeSingleton</c>
    /// </summary>
    [PublicAPI]
    public class Scope: IDisposable
    {
        [NotNull] internal readonly object ScopeKey;

        [CanBeNull] private readonly Scope _prevScope;

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
            Current = _prevScope ?? throw new NotSupportedException();
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
