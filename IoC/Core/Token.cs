﻿namespace IoC.Core
{
    using System;
    using System.Runtime.CompilerServices;

    internal struct Token: IToken
    {
        [NotNull] private readonly IDisposable _dependencyToken;

        public Token([NotNull] IContainer container, [NotNull] IDisposable dependencyToken)
        {
            Container = container ?? throw new ArgumentNullException(nameof(container));
            _dependencyToken = dependencyToken ?? throw new ArgumentNullException(nameof(dependencyToken));
        }

        public IContainer Container { get; }

        [MethodImpl((MethodImplOptions)256)]
        public void Dispose() => _dependencyToken.Dispose();
    }
}