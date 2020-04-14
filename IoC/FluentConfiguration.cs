namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using Core;

    /// <summary>
    /// Represents extensions to configure a container.
    /// </summary>
    [PublicAPI]
    public static class FluentConfiguration
    {
        /// <summary>
        /// Creates configuration from factory.
        /// </summary>
        /// <param name="configurationFactory">The configuration factory.</param>
        /// <returns>The configuration instance.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IConfiguration Create([NotNull] Func<IContainer, IToken> configurationFactory) =>
            new ConfigurationFromDelegate(configurationFactory ?? throw new ArgumentNullException(nameof(configurationFactory)));

        /// <summary>
        /// Converts a disposable resource to the container's token.
        /// </summary>
        /// <param name="disposableToken">A disposable resource.</param>
        /// <param name="container">The target container.</param>
        /// <returns></returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken AsTokenOf([NotNull] this IDisposable disposableToken, [NotNull] IMutableContainer container) =>
            new Token(container ?? throw new ArgumentNullException(nameof(container)), disposableToken ?? throw new ArgumentNullException(nameof(disposableToken)));

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.ApplyConfigurationFromData(configurationText);
        }

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params string[] configurationText) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurationText);

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.ApplyConfigurationFromData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params Stream[] configurationStreams) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurationStreams);

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.ApplyConfigurationFromData(configurationReaders);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params TextReader[] configurationReaders) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurationReaders);

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            return Disposable.Create(configurations.Select(i => i.Apply(container)).SelectMany(i => i).ToArray()).AsTokenOf(container);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurations);

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            return container.Apply((IEnumerable<IConfiguration>) configurations);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params IConfiguration[] configurations) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply(configurations);

        /// <summary>
        /// Applies a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The target container token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply<T>([NotNull] this IMutableContainer container)
            where T : IConfiguration, new() =>
            (container ?? throw new ArgumentNullException(nameof(container))).Apply(new T());

        /// <summary>
        /// Applies a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="token">The target container token.</param>
        /// <returns>The target container token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply<T>([NotNull] this IToken token)
            where T : IConfiguration, new() =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Apply<T>();

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IMutableContainer Using([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            container.RegisterResource(container.Apply(configurations));
            return container;
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IMutableContainer Using([NotNull] this IToken token, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using(configurations);
        }

        /// <summary>
        /// Uses a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IMutableContainer Using<T>([NotNull] this IMutableContainer container)
            where T : IConfiguration, new()
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Using(new T());
        }

        /// <summary>
        /// Uses a configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="token">The target container token.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IMutableContainer Using<T>([NotNull] this IToken token)
            where T : IConfiguration, new()
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using<T>();
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        private static IToken ApplyConfigurationFromData<T>([NotNull] this IMutableContainer container, [NotNull] [ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Apply(configurationData.Select(configurationItem => container.Resolve<IConfiguration>(configurationItem)).ToArray());
        }
    }
}