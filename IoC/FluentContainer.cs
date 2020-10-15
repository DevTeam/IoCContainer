namespace IoC
{
    using System;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Extension methods for IoC containers and configurations.
    /// </summary>
    [PublicAPI]
    public static class FluentContainer
    {
        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="parentContainer">The parent container.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Create([NotNull] this IContainer parentContainer, [NotNull] string name = "")
        {
            if (parentContainer == null) throw new ArgumentNullException(nameof(parentContainer));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return parentContainer.Resolve<IMutableContainer>(name);
        }

        /// <summary>
        /// Creates child container.
        /// </summary>
        /// <param name="token">The parent container token.</param>
        /// <param name="name">The name of child container.</param>
        /// <returns>The child container.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IMutableContainer Create([NotNull] this IToken token, [NotNull] string name = "")
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (name == null) throw new ArgumentNullException(nameof(name));
            return token.Container.Resolve<IMutableContainer>(name);
        }

        /// <summary>
        /// Buildups an instance which was not registered in container. Can be used as entry point of DI.
        /// </summary>
        /// <param name="configuration">The configurations.</param>
        /// <param name="args">The optional arguments.</param>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <returns>The disposable instance holder.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static ICompositionRoot<TInstance> BuildUp<TInstance>([NotNull] this IConfiguration configuration, [NotNull] [ItemCanBeNull] params object[] args)
            where TInstance : class
            => Container.Create().Using(configuration ?? throw new ArgumentNullException(nameof(configuration))).BuildUp<TInstance>(args ?? throw new ArgumentNullException(nameof(args)));

        /// <summary>
        /// Buildups an instance.
        /// Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.
        /// </summary>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <param name="token">The target container token.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The disposable instance holder.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static ICompositionRoot<TInstance> BuildUp<TInstance>([NotNull] this IToken token, [NotNull] [ItemCanBeNull] params object[] args)
            where TInstance : class =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.BuildUp<TInstance>(args ?? throw new ArgumentNullException(nameof(args)));

        /// <summary>
        /// Buildups an instance.
        /// Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.
        /// </summary>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The disposable instance holder.</returns>
        [NotNull]
        [MethodImpl((MethodImplOptions)0x200)]
        public static ICompositionRoot<TInstance> BuildUp<TInstance>([NotNull] this IMutableContainer container, [NotNull] [ItemCanBeNull] params object[] args)
            where TInstance : class
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (container.TryGetResolver<TInstance>(out var resolver))
            {
                return new CompositionRoot<TInstance>(new Token(container, Disposable.Empty), resolver(container, args));
            }

            var buildId = Guid.NewGuid();
            var token = container.Bind<TInstance>().Tag(buildId).To();
            try
            {
                var instance = container.Resolve<TInstance>(buildId.AsTag(), args);
                return new CompositionRoot<TInstance>(token, instance);
            }
            catch
            {
                token.Dispose();
                throw;
            }
        }
    }
}