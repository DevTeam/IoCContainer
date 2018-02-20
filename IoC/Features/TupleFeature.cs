namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Allows to resolve Tuples.
    /// </summary>
    [PublicAPI]
    public sealed  class TupleFeature : IConfiguration
    {
        /// The shared instance.
        public static readonly IConfiguration Shared = new TupleFeature();

        private TupleFeature()
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Bind<Tuple<TT>>().AnyTag().To(
                ctx => new Tuple<TT>(ctx.Container.Inject<TT>(ctx.Key.Tag)));

            yield return container.Bind<Tuple<TT1, TT2>>().AnyTag().To(ctx => new Tuple<TT1, TT2>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag)));

            yield return container.Bind<Tuple<TT1, TT2, TT3>>().AnyTag().To(ctx => new Tuple<TT1, TT2, TT3>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag)));

            yield return container.Bind<Tuple<TT1, TT2, TT3, TT4>>().AnyTag().To(ctx => new Tuple<TT1, TT2, TT3, TT4>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag)));

            yield return container.Bind<Tuple<TT1, TT2, TT3, TT4, TT5>>().AnyTag().To(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag),
                ctx.Container.Inject<TT5>(ctx.Key.Tag)));

            yield return container.Bind<Tuple<TT1, TT2, TT3, TT4, TT5, TT6>>().AnyTag().To(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5, TT6>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag),
                ctx.Container.Inject<TT5>(ctx.Key.Tag),
                ctx.Container.Inject<TT6>(ctx.Key.Tag)));

            yield return container.Bind<Tuple<TT1, TT2, TT3, TT4, TT5, TT6, TT7>>().AnyTag().To(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5, TT6, TT7>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag),
                ctx.Container.Inject<TT5>(ctx.Key.Tag),
                ctx.Container.Inject<TT6>(ctx.Key.Tag),
                ctx.Container.Inject<TT7>(ctx.Key.Tag)));

            yield return container.Bind<Tuple<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8>>().AnyTag().To(ctx => new Tuple<TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8>(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag),
                ctx.Container.Inject<TT5>(ctx.Key.Tag),
                ctx.Container.Inject<TT6>(ctx.Key.Tag),
                ctx.Container.Inject<TT7>(ctx.Key.Tag),
                ctx.Container.Inject<TT8>(ctx.Key.Tag)));
        }
    }
}
