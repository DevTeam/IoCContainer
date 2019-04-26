namespace IoC
{
    using System;
    using Core;

    /// <summary>
    /// Represents extensions to build up from the container.
    /// </summary>
    [PublicAPI]
    public static class FluentBuild
    {
        /// <summary>
        /// Buildups an instance.
        /// </summary>
        /// <param name="configuration">The configurations.</param>
        /// <param name="args">The optional arguments.</param>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <returns>The disposable instance holder.</returns>
        public static IHolder<TInstance> BuildUp<TInstance>([NotNull] this IConfiguration configuration, [NotNull] [ItemCanBeNull] params object[] args) =>
            Container.Create().Using(configuration ?? throw new ArgumentNullException(nameof(configuration))).BuildUp<TInstance>(args ?? throw new ArgumentNullException(nameof(args)));

        /// <summary>
        /// Buildups an instance.
        /// Registers the instance type in the container if it is required, resolves the instance and removes the registration from the container immediately if it was registered here.
        /// </summary>
        /// <typeparam name="TInstance">The instance type.</typeparam>
        /// <param name="container">The target container.</param>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The disposable instance holder.</returns>
        public static IHolder<TInstance> BuildUp<TInstance>([NotNull] this IContainer container, [NotNull] [ItemCanBeNull] params object[] args)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (args == null) throw new ArgumentNullException(nameof(args));

            if (container.TryGetResolver<TInstance>(typeof(TInstance), null, out var resolver, out _, container))
            {
                return new Holder<TInstance>(Disposable.Empty, resolver(container, args));
            }

            var buildId = Guid.NewGuid();
            var childContainer = container.Bind<TInstance>().Tag(buildId).To();
            try
            {
                var instance = container.Resolve<TInstance>(buildId.AsTag(), args);
                return new Holder<TInstance>(childContainer, instance);
            }
            catch
            {
                childContainer.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Represents a holder for a created instance.
        /// </summary>
        /// <typeparam name="TInstance"></typeparam>
        public interface IHolder<out TInstance>: IDisposable
        {            
            /// <summary>
            /// The created instance.
            /// </summary>
            TInstance Instance { get; }
        }   

        internal class Holder<TInstance> : IHolder<TInstance>
        {
            [NotNull] private readonly IDisposable _container;
            
            public Holder([NotNull] IDisposable container, TInstance instance)
            {
                _container = container ?? throw new ArgumentNullException(nameof(container));
                Instance = instance;
            }

            public TInstance Instance { get; }

            public void Dispose()
            {
                if (Instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                _container.Dispose();
            }
        }
    }
}