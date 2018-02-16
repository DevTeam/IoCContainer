namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using Core;

    [PublicAPI]
    public sealed  class FuncFeature : IConfiguration
    {
        public static readonly IConfiguration Shared = new FuncFeature();
        private static readonly Dictionary<Type, Type> Factories = new Dictionary<Type, Type>()
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
            foreach (var factory in Factories)
            {
                var curFactory = factory;
                yield return container
                    .Bind(curFactory.Value)
                    .AnyTag()
                    .To(curFactory.Value);

                yield return container
                    .Bind(curFactory.Key)
                    .AnyTag()
                    .To(ctx => CreateFunc(ctx.Key, ctx.Container, curFactory.Value));
            }
        }

        private static object CreateFunc(Key key, [NotNull] IContainer container, Type factoryType)
        {
            Type[] genericTypeArguments;
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            var typeInfo = key.Type.Info();
            if (typeInfo.IsConstructedGenericType)
            {
                genericTypeArguments = typeInfo.GenericTypeArguments;
            }
            else
            {
                genericTypeArguments = container.Get<IIssueResolver>().CannotGetGenericTypeArguments(key.Type);
            }

            var instanceType = factoryType.MakeGenericType(genericTypeArguments);
            if (!container.TryGetResolver<IFuncFactory>(container, instanceType, key.Tag, out var resolver))
            {
                var objectResolver = container.Get<IIssueResolver>().CannotGetResolver<object>(container, new Key(instanceType, key.Tag));
                return ((IFuncFactory)objectResolver(container)).Create();
            }

            return resolver(container).Create();
        }

        private interface IFuncFactory
        {
            object Create();
        }

        private class FuncFactory<T> : IFuncFactory
        {
            protected readonly Resolver<T> Resolver;
            protected readonly IContainer Container;

            // ReSharper disable once MemberCanBeProtected.Local
            public FuncFactory(Context context)
            {
                Container = context.Container;
                if (!context.Container.TryGetResolver(Container, typeof(T), context.Key.Tag, out Resolver))
                {
                    var key = Key.Create<T>(context.Key.Tag);
                    Resolver = context.Container.Get<IIssueResolver>().CannotGetResolver<T>(Container, key);
                }
            }

            public virtual object Create()
            {
                return new Func<T>(() => Resolver(Container));
            }
        }

        private sealed class FuncFactory<T1, T> : FuncFactory<T>
        {
            public FuncFactory(Context context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T>(arg1 => Resolver(Container, arg1));
            }
        }

        private sealed class FuncFactory<T1, T2, T> : FuncFactory<T>
        {
            public FuncFactory(Context context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T>((arg1, arg2) => Resolver(Container, arg1, arg2));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T> : FuncFactory<T>
        {
            public FuncFactory(Context context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T>((arg1, arg2, arg3) => Resolver(Container, arg1, arg2, arg3));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T> : FuncFactory<T>
        {
            public FuncFactory(Context context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T>((arg1, arg2, arg3, arg4) => Resolver(Container, arg1, arg2, arg3, arg4));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T5, T> : FuncFactory<T>
        {
            public FuncFactory(Context context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T5, T>((arg1, arg2, arg3, arg4, arg5) => Resolver(Container, arg1, arg2, arg3, arg4, arg5));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T5, T6, T> : FuncFactory<T>
        {
            public FuncFactory(Context context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T5, T6, T>((arg1, arg2, arg3, arg4, arg5, arg6) => Resolver(Container, arg1, arg2, arg3, arg4, arg5, arg6));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T5, T6, T7, T> : FuncFactory<T>
        {
            public FuncFactory(Context context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Resolver(Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7));
            }
        }

        private sealed class FuncFactory<T1, T2, T3, T4, T5, T6, T7, T8, T> : FuncFactory<T>
        {
            public FuncFactory(Context context) : base(context) { }

            public override object Create()
            {
                return new Func<T1, T2, T3, T4, T5, T6, T7, T8, T>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Resolver(Container, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8));
            }
        }
    }
}
