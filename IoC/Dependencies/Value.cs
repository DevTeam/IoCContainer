namespace IoC.Dependencies
{
    using System;
    using Core;

    [PublicAPI]
    public sealed class Value : IDependency
    {
        [NotNull] internal readonly ITypeInfo TypeInfo;
        [CanBeNull] public readonly object ValueObject;

        internal Value(
            [NotNull] ITypeInfo typeInfo,
            [CanBeNull] object value)
        {
            TypeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
            ValueObject = value;
        }

        public Type Type => TypeInfo.Type;
    }
}
