namespace IoC.Dependencies
{
    using System;
    using Core;

    [PublicAPI]
    public sealed class Autowiring: IDependency
    {
        [NotNull] private static readonly Method[] EmptyMethods = new Method[0];
        [NotNull] private static readonly MethodsSelector EmptyMethodSelector = types => EmptyMethods;
        [NotNull] internal readonly ITypeInfo TypeInfo;
        [NotNull] public readonly ConstructorSelector Constructor;
        [NotNull] public readonly MethodsSelector Methods;

        internal Autowiring(
            [NotNull] ITypeInfo typeInfo,
            [NotNull] ConstructorSelector constructorSelector,
            [NotNull] MethodsSelector methodsSelector)
        {
            TypeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
            Constructor = constructorSelector ?? throw new ArgumentNullException(nameof(constructorSelector));
            Methods = methodsSelector ?? throw new ArgumentNullException(nameof(methodsSelector));
        }

        public Type Type => TypeInfo.Type;

        [NotNull] public delegate Constructor ConstructorSelector([NotNull] [ItemNotNull] Type[] genericTypeArguments);
        [NotNull] public delegate Method[] MethodsSelector([NotNull] [ItemNotNull] Type[] genericTypeArguments);
    }
}
