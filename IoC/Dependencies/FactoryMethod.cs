namespace IoC.Dependencies
{
    using System;
    using Core;

    [PublicAPI]
    public sealed class FactoryMethod: IDependency
    {
        [NotNull] internal readonly ITypeInfo TypeInfo;
        [NotNull] public readonly Delegate FactoryMethodDelegate;

        internal FactoryMethod(
            [NotNull] ITypeInfo typeInfo,
            [NotNull] Delegate factoryMethodDelegate)
        {
            TypeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
            FactoryMethodDelegate = factoryMethodDelegate ?? throw new ArgumentNullException(nameof(factoryMethodDelegate));
        }

        public Type Type => TypeInfo.Type;
    }
}
