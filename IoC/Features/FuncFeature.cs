namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Internal;

    public sealed  class FuncFeature : IConfiguration
    {
        public static readonly IConfiguration Shared = new FuncFeature();

        private static readonly Dictionary<Type, Type> FuncFactories = new Dictionary<Type, Type>()
        {
            { typeof(Func<>), typeof(FuncFactory<>) },
            { typeof(Func<,>), typeof(FuncFactory<,>) },
            { typeof(Func<,,>), typeof(FuncFactory<,,>) },
            { typeof(Func<,,,>), typeof(FuncFactory<,,,>) },
            { typeof(Func<,,,,>), typeof(FuncFactory<,,,,>) },
            { typeof(Func<,,,,,>), typeof(FuncFactory<,,,,,>) },
            { typeof(Func<,,,,,,>), typeof(FuncFactory<,,,,,,>) },
            { typeof(Func<,,,,,,,>), typeof(FuncFactory<,,,,,,,>) },
            { typeof(Func<,,,,,,,,>), typeof(FuncFactory<,,,,,,,,>) }
        };

        private FuncFeature()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            foreach (var func in FuncFactories)
            {
                yield return container
                    .Bind(func.Value)
                    .AnyTag()
                    .To(func.Value);

                yield return container
                    .Bind(func.Key)
                    .AnyTag()
                    .To(ctx => CreateFunc(ctx, func.Value));
            }
        }

        private static object CreateFunc(ResolvingContext context, Type funcFactoryType)
        {
            Type[] genericTypeArguments;
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (context.IsGenericResolvingType)
            {
                genericTypeArguments = context.ResolvingKey.ContractType.GenericTypeArguments();
            }
            else
            {
                genericTypeArguments = context.ResolvingContainer.Get<IIssueResolver>().CannotGetGenericTypeArguments(context.ResolvingKey.ContractType);
            }

            var instanceType = funcFactoryType.MakeGenericType(genericTypeArguments);
            var tag = context.ResolvingKey.Tag;
            return ((IFuncFactory)context.ResolvingContainer.Tag(tag).Get(instanceType)).Create();
        }

        private interface IFuncFactory
        {
            object Create();
        }

        private class FuncFactory<T> : IFuncFactory
        {
            private static readonly Key ResolvingKey = new Key(typeof(T));
            private static readonly bool IsGenericResolvingType = typeof(T).IsConstructedGenericType();
            protected readonly IResolver Resolver;
            protected ResolvingContext ResolvingContext;

            // ReSharper disable once MemberCanBeProtected.Local
            public FuncFactory(ResolvingContext context)
            {
                var resolvingKey = context.ResolvingKey.Tag == null ? ResolvingKey : new Key(typeof(T), context.ResolvingKey.Tag);
                ResolvingContext = new ResolvingContext(context.RegistrationContext)
                {
                    ResolvingKey = resolvingKey,
                    ResolvingContainer = context.ResolvingContainer,
                    Args = context.Args,
                    IsGenericResolvingType = IsGenericResolvingType
                };

                if (!ResolvingContext.ResolvingContainer.TryGetResolver(ResolvingContext.ResolvingKey, out Resolver))
                {
                    Resolver = ResolvingContext.ResolvingContainer.Get<IIssueResolver>().CannotGetResolver(ResolvingContext.ResolvingContainer, ResolvingContext.ResolvingKey);
                }
            }

            public virtual object Create()
            {
                return new Func<T>(() => (T)Resolver.Resolve(ResolvingContext.ResolvingKey, ResolvingContext.ResolvingContainer));
            }
        }

        private sealed class FuncFactory<T1, T> : FuncFactory<T>
        {
            public FuncFactory(ResolvingContext context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T>(arg1 => (T)Resolver.Resolve(ResolvingContext.ResolvingKey, ResolvingContext.ResolvingContainer, 0, arg1));
            }
        }

        private sealed class FuncFactory<T1, T2, T> : FuncFactory<T>
        {
            public FuncFactory(ResolvingContext context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T>((arg1, arg2) => (T)Resolver.Resolve(ResolvingContext.ResolvingKey, ResolvingContext.ResolvingContainer, 0, arg1, arg2));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T> : FuncFactory<T>
        {
            public FuncFactory(ResolvingContext context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T>((arg1, arg2, arg3) => (T)Resolver.Resolve(ResolvingContext.ResolvingKey, ResolvingContext.ResolvingContainer, 0, arg1, arg2, arg3));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T> : FuncFactory<T>
        {
            public FuncFactory(ResolvingContext context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T>((arg1, arg2, arg3, arg4) => (T)Resolver.Resolve(ResolvingContext.ResolvingKey, ResolvingContext.ResolvingContainer, 0, arg1, arg2, arg3, arg4));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T5, T> : FuncFactory<T>
        {
            public FuncFactory(ResolvingContext context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T5, T>((arg1, arg2, arg3, arg4, arg5) => (T)Resolver.Resolve(ResolvingContext.ResolvingKey, ResolvingContext.ResolvingContainer, 0, arg1, arg2, arg3, arg4, arg5));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T5, T6, T> : FuncFactory<T>
        {
            public FuncFactory(ResolvingContext context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T5, T6, T>((arg1, arg2, arg3, arg4, arg5, arg6) => (T)Resolver.Resolve(ResolvingContext.ResolvingKey, ResolvingContext.ResolvingContainer, 0, arg1, arg2, arg3, arg4, arg5, arg6));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T5, T6, T7, T> : FuncFactory<T>
        {
            public FuncFactory(ResolvingContext context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => (T)Resolver.Resolve(ResolvingContext.ResolvingKey, ResolvingContext.ResolvingContainer, 0, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T5, T6, T7, T8, T> : FuncFactory<T>
        {
            public FuncFactory(ResolvingContext context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => (T)Resolver.Resolve(ResolvingContext.ResolvingKey, ResolvingContext.ResolvingContainer, 0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
            }
        }
    }
}
