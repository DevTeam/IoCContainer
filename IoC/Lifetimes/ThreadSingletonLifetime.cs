namespace IoC.Lifetimes
{
    using System;
    using System.Threading;
    
    // Represents the custom thread singleton lifetime based on the KeyBasedLifetime
    internal sealed class ThreadSingletonLifetime : KeyBasedLifetime<int>
    {
        [ThreadStatic] private static volatile int _threadId;
        private static volatile int _latsThreadId;

        // Creates a clone of the current lifetime (for the case with generic types)
        public override ILifetime CreateLifetime() =>
            new ThreadSingletonLifetime();

        // Provides an instance key. In this case, it is just a thread identifier.
        // If a key the same an instance is the same too.
        protected override int CreateKey(IContainer container, object[] args)
        {
            if (_threadId == 0)
            {
                Interlocked.Exchange(ref _threadId, Interlocked.Increment(ref _latsThreadId));
            }

            return _threadId;
        }
    }
}
