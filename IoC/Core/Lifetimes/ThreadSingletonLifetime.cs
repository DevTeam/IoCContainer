#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_5 && !NETSTANDARD1_6
namespace IoC.Core.Lifetimes
{
    using System;

    internal class ThreadSingletonLifetime : SingletonBasedLifetime<int>
    {
        [NotNull] private readonly Func<ILifetime> _singletonLifetimeFactory;

        public ThreadSingletonLifetime([NotNull] Func<ILifetime> singletonLifetimeFactory) : base(singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory ?? throw new ArgumentNullException(nameof(singletonLifetimeFactory));
        }

        protected override int CreateKey(IContainer container, object[] args)
        {
            return System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        public override string ToString()
        {
            return Lifetime.ScopeSingleton.ToString();
        }

        public override ILifetime Clone()
        {
            return new ThreadSingletonLifetime(_singletonLifetimeFactory);
        }
    }
}
#endif