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

#if !NET40 && !NET45 && !NET46 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
            yield return container.Bind<(TT1, TT2)>().AnyTag().To(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag)));

            yield return container.Bind<(TT1, TT2, TT3)>().AnyTag().To(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag)));

            yield return container.Bind<(TT1, TT2, TT3, TT4)>().AnyTag().To(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag)));

            yield return container.Bind<(TT1, TT2, TT3, TT4, TT5)>().AnyTag().To(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag),
                ctx.Container.Inject<TT5>(ctx.Key.Tag)));

            yield return container.Bind<(TT1, TT2, TT3, TT4, TT5, TT6)>().AnyTag().To(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag),
                ctx.Container.Inject<TT5>(ctx.Key.Tag),
                ctx.Container.Inject<TT6>(ctx.Key.Tag)));

            yield return container.Bind<(TT1, TT2, TT3, TT4, TT5, TT6, TT7)>().AnyTag().To(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag),
                ctx.Container.Inject<TT5>(ctx.Key.Tag),
                ctx.Container.Inject<TT6>(ctx.Key.Tag),
                ctx.Container.Inject<TT7>(ctx.Key.Tag)));

            yield return container.Bind<(TT1, TT2, TT3, TT4, TT5, TT6, TT7, TT8)>().AnyTag().To(ctx => CreateTuple(
                ctx.Container.Inject<TT1>(ctx.Key.Tag),
                ctx.Container.Inject<TT2>(ctx.Key.Tag),
                ctx.Container.Inject<TT3>(ctx.Key.Tag),
                ctx.Container.Inject<TT4>(ctx.Key.Tag),
                ctx.Container.Inject<TT5>(ctx.Key.Tag),
                ctx.Container.Inject<TT6>(ctx.Key.Tag),
                ctx.Container.Inject<TT7>(ctx.Key.Tag),
                ctx.Container.Inject<TT8>(ctx.Key.Tag)));
#endif
        }

#if !NET40 && !NET45 && !NET46 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2&& !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6
        private static (T1, T2) CreateTuple<T1, T2>(T1 val1, T2 val2) => (val1, val2);

        private static (T1, T2, T3) CreateTuple<T1, T2, T3>(T1 val1, T2 val2, T3 val3) => (val1, val2, val3);

        private static (T1, T2, T3, T4) CreateTuple<T1, T2, T3, T4>(T1 val1, T2 val2, T3 val3, T4 val4) => (val1, val2, val3, val4);

        private static (T1, T2, T3, T4, T5) CreateTuple<T1, T2, T3, T4, T5>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5) => (val1, val2, val3, val4, val5);

        private static (T1, T2, T3, T4, T5, T6) CreateTuple<T1, T2, T3, T4, T5, T6>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5, T6 val6) => (val1, val2, val3, val4, val5, val6);

        private static (T1, T2, T3, T4, T5, T6, T7) CreateTuple<T1, T2, T3, T4, T5, T6, T7>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5, T6 val6, T7 val7) => (val1, val2, val3, val4, val5, val6, val7);

        private static (T1, T2, T3, T4, T5, T6, T7, T8) CreateTuple<T1, T2, T3, T4, T5, T6, T7, T8>(T1 val1, T2 val2, T3 val3, T4 val4, T5 val5, T6 val6, T7 val7, T8 val8) => (val1, val2, val3, val4, val5, val6, val7, val8);
#endif
    }
}
