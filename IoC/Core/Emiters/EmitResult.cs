namespace IoC.Core.Emiters
{
    using System;

    internal struct EmitResult
    {
        [NotNull] public readonly ITypeInfo TypeInfo;

        public EmitResult([NotNull] ITypeInfo typeInfo)
        {
            TypeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
        }
    }
}
