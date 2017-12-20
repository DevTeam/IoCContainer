namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    internal class FuncFeature: IConfiguration
    {
        public static readonly IConfiguration Shared = new FuncFeature();

        private FuncFeature()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container
                .Map(typeof(Func<>))
                .To(CreateFunc);

            yield return container
                .Map(typeof(Func<,>))
                .To(CreateFunc);

            yield return container
                .Map(typeof(Func<,,>))
                .To(CreateFunc);

            yield return container
                .Map(typeof(Func<,,,>))
                .To(CreateFunc);

            yield return container
                .Map(typeof(Func<,,,,>))
                .To(CreateFunc);

            yield return container
                .Map(typeof(Func<,,,,,>))
                .To(CreateFunc);

            yield return container
                .Map(typeof(Func<,,,,,,>))
                .To(CreateFunc);

            yield return container
                .Map(typeof(Func<,,,,,,,>))
                .To(CreateFunc);

            yield return container
                .Map(typeof(Func<,,,,,,,,>))
                .To(CreateFunc);
        }

        private static object CreateFunc(Context ctx)
        {
            if (!ctx.ContractType.IsConstructedGenericType)
            {
                throw new InvalidOperationException();
            }

            var genericTypeArguments = ctx.ContractType.GenericTypeArguments;
            Type instanceFuncType;
            switch (genericTypeArguments.Length)
            {
                case 1:
                    instanceFuncType = typeof(InstanceFunc<>);
                    break;

                case 2:
                    instanceFuncType = typeof(InstanceFunc<,>);
                    break;

                case 3:
                    instanceFuncType = typeof(InstanceFunc<,,>);
                    break;

                case 4:
                    instanceFuncType = typeof(InstanceFunc<,,,>);
                    break;

                case 5:
                    instanceFuncType = typeof(InstanceFunc<,,,,>);
                    break;

                case 6:
                    instanceFuncType = typeof(InstanceFunc<,,,,,>);
                    break;

                case 7:
                    instanceFuncType = typeof(InstanceFunc<,,,,,,>);
                    break;

                case 8:
                    instanceFuncType = typeof(InstanceFunc<,,,,,,,>);
                    break;

                case 9:
                    instanceFuncType = typeof(InstanceFunc<,,,,,,,,>);
                    break;

                default:
                    throw new InvalidOperationException();
            }

            var instanceType = instanceFuncType.MakeGenericType(genericTypeArguments);
            var func = ((IFuncFactory)Activator.CreateInstance(instanceType, ctx)).Create();
            return func;
        }


        private interface IFuncFactory
        {
            object Create();
        }

        private class InstanceFunc<T> : IFuncFactory
        {
            protected readonly Context Ctx;
            protected readonly IResolver Resolver;

            // ReSharper disable once MemberCanBeProtected.Local
            public InstanceFunc(Context ctx)
            {
                Ctx = ctx;
                var key = new Key(new Contract(typeof(T)), ctx.Key.Tag);
                if (!ctx.ResolvingContainer.TryGetResolver(key, out Resolver))
                {
                    throw new InvalidOperationException();
                }
            }

            public virtual object Create() { return CreateFunc(); }
            private Func<T> CreateFunc() { return () => (T)Resolver.Resolve(Ctx.ResolvingContainer, typeof(T)); }
        }

        private class InstanceFunc<T1, T> : InstanceFunc<T>
        {
            public InstanceFunc(Context ctx) : base(ctx) { }
            public override object Create() { return CreateFunc(); }
            private Func<T1, T> CreateFunc() { return arg1 => (T)Resolver.Resolve(Ctx.ResolvingContainer, typeof(T), arg1); }
        }

        private class InstanceFunc<T1, T2, T> : InstanceFunc<T>
        {
            public InstanceFunc(Context ctx) : base(ctx) { }
            public override object Create() { return CreateFunc(); }
            private Func<T1, T2, T> CreateFunc() { return (arg1, arg2) => (T)Resolver.Resolve(Ctx.ResolvingContainer, typeof(T), arg1, arg2); }
        }

        private class InstanceFunc<T1, T2, T3, T> : InstanceFunc<T>
        {
            public InstanceFunc(Context ctx) : base(ctx) { }
            public override object Create() { return CreateFunc(); }
            private Func<T1, T2, T3, T> CreateFunc() { return (arg1, arg2, arg3) => (T)Resolver.Resolve(Ctx.ResolvingContainer, typeof(T), arg1, arg2, arg3); }
        }

        private class InstanceFunc<T1, T2, T3, T4, T> : InstanceFunc<T>
        {
            public InstanceFunc(Context ctx) : base(ctx) { }
            public override object Create() { return CreateFunc(); }
            private Func<T1, T2, T3, T4, T> CreateFunc() { return (arg1, arg2, arg3, arg4) => (T)Resolver.Resolve(Ctx.ResolvingContainer, typeof(T), arg1, arg2, arg3, arg4); }
        }

        private class InstanceFunc<T1, T2, T3, T4, T5, T> : InstanceFunc<T>
        {
            public InstanceFunc(Context ctx) : base(ctx) { }
            public override object Create() { return CreateFunc(); }
            private Func<T1, T2, T3, T4, T5, T> CreateFunc() { return (arg1, arg2, arg3, arg4, arg5) => (T)Resolver.Resolve(Ctx.ResolvingContainer, typeof(T), arg1, arg2, arg3, arg4, arg5); }
        }

        private class InstanceFunc<T1, T2, T3, T4, T5, T6, T> : InstanceFunc<T>
        {
            public InstanceFunc(Context ctx) : base(ctx) { }
            public override object Create() { return CreateFunc(); }
            private Func<T1, T2, T3, T4, T5, T6, T> CreateFunc() { return (arg1, arg2, arg3, arg4, arg5, arg6) => (T)Resolver.Resolve(Ctx.ResolvingContainer, typeof(T), arg1, arg2, arg3, arg4, arg5, arg6); }
        }

        private class InstanceFunc<T1, T2, T3, T4, T5, T6, T7, T> : InstanceFunc<T>
        {
            public InstanceFunc(Context ctx) : base(ctx) { }
            public override object Create() { return CreateFunc(); }
            private Func<T1, T2, T3, T4, T5, T6, T7, T> CreateFunc() { return (arg1, arg2, arg3, arg4, arg5, arg6, arg7) => (T)Resolver.Resolve(Ctx.ResolvingContainer, typeof(T), arg1, arg2, arg3, arg4, arg5, arg6, arg7); }
        }

        private class InstanceFunc<T1, T2, T3, T4, T5, T6, T7, T8, T> : InstanceFunc<T>
        {
            public InstanceFunc(Context ctx) : base(ctx) { }
            public override object Create() { return CreateFunc(); }
            private Func<T1, T2, T3, T4, T5, T6, T7, T8, T> CreateFunc() { return (arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => (T)Resolver.Resolve(Ctx.ResolvingContainer, typeof(T), arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8); }
        }
    }
}
