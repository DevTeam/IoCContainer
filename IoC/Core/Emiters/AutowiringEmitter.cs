namespace IoC.Core.Emiters
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Dependencies;

    internal sealed class AutowiringEmitter: IEmitter<Autowiring>
    {
        public static readonly IEmitter<Autowiring> Shared = new AutowiringEmitter();

        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public EmitResult Emit(EmitContext ctx, Autowiring autowiring)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (autowiring == null) throw new ArgumentNullException(nameof(autowiring));
            var typeInfo = ctx.Key.Type.Info();
            var constructor = autowiring.Constructor(typeInfo.GenericTypeArguments);
            var constructorInfo = constructor.Info;
            if (typeInfo.IsGenericTypeDefinition)
            {
                throw new ArgumentException("Cannot create object using a generic defenition type.");
            }

            for (var dependencyIndex = 0; dependencyIndex < constructor.Dependencies.Length; dependencyIndex++)
            {
                var dependency = constructor.Dependencies[dependencyIndex];
                ctx.DependencyEmitter.Emit(ctx, dependency);
            }

            ctx.Emitter.Newobj(constructorInfo);
            var methods = autowiring.Methods(typeInfo.GenericTypeArguments);
            if (methods.Length > 0)
            {
                var instanceLocal = ctx.Emitter.DeclareLocal(typeInfo.Type);
                ctx.Emitter.Stloc(instanceLocal);
                for (var methodIndex = 0; methodIndex < methods.Length; methodIndex++)
                {
                    var method = methods[methodIndex];
                    ctx.Emitter.Ldloc(instanceLocal);
                    for (var dependencyIndex = 0; dependencyIndex < method.Dependencies.Length; dependencyIndex++)
                    {
                        ctx.DependencyEmitter.Emit(ctx, method.Dependencies[dependencyIndex]);
                    }

                    var methodInfo = method.Info;
                    ctx.Emitter.Call(methodInfo);
                    if (methodInfo.ReturnType != typeof(void))
                    {
                        ctx.Emitter.Pop();
                    }
                }

                ctx.Emitter.Ldloc(instanceLocal);
            }

            return new EmitResult(typeInfo);
        }
    }
}
