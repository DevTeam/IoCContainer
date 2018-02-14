namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static class Disposable
    {
        [NotNull]
        public static readonly IDisposable Empty = new EmptyDisposable();

        [NotNull]
        public static IDisposable Create([NotNull] Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            return new DisposableAction(action);
        }

        [NotNull]
        public static IDisposable Create([NotNull][ItemCanBeNull] IEnumerable<IDisposable> disposables)
        {
            if (disposables == null) throw new ArgumentNullException(nameof(disposables));
            return new CompositeDisposable(disposables);
        }

        private class DisposableAction : IDisposable
        {
            [NotNull] private readonly Action _action;
            private bool _disposed;

            public DisposableAction([NotNull] Action action)
            {
                _action = action;
                _disposed = false;
            }

            public void Dispose()
            {
                if (_disposed)
                {
                    return;
                }

                _disposed = true;
                _action();
            }
        }

        private class CompositeDisposable: IDisposable
        {
            private readonly List<IDisposable> _disposables;

            public CompositeDisposable(IEnumerable<IDisposable> disposables)
            {
                _disposables = disposables.ToList();
            }

            public void Dispose()
            {
                foreach (var disposable in _disposables)
                {
                    disposable?.Dispose();
                }

                _disposables.Clear();
            }
        }

        private class EmptyDisposable: IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}
