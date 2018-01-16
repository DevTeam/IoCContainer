namespace IoC.Dependencies
{
    using System;
    using System.Reflection;

    [PublicAPI]
    public sealed class Method
    {
        [NotNull] public readonly MethodInfo Info;
        [NotNull] [ItemNotNull] public readonly IDependency[] Dependencies;

        internal Method(
            [NotNull] MethodInfo methodInfo,
            [NotNull][ItemNotNull] params IDependency[] dependencies)
        {
            Info = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
            Dependencies = dependencies ?? throw new ArgumentNullException(nameof(dependencies));
            if (methodInfo.GetParameters().Length != dependencies.Length)
            {
                throw new ArgumentException($"The number of elenemts in the array \"{nameof(dependencies)}\" should be the same as the number of parameters in the method {nameof(methodInfo)}");
            }
        }
    }
}
