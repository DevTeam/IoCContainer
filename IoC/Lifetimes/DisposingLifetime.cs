namespace IoC.Lifetimes
{
    using System;
    using System.Collections.Generic;
    // ReSharper disable once RedundantUsingDirective
    using Core;

    /// <summary>
    /// Automatically calls a <c>Disposable()</c> method for disposable instances after a container has disposed.
    /// </summary>
    [PublicAPI]
    public class DisposingLifetime: TrackedLifetime
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();
#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
        private readonly List<IAsyncDisposable> _asyncDisposables = new List<IAsyncDisposable>();
#endif

        public DisposingLifetime()
            : base(TrackTypes.AfterCreation)
        {
        }

        public override ILifetime CreateLifetime() => new DisposingLifetime();

        protected override object AfterCreation(object newInstance, IContainer container, object[] args)
        {
            var instance = base.AfterCreation(newInstance, container, args);
            lock (_disposables)
            {
                if (instance is IDisposable disposable)
                {
                    _disposables.Add(disposable);
                }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
                if (instance is IAsyncDisposable asyncDisposable)
                {
                    _asyncDisposables.Add(asyncDisposable);
                }
#endif
            }

            return instance;
        }

        public override void Dispose()
        {
            var disposables = new List<IDisposable>();
#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            var asyncDisposables = new List<IAsyncDisposable>();
#endif
            lock (_disposables)
            {
                disposables.AddRange(_disposables);
                _disposables.Clear();

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
                asyncDisposables.AddRange(_asyncDisposables);
                _asyncDisposables.Clear();
#endif
            }

            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }

#if NETCOREAPP5_0 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            foreach (var asyncDisposable in asyncDisposables)
            {
                asyncDisposable.ToDisposable().Dispose();
            }
#endif

            base.Dispose();
        }
    }
}
