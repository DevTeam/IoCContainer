namespace IoC.Dependencies
{
    using System;
    using System.Reflection;

    [PublicAPI]
    public sealed class Constructor
    {
        [NotNull] public readonly ConstructorInfo Info;
        [NotNull] [ItemNotNull] public readonly IDependency[] Dependencies;

        internal Constructor(
            [NotNull] ConstructorInfo constructorInfo,
            [NotNull][ItemNotNull] params IDependency[] dependencies)
        {
            Info = constructorInfo ?? throw new ArgumentNullException(nameof(constructorInfo));
            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            if (constructorInfo.GetParameters().Length != dependencies.Length)
            {
                throw new ArgumentException($"The number of elenemts in the array \"{nameof(dependencies)}\" should be the same as the number of parameters in the constructor {nameof(constructorInfo)}");
            }
        }
    }
}
