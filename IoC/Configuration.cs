﻿namespace IoC
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
    public static class Configuration
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
        public static IToken AsTokenOf([NotNull] this IDisposable disposableToken, [NotNull] IContainer container) =>
            new Token(container ?? throw new ArgumentNullException(nameof(container)), disposableToken ?? throw new ArgumentNullException(nameof(disposableToken)));

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.ApplyData(configurationText);
        }

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Apply(configurationText);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.ApplyData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Apply(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.ApplyData(configurationReaders);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return token.Container.Apply(configurationReaders);
        }

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.UsingData(configurationText);
        }

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using([NotNull] this IToken token, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using(configurationText);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.UsingData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using([NotNull] this IToken token, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.UsingData(configurationReaders);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="token">The target container token.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using([NotNull] this IToken token, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using(configurationReaders);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations)
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
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Apply(configurations);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The dependency token.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IToken Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
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
        public static IToken Apply([NotNull] this IToken token, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Apply(configurations);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
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
        public static IContainer Using([NotNull] this IToken token, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using(configurations);
        }

        /// <summary>
        /// Applies configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using<T>([NotNull] this IContainer container)
            where T : IConfiguration, new()
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Using(new T());
        }

        /// <summary>
        /// Applies configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="token">The target container token.</param>
        /// <returns>The target container.</returns>
        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static IContainer Using<T>([NotNull] this IToken token)
            where T : IConfiguration, new()
        {
            if (token == null) throw new ArgumentNullException(nameof(token));
            return token.Container.Using<T>();
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        private static IToken ApplyData<T>([NotNull] this IContainer container, [NotNull] [ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Apply(configurationData.Select(configurationItem => container.Resolve<IConfiguration>(configurationItem)).ToArray());
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        private static IContainer UsingData<T>([NotNull] this IContainer container, [NotNull] [ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Using(configurationData.Select(configurationItem => container.Resolve<IConfiguration>(configurationItem)).ToArray());
        }
    }
}