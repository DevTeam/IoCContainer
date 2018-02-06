namespace IoC.Core
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Emitters;

    internal sealed class ResolverGenerator : IResolverGenerator
    {
        public static readonly IResolverGenerator Shared = new ResolverGenerator(DependencyEmitter.Shared, LifetimeEmitter.Shared);
        [NotNull] private readonly IDependencyEmitter<IDependency> _dependencyEmitter;
        [NotNull] private readonly ILifetimeEmitter _lifetimeEmitter;

        public ResolverGenerator(
            [NotNull] IDependencyEmitter<IDependency> dependencyEmitter,
            [NotNull] ILifetimeEmitter lifetimeEmitter)
        {
            _dependencyEmitter = dependencyEmitter ?? throw new ArgumentNullException(nameof(dependencyEmitter));
            _lifetimeEmitter = lifetimeEmitter ?? throw new ArgumentNullException(nameof(lifetimeEmitter));
        }

        public ResolverHolder<T> Generate<T>(Key key, IContainer container, IDependency dependency, ILifetime lifetime = null)
        {
            var targetTypeInfo = key.Type.Info();
            if (targetTypeInfo.IsGenericTypeDefinition)
            {
                throw new ArgumentException($"The type {typeof(T)} is generic type definition and cannot be constructed.");
            }

            var emitter = new Emitter(Arguments.ResolverParameters);
            var ctx = new EmitContext(key, container, _dependencyEmitter, _lifetimeEmitter, emitter, new EmitStatistics());

            // Emit resolver
            _dependencyEmitter.Emit(ctx, dependency);
            if (ctx.Emitter.LocalVars.Any())
            {
                ctx.Emitter.Block(1, ctx.Emitter.LocalVars.ToArray());
            }

            // Emit lifetime
            if (lifetime != null)
            {
                _lifetimeEmitter.Emit(ctx, lifetime);
            }

            var body = emitter.Pop();
            if (!targetTypeInfo.IsAssignableFrom(body.Type.Info()))
            {
                emitter
                    .Push(body)
                    .Cast(targetTypeInfo.Type);
                body = emitter.Pop();
            }

            var expression = Expression.Lambda<Resolver<T>>(body, true, Arguments.ResolverParameters);
            return new ResolverHolder<T>(expression.Compile());
        }
    }
}
