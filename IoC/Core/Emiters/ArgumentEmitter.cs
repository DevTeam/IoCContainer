namespace IoC.Core.Emiters
{
    using System;
    using Dependencies;

    internal sealed class ArgumentEmitter: IEmitter<Argument>
    {
        public static readonly IEmitter<Argument> Shared = new ArgumentEmitter();

        public EmitResult Emit(EmitContext ctx, Argument argument)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (argument == null) throw new ArgumentNullException(nameof(argument));
            ctx.Emitter
                .Ldarg(Arguments.Args)
                .Ldc(argument.ArgIndex)
                .Ldelem_Ref();
            return new EmitResult(Type<object>.Info);
        }
    }
}
