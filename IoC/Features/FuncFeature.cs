namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;

    /// <summary>
    /// Allows to resolve Functions.
    /// </summary>
    [PublicAPI]
    public sealed  class FuncFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new FuncFeature();

        /// The high-performance instance.
        public static readonly IConfiguration LightSet = new FuncFeature(true);

        private readonly bool _light;

        private FuncFeature(bool light = false) => _light = light;

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register<Func<TT>>(ctx => () => ctx.Container.Inject<TT>(ctx.Key.Tag), null, Sets.AnyTag);
            yield return container.Register<Func<TT1, TT>>(ctx => arg1 => ctx.Container.Inject<TT>(ctx.Key.Tag, arg1), null, Sets.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT>>(ctx => (arg1, arg2) => ctx.Container.Inject<TT>(ctx.Key.Tag, arg1, arg2), null, Sets.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT>>(ctx => (arg1, arg2, arg3) => ctx.Container.Inject<TT>(ctx.Key.Tag, arg1, arg2, arg3), null, Sets.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT>>(ctx => (arg1, arg2, arg3, arg4) => ctx.Container.Inject<TT>(ctx.Key.Tag, arg1, arg2, arg3, arg4), null, Sets.AnyTag);

            if (_light) yield break;

            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5) => ctx.Container.Inject<TT>(ctx.Key.Tag, arg1, arg2, arg3, arg4, arg5), null, Sets.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5, arg6) => ctx.Container.Inject<TT>(ctx.Key.Tag, arg1, arg2, arg3, arg4, arg5, arg6), null, Sets.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ctx.Container.Inject<TT>(ctx.Key.Tag, arg1, arg2, arg3, arg4, arg5, arg6, arg7), null, Sets.AnyTag);
            yield return container.Register<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8, TT>>(ctx => (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ctx.Container.Inject<TT>(ctx.Key.Tag, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8), null, Sets.AnyTag);
        }
    }
}
