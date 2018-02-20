namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Allows to resolve Funcs.
    /// </summary>
    [PublicAPI]
    public sealed  class FuncFeature : IConfiguration
    {
        /// The shared instance.
        public static readonly IConfiguration Shared = new FuncFeature();

        private FuncFeature()
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Bind<Func<TT>>().AnyTag().To(
                ctx => (() => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container)));

            yield return container.Bind<Func<TT1, TT>>().AnyTag().To(
                ctx => (arg1 => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1)));

            yield return container.Bind<Func<TT1, TT2, TT>>().AnyTag().To(
                ctx => ((arg1, arg2) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2)));

            yield return container.Bind<Func<TT1, TT2, TT3, TT>>().AnyTag().To(
                ctx => ((arg1, arg2, arg3) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3)));

            yield return container.Bind<Func<TT1, TT2, TT3, TT4, TT>>().AnyTag().To(
                ctx => ((arg1, arg2, arg3, arg4) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4)));

            yield return container.Bind<Func<TT1, TT2, TT3, TT4, TT5, TT>>().AnyTag().To(
                ctx => ((arg1, arg2, arg3, arg4, arg5) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5)));

            yield return container.Bind<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT>>().AnyTag().To(
                ctx => ((arg1, arg2, arg3, arg4, arg5, arg6) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5, arg6)));

            yield return container.Bind<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT>>().AnyTag().To(
                ctx => ((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7)));

            yield return container.Bind<Func<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8, TT>>().AnyTag().To(
                ctx => ((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => ctx.Container.Inject<Resolver<TT>>(ctx.Key.Tag)(ctx.Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)));
        }
    }
}
