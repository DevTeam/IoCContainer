namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Allows to resolve Lazy.
    /// </summary>
    public class LazyFeature : IConfiguration
    {
        /// The shared instance.
        public static readonly IConfiguration Shared = new LazyFeature();

        private LazyFeature()
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            yield return container
                .Bind<Lazy<TT>>()
                .AnyTag()
                .To(ctx => new Lazy<TT>(() => ctx.Container.Inject<TT>(), true));
        }
    }
}
