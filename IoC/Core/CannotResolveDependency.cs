namespace IoC.Core
{
    using System;
    using Issues;

    internal sealed class CannotResolveDependency : ICannotResolveDependency
    {
        public static readonly ICannotResolveDependency Shared = new CannotResolveDependency();

        private CannotResolveDependency() { }

        public DependencyDescription Resolve(IBuildContext buildContext)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            throw new InvalidOperationException($"Cannot find the dependency for the key {buildContext.Key} in the container {buildContext.Container}.\n{buildContext}");
        }
    }
}