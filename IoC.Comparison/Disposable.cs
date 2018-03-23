namespace IoC.Comparison
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class Disposable
    {
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IDisposable Create([NotNull] Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            return new DisposableAction(action);
        }

        private sealed class DisposableAction : IDisposable
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
    }
}
