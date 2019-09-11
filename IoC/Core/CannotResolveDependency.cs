namespace IoC.Core
{
    using System;
    using Issues;

    internal class CannotResolveDependency : ICannotResolveDependency
    {
        public static readonly ICannotResolveDependency Shared = new CannotResolveDependency();

        private CannotResolveDependency() { }

        public DependencyDescription Resolve(IContainer container, Key key)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot find the dependency for the key {key} in the container {container}.");
        }
    }
}