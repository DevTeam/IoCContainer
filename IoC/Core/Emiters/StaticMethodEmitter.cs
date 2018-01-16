namespace IoC.Core.Emiters
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Dependencies;

    internal sealed class StaticMethodEmitter : IEmitter<StaticMethod>
    {
        public static readonly IEmitter<StaticMethod> Shared = new StaticMethodEmitter();

        [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
        public EmitResult Emit(EmitContext ctx, StaticMethod method)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (method == null) throw new ArgumentNullException(nameof(method));
            for (var dependencyIndex = 0; dependencyIndex < method.Dependencies.Length; dependencyIndex++)
            {
                ctx.DependencyEmitter.Emit(ctx, method.Dependencies[dependencyIndex]);
            }

            ctx.Emitter.Call(method.Info);
            return new EmitResult(method.TypeInfo);
        }
    }
}
