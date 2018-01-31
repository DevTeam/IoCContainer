namespace IoC
{
    using System;

    public class ResolvingScope: IDisposable
    {
        [NotNull] private static ResolvingScope _currentScope = new ResolvingScope(DefaultScopeKey.Shared);
        [NotNull] public readonly object ScopeKey;
        [CanBeNull] private readonly ResolvingScope _prevScope;

        [NotNull] public static ResolvingScope Current => _currentScope;

        public ResolvingScope([NotNull] object scopeKey)
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
