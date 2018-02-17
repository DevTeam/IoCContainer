namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    public class LazyFeature : IConfiguration
    {
        public static readonly IConfiguration Shared = new LazyFeature();

        private LazyFeature()
        {
        }

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
