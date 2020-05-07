namespace IoC.Features.Interception
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal static class Disposable
    {
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IDisposable Create([NotNull] Action action)
        {
#if DEBUG   
            if (action == null) throw new ArgumentNullException(nameof(action));
#endif
            return new DisposableAction(action);
        }

        private sealed class DisposableAction : IDisposable
        {
            [NotNull] private readonly Action _action;
            private int _counter;

            public DisposableAction([NotNull] Action action) => _action = action;

            public void Dispose()
            {
                if (Interlocked.Increment(ref _counter) != 1) return;
                _action();
            }
        }
    }
}
