#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
namespace IoC.Lifetimes
{
    /// <summary>
    /// Represents singleton per thread lifetime.
    /// </summary>
    [PublicAPI]
    public class ThreadSingletonLifetime : SingletonBasedLifetime<int>
    {
        /// <inheritdoc />
        protected override int CreateKey(IContainer container, object[] args) => System.Threading.Thread.CurrentThread.ManagedThreadId;

        /// <inheritdoc />
        public override string ToString() => Lifetime.ScopeSingleton.ToString();

        /// <inheritdoc />
        public override ILifetime Clone() => new ThreadSingletonLifetime();

        /// <inheritdoc />
        protected override void OnNewInstanceCreated<T>(T newInstance, int key, IContainer container, object[] args)
        {
        }
    }
}
#endif