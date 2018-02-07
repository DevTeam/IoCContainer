namespace IoC
{
    using System;

    [PublicAPI]
    public class Scope: IDisposable
    {
        [NotNull] private static Scope _currentScope = new Scope(DefaultScopeKey.Shared);
        [NotNull] public readonly object ScopeKey;
        [CanBeNull] private readonly Scope _prevScope;

        [NotNull] public static Scope Current => _currentScope;

        public Scope([NotNull] object scopeKey)
        {
            ScopeKey = scopeKey ?? throw new ArgumentNullException(nameof(scopeKey));
            _prevScope = _currentScope;
            _currentScope = this;
        }

        public void Dispose()
        {
            _currentScope = _prevScope ?? throw new NotSupportedException();
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
