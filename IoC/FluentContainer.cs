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
        public static IComposition<TInstance> Build<TInstance>([NotNull] this IConfiguration configuration, [NotNull] [ItemCanBeNull] params object[] args)
            where TInstance : class
            => Container.Create().Using(configuration ?? throw new ArgumentNullException(nameof(configuration))).Build<TInstance>(args ?? throw new ArgumentNullException(nameof(args)));

        /// <summary>
        /// Build a composition root.
        /// Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.
        /// </summary>
        /// <typeparam name="T">The root instance type.</typeparam>
        /// <param name="token">The target container token.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The disposable instance holder.</returns>
        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static IComposition<T> Build<T>([NotNull] this IToken token, [NotNull] [ItemCanBeNull] params object[] args)
            where T : class =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Build<T>(args ?? throw new ArgumentNullException(nameof(args)));

        /// <summary>
        /// Build a composition root.
        /// Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.
        /// </summary>
        /// <typeparam name="T">The root instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The disposable instance holder.</returns>
        [NotNull]
        [MethodImpl((MethodImplOptions)0x200)]
        public static IComposition<T> Build<T>([NotNull] this IMutableContainer container, [NotNull] [ItemCanBeNull] params object[] args)
            where T : class
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (container.TryGetResolver<T>(out var resolver))
            {
                return new Composition<T>(container, new Token(container, Disposable.Empty), resolver, args);
            }

            if (typeof(T).Descriptor().IsAbstract())
            {
                throw new InvalidOperationException("The composition root must be of a non-abstract type, or must be registered with the container.");
            }

            var token = container.Bind<T>().To();
            try
            {
                return new Composition<T>(container, token, container.GetResolver<T>(), args);
            }
            catch
            {
                token.Dispose();
                throw;
            }
        }
    }
}