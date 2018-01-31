namespace IoC.Core.Emitters
{
    using System;
    using System.Reflection;
    using Dependencies;

    internal sealed class FactoryMethodEmitter: IDependencyEmitter<FactoryMethod>
    {
        public static readonly IDependencyEmitter<FactoryMethod> Shared = new FactoryMethodEmitter();

        public void Emit(EmitContext ctx, FactoryMethod factoryMethod)
        {
            if (factoryMethod == null) throw new ArgumentNullException(nameof(factoryMethod));
            if (factoryMethod == null) throw new ArgumentNullException(nameof(factoryMethod));
            var methodInfo = factoryMethod.FactoryMethodDelegate.GetMethodInfo();
            if (factoryMethod.FactoryMethodDelegate.Target != null)
            {
                ctx.Emitter.LoadConst(factoryMethod.FactoryMethodDelegate.Target.GetType(), factoryMethod.FactoryMethodDelegate.Target);
            }

            ctx.Emitter
                .LoadConst(ctx.Key)
                .LoadArg(Arguments.Container)
                .LoadArg(Arguments.Args)
                .Call(methodInfo);

            var typeInfo = ctx.Key.Type.Info();
            if (typeInfo.Type != typeof(object))
            {
                ctx.Emitter.Cast(typeInfo.Type);
            }
        }
    }
}
