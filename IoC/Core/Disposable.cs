// ReSharper disable RedundantUsingDirective
namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal static class Disposable
    {
        private static readonly TypeDescriptor DisposableTypeDescriptor = typeof(IDisposable).Descriptor();
#if NET5_0_OR_GREATER || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
        private static readonly TypeDescriptor AsyncDisposableTypeDescriptor = typeof(IAsyncDisposable).Descriptor();
#endif

        [NotNull] public static readonly IDisposable Empty = EmptyDisposable.Shared;

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IDisposable Create([NotNull] Action action)
        {
#if DEBUG
            if (action == null) throw new ArgumentNullException(nameof(action));
#endif
            return new DisposableAction(action);
        }

        public static bool IsDisposable(this TypeDescriptor typeDescriptor)
        {
            if (DisposableTypeDescriptor.IsAssignableFrom(typeDescriptor))
            {
                return true;
            }

#if NET5_0_OR_GREATER || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            if (AsyncDisposableTypeDescriptor.IsAssignableFrom(typeDescriptor))
            {
                return true;
            }
#endif

            return false;
        }

    [CanBeNull]
        public static IDisposable AsDisposable<T>(this T instance)
        {
            var disposable = instance as IDisposable;
#if NET5_0_OR_GREATER || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_1
            if (instance is IAsyncDisposable asyncDisposable)
            {
                return new DisposableAction(() =>
                    {
                        disposable?.Dispose();
                        asyncDisposable.DisposeAsync().AsTask().Wait();
                    },
                    instance);
            }
#endif
            return disposable;
        }

        public static void Register([NotNull] this IResourceRegistry registry, [CanBeNull] IDisposable disposable)
        {
            if (disposable != null)
            {
                registry.RegisterResource(disposable);
            }
        }
        public static void UnregisterAndDispose([NotNull] this IResourceRegistry registry, [CanBeNull] IDisposable disposable)
        {
            if (disposable != null && registry.UnregisterResource(disposable))
            {
                disposable.Dispose();
            }
        }

        [MethodImpl((MethodImplOptions)0x100)]
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
            [CanBeNull] private readonly object _key;
            private int _counter;

            public DisposableAction([NotNull] Action action, [CanBeNull] object key = null)
            {
                _action = action;
                _key = key ?? action;
            }

            public void Dispose()
            {
                if (Interlocked.Increment(ref _counter) != 1) return;
                _action();
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is DisposableAction other && Equals(_key, other._key);
            }

            public override int GetHashCode() => 
                _key != null ? _key.GetHashCode() : 0;
        }

        private sealed class CompositeDisposable : IDisposable
        {
            private readonly IEnumerable<IDisposable> _disposables;
            private int _counter;

            public CompositeDisposable(IEnumerable<IDisposable> disposables)
                => _disposables = disposables;

            public void Dispose()
            {
                if (Interlocked.Increment(ref _counter) != 1) return;
                foreach (var disposable in _disposables)
                {
                    disposable?.Dispose();
                }
            }
        }

        private sealed class EmptyDisposable : IDisposable
        {
            [NotNull] public static readonly IDisposable Shared = new EmptyDisposable();

            private EmptyDisposable() { }

            public void Dispose() { }
        }
    }
}
