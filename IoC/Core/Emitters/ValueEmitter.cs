namespace IoC.Core.Emitters
{
    using System;
    using Dependencies;

    internal sealed class ValueEmitter: IDependencyEmitter<Value>
    {
        public static readonly IDependencyEmitter<Value> Shared = new ValueEmitter();

        public void Emit(EmitContext ctx, Value value)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (value == null) throw new ArgumentNullException(nameof(value));
            ctx.Emitter.LoadConst(value.Type, value.ValueObject);
        }
    }
}
