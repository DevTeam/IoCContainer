namespace IoC.Core.Emitters
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using IoC.Lifetimes;

    internal class LifetimeEmitter : ILifetimeEmitter
    {
        public static readonly ILifetimeEmitter Shared = new LifetimeEmitter();

        public void Emit(EmitContext ctx, ILifetime lifetime)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            if (lifetime is IEmitable emitter)
            {
                var body = ctx.Emitter.Pop();
                ctx.Emitter.Push(emitter.Emit(body));
            }
            else
            {
                var lifetimeType = lifetime.GetType();
                var body = ctx.Emitter.Pop();
                var lifetimeGetOrCreateMethod = lifetimeType
                    .Info()
                    .DeclaredMethods.Single(i => i.Name == "GetOrCreate")
                    .MakeGenericMethod(body.Type);

                var resolverType = typeof(Resolver<>).MakeGenericType(body.Type);
                var resolver = Expression.Lambda(resolverType, body, true, Arguments.ResolverArgsuments).Compile();
                ctx.Emitter
                    .LoadConst(lifetimeType, lifetime)
                    .LoadArg(Arguments.Container)
                    .LoadArg(Arguments.Args)
                    .LoadConst(resolverType, resolver)
                    .Call(lifetimeGetOrCreateMethod);
            }
        }
    }
}