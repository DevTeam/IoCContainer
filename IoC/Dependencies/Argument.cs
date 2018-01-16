namespace IoC.Dependencies
{
    using System;
    using Core;

    [PublicAPI]
    public sealed class Argument : IDependency
    {
        [NotNull] internal readonly ITypeInfo TypeInfo;
        public readonly int ArgIndex;

        internal Argument(
            [NotNull] ITypeInfo typeInfo,
            int argIndex)
        {
            TypeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
            if (argIndex < 0) throw new ArgumentOutOfRangeException(nameof(argIndex));
            ArgIndex = argIndex;
        }

        public Type Type => TypeInfo.Type;
    }
}
