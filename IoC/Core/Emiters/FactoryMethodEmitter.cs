namespace IoC.Core.Emiters
{
    using System;
    using System.Reflection;
    using Dependencies;

    internal sealed class FactoryMethodEmitter: IEmitter<FactoryMethod>
    {
        public static readonly IEmitter<FactoryMethod> Shared = new FactoryMethodEmitter();

        public EmitResult Emit(EmitContext ctx, FactoryMethod factoryMethod)
        {
            if (factoryMethod == null) throw new ArgumentNullException(nameof(factoryMethod));
            if (factoryMethod == null) throw new ArgumentNullException(nameof(factoryMethod));
            var methodInfo = factoryMethod.FactoryMethodDelegate.GetMethodInfo();
            if (factoryMethod.FactoryMethodDelegate.Target != null)
            {
                ctx.Emitter.Ldobj(factoryMethod.FactoryMethodDelegate.Target);
            }

            ctx.Emitter
                .Ldobj(ctx.Key)
                .Ldarg(Arguments.Container)
                .Ldarg(Arguments.Args)
                .Call(methodInfo);

            if (methodInfo.ReturnType == typeof(object))
            {
                if (factoryMethod.TypeInfo.IsValueType)
                {
                    ctx.Emitter.Unbox_Any(factoryMethod.TypeInfo.Type);
                }
            }

            return new EmitResult(factoryMethod.TypeInfo);
        }
    }
}
