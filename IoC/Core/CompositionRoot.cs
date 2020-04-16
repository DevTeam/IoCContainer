﻿namespace IoC.Core
{
    using System;

    internal sealed class CompositionRoot<TInstance> : ICompositionRoot<TInstance>
    {
        [NotNull] private readonly IToken _token;

        public CompositionRoot([NotNull] IToken token, [NotNull] TInstance instance)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            Instance = instance;
        }

        public TInstance Instance { get; }

        public void Dispose()
        {
            using (_token.Container)
            using (_token)
            {
                if (Instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}