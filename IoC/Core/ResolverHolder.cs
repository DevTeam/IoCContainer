namespace IoC.Core
{
    using System;

    internal struct ResolverHolder<T>: IResolverHolder<T>
    {
        public ResolverHolder([NotNull] Resolver<T> resolver)
        {
            Resolve = resolver ?? throw new ArgumentNullException(nameof(resolver));
        }

        [NotNull]
        public Resolver<T> Resolve { get; }

        public void Dispose()
        {
        }
    }
}
