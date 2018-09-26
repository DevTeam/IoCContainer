namespace IoC.Features.Interception
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal static class Disposable
    {
        [NotNull]
        public static readonly IDisposable Empty = EmptyDisposable.Shared;

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Create([NotNull] Action action)
        {
#if DEBUG   
            if (action == null) throw new ArgumentNullException(nameof(action));
#endif
            return new DisposableAction(action);
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Create([NotNull][ItemCanBeNull] IEnumerable<IDisposable> disposables)
        {
#if DEBUG   
            if (disposables == null) throw new ArgumentNullException(nameof(disposables));
#endif
            return new CompositeDisposable(disposables);
        }

        private sealed class DisposableAction : IDisposable
        {
            [NotNull] private readonly Action _action;
            private volatile object _lockObject = new object();

            public DisposableAction([NotNull] Action action)
            {
                _action = action;
            }

            public void Dispose()
            {
                var lockObject = _lockObject;
                if (lockObject == null)
                {
                    return;
                }
                
                lock (lockObject)
                {
                    if (_lockObject == null)
                    {
                        return;
                    }

                    _lockObject = null;
                }

                _action();
            }
        }

        private sealed class CompositeDisposable : IDisposable
        {
            private IDisposable[] _disposables;
            
            public CompositeDisposable(IEnumerable<IDisposable> disposables)
                => _disposables = disposables.ToArray();

            public void Dispose()
            {
                var disposables = _disposables;
                if (disposables == null)
                {
                    return;
                }

                lock (disposables)
                {
                    if (_disposables == null)
                    {
                        return;
                    }

                    _disposables = null;
                }

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var index = 0; index < disposables.Length; index++)
                {
                    disposables[index]?.Dispose();
                }
            }
        }

        private class EmptyDisposable: IDisposable
        {
            [NotNull]
            public static readonly IDisposable Shared = new EmptyDisposable();

            private EmptyDisposable() { }

            public void Dispose() { }
        }
    }
}
