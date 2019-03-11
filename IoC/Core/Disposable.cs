namespace IoC.Core
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

#if NETCOREAPP3_0
        public static IDisposable ToDisposable([NotNull] this IAsyncDisposable asyncDisposable)
        {
#if DEBUG
            if (asyncDisposable == null) throw new ArgumentNullException(nameof(asyncDisposable));
#endif
            return new DisposableAction(() => { asyncDisposable.DisposeAsync().AsTask().Wait(); }, asyncDisposable);            
        }
#endif
        private sealed class DisposableAction : IDisposable
        {
            [NotNull] private readonly Action _action;
            [CanBeNull] private readonly object _key;
            
            public DisposableAction([NotNull] Action action, [CanBeNull] object key = null)
            {
                _action = action;
                _key = key ?? action;
            }

            public void Dispose()
            {
                _action();
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is DisposableAction other && Equals(_key, other._key);
            }

            public override int GetHashCode()
            {
                return _key != null ? _key.GetHashCode() : 0;
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
