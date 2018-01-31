namespace IoC.Core
{
    using System;

    internal struct ResolverHolder<T>: IDisposable
    {
        [NotNull] public readonly Resolver<T> Resolve;

        public ResolverHolder([NotNull] Resolver<T> resolver)
        {
            Resolve = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        public void Dispose()
        {
        }
    }
}
