namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;
    using static Core.FluentRegister;

    /// <summary>
    /// Allows to resolve common types like a <c>Lazy</c>.
    /// </summary>
    public sealed class CommonTypesFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new CommonTypesFeature();

        private CommonTypesFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => new Lazy<TT>(() => ctx.Container.Inject<TT>(ctx.Key.Tag), true), null, AnyTag);
            yield return container.Register(ctx => ctx.Container.TryInjectValue<TTS>(ctx.Key.Tag), null, AnyTag);
        }
    }
}
