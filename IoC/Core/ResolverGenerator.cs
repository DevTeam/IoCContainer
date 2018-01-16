namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Threading;
    using Emiters;

    internal sealed class ResolverGenerator : IResolverGenerator
    {
        public static readonly IResolverGenerator Shared = new ResolverGenerator(DependencyEmitter.Shared);
        private static int _methodId;
        private static readonly Type[] DynamicMethodParametersTypes = { typeof(IContainer), typeof(object[]) };
        private readonly IEmitter<IDependency> _dependencyEmitter;

        public ResolverGenerator([NotNull] IEmitter<IDependency> dependencyEmitter)
        {
            _dependencyEmitter = dependencyEmitter ?? throw new ArgumentNullException(nameof(dependencyEmitter));
        }


        public ResolverHolder<T> Generate<T>(Key key, IContainer container, IDependency dependency, ILifetime lifetime = null)
        {
            var methodId = Interlocked.Increment(ref _methodId);
#if DEBUG
            var targetTypeInfo = key.Type.Info();
            if (targetTypeInfo.IsGenericTypeDefinition)
            {
                throw new ArgumentException($"The type {typeof(T)} is generic type definition and cannot be constructed.");
            }

            var dynamicMethodName = "Create" + "_" + dependency.Type.FullName  + "_" + key + "_" + methodId;
#else
            var dynamicMethodName = "Create" + methodId;
#endif
            var dynamicMethod = new DynamicMethod(dynamicMethodName, typeof(T), DynamicMethodParametersTypes, GetType(), true);
            var generator = dynamicMethod.GetILGenerator();
            var emitter = generator.CreateEmitter();
            var ctx = new EmitContext(key, container, _dependencyEmitter, emitter, new EmitStatistics());

            var resources = new List<IDisposable> {emitter};

            if (lifetime == null)
            {
                Emmit<T>(ctx, dependency);
            }
            else
            {
                var resolverHolder = Generate<T>(key, container, dependency);
                resources.Add(resolverHolder);
                ctx.Emitter
                    .Ldobj(lifetime)
                    .Ldobj(ctx.Key)
                    .Ldarg(Arguments.Container)
                    .Ldarg(Arguments.Args)
                    .Ldobj(resolverHolder.Resolve);
                emitter.Call(LifetimeMethodInfo<T>.Shared);
                ctx.Emitter.Ret();
            }

            var factoryMethod = (Resolver<T>)dynamicMethod.CreateDelegate(typeof(Resolver<T>));
            return new ResolverHolder<T>(factoryMethod, Disposable.Create(resources), ctx.Statistics.Dependencies);
        }

        private void Emmit<T>(EmitContext ctx, IDependency dependency)
        {
            var emitResult = _dependencyEmitter.Emit(ctx, dependency);
            if (typeof(T) == typeof(object))
            {
                if (emitResult.TypeInfo.IsValueType)
                {
                    ctx.Emitter.Box(emitResult.TypeInfo.Type);
                }
            }
            else
            {
                if (!emitResult.TypeInfo.IsValueType)
                {
                    ctx.Emitter.Unbox_Any(typeof(T));
                }
            }

            ctx.Emitter.Ret();
        }

        private static class LifetimeMethodInfo<T>
        {
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public static readonly MethodInfo Shared = typeof(ILifetime).Info().DeclaredMethods.Single(i => i.Name == "GetOrCreate").MakeGenericMethod(typeof(T));
        }
    }
}
