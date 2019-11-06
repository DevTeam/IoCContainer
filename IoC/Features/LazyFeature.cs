namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;

    /// <summary>
    /// Allows to resolve Lazy.
    /// </summary>
    public class LazyFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new LazyFeature();

        private LazyFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => new Lazy<TT>(() => ctx.Container.Inject<TT>(ctx.Key.Tag), true), null, Feature.AnyTag);
        }
    }
}
