﻿namespace IoC.Core
{
    using System;

    internal class Holder<TInstance> : IHolder<TInstance>
    {
        [NotNull] private readonly IToken _token;

        public Holder([NotNull] IToken token, [NotNull] TInstance instance)
        {
            _token = token ?? throw new ArgumentNullException(nameof(token));
            Instance = instance;
        }

        public IContainer Container => _token.Container;

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