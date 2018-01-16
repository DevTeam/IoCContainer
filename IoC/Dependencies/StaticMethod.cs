namespace IoC.Dependencies
{
    using System;
    using System.Reflection;
    using Core;

    [PublicAPI]
    public sealed class StaticMethod : IDependency
    {
        [NotNull] internal readonly ITypeInfo TypeInfo;
        [NotNull] public readonly MethodInfo Info;
        [NotNull] [ItemNotNull] public readonly IDependency[] Dependencies;

        internal StaticMethod(
            [NotNull] ITypeInfo typeInfo,
            [NotNull] MethodInfo methodInfo,
            [NotNull][ItemNotNull] params IDependency[] dependencies)
        {
            TypeInfo = typeInfo ?? throw new ArgumentNullException(nameof(typeInfo));
            Info = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            if (methodInfo.GetParameters().Length != dependencies.Length)
            {
                throw new ArgumentException($"The number of elenemts in the array \"{nameof(dependencies)}\" should be the same as the number of parameters in the method {nameof(methodInfo)}");
            }
        }

        public Type Type => TypeInfo.Type;

        private static void CheckType(ITypeInfo baseTypeInfo, ITypeInfo typeInfo)
        {
            if (!baseTypeInfo.IsAssignableFrom(typeInfo))
            {
                throw new ArgumentException($"The type {typeInfo.Type.Name} canot be casted to the type {baseTypeInfo.Type.Name}.");
            }
        }
    }
}
