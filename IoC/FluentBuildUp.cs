namespace IoC
{
    using System;

    /// <summary>
    /// Represents extensions to build up from the container.
    /// </summary>
    [PublicAPI]
    public static class FluentBuildUp
    {
        /// <summary>
        /// Buildups an instance.
        /// Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.
        /// </summary>
        /// <typeparam name="T">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The instance.</returns>
        public static T BuildUp<T>(this IContainer container, [NotNull] [ItemCanBeNull] params object[] args)
        {
            if (container.TryGetResolver<T>(typeof(T), null, out var resolver, out _, container))
            {
                return resolver(container, args);
            }

            var buildId = Guid.NewGuid();
            using(container.Bind<T>().Tag(buildId).To())
            {
                return container.Resolve<T>(buildId.AsTag(), args);
            }
        }
    }
}