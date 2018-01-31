namespace IoC.Core.Emitters
{
    using System;
    using Dependencies;

    internal sealed class ArgumentEmitter: IDependencyEmitter<Argument>
    {
        public static readonly IDependencyEmitter<Argument> Shared = new ArgumentEmitter();

        public void Emit(EmitContext ctx, Argument argument)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (argument == null) throw new ArgumentNullException(nameof(argument));
            ctx.Emitter
                .LoadArg(Arguments.Args)
                .LoadConst(argument.ArgIndex)
                .LoadElem()
                .Cast(argument.Type);
        }
    }
}
