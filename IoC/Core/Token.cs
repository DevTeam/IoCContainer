namespace IoC.Core
{
    using System;

    internal struct Token: IToken
    {
        [NotNull] private readonly IDisposable _dependencyToken;

        public Token([NotNull] IMutableContainer container, [NotNull] IDisposable dependencyToken)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _dependencyToken = dependencyToken ?? throw new ArgumentNullException(nameof(dependencyToken));
        }

        public IMutableContainer Container { get; }

        public void Dispose() => _dependencyToken.Dispose();
    }
}
