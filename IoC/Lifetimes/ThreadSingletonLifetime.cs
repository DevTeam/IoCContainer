#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
namespace IoC.Lifetimes
{
    using System;

    /// <summary>
    /// Represents singleton per thread lifetime.
    /// </summary>
    [PublicAPI]
    public class ThreadSingletonLifetime : SingletonBasedLifetime<int>
    {
        [NotNull] private readonly Func<ILifetime> _singletonLifetimeFactory;

        /// <inheritdoc />
        public ThreadSingletonLifetime([NotNull] Func<ILifetime> singletonLifetimeFactory) : base(singletonLifetimeFactory)
        {
            _singletonLifetimeFactory = singletonLifetimeFactory ?? throw new ArgumentNullException(nameof(singletonLifetimeFactory));
        }

        /// <inheritdoc />
        protected override int CreateKey(IContainer container, object[] args)
        {
            return System.Threading.Thread.CurrentThread.ManagedThreadId;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return Lifetime.ScopeSingleton.ToString();
        }

        /// <inheritdoc />
        public override ILifetime Clone()
        {
            return new ThreadSingletonLifetime(_singletonLifetimeFactory);
        }
    }
}
#endif