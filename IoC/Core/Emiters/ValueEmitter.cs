namespace IoC.Core.Emiters
{
    using System;
    using Dependencies;

    internal sealed class ValueEmitter: IEmitter<Value>
    {
        public static readonly IEmitter<Value> Shared = new ValueEmitter();

        public EmitResult Emit(EmitContext ctx, Value value)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (value == null) throw new ArgumentNullException(nameof(value));
            var typeInfo = value.TypeInfo;
            var type = value.TypeInfo.Type;
            var valueObject = value.ValueObject;

            var emmiter = ctx.Emitter;
            if (typeof(int) == type)
            {
                emmiter.Ldc((int)(valueObject ?? default(int)));
            }
            else
            if (typeof(long) == type)
            {
                emmiter.Ldc((long)(valueObject ?? default(long)));
            }
            else
            if (typeof(float) == type)
            {
                emmiter.Ldc((float)(valueObject ?? default(float)));
            }
            else
            if (typeof(double) == type)
            {
                emmiter.Ldc((double)(valueObject ?? default(double)));
            }
            else
            if (typeof(string) == type)
            {
                emmiter.Ldstr((string)valueObject);
            }
            else
            if (!typeInfo.IsValueType)
            {
                emmiter.Ldobj(valueObject);
            }
            else
            {
                emmiter.Ldobj(valueObject);
                emmiter.Unbox_Any(typeInfo.Type);
            }

            return new EmitResult(typeInfo);
        }
    }
}
