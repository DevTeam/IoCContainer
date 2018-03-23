namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Core;

    /// <summary>
    /// Represents extensons to configure a container.
    /// </summary>
    [PublicAPI]
    public static class FluentConfiguration
    {
        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.ApplyData(configurationText);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.ApplyData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.ApplyData(configurationReaders);
        }

        /// <summary>
        /// Applies text configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationText">The text configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params string[] configurationText)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationText == null) throw new ArgumentNullException(nameof(configurationText));
            if (configurationText.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationText));
            return container.UsingData(configurationText);
        }

        /// <summary>
        /// Applies text configurations from streams for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationStreams">The set of streams with text configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params Stream[] configurationStreams)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationStreams == null) throw new ArgumentNullException(nameof(configurationStreams));
            if (configurationStreams.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationStreams));
            return container.UsingData(configurationStreams);
        }

        /// <summary>
        /// Applies text configurations from text readers for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurationReaders">The set of text readers with text configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params TextReader[] configurationReaders)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationReaders == null) throw new ArgumentNullException(nameof(configurationReaders));
            if (configurationReaders.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationReaders));
            return container.UsingData(configurationReaders);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] IEnumerable<IConfiguration> configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            return Disposable.Create(configurations.Select(i => i.Apply(container)).SelectMany(i => i));
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The registration token.</returns>
        [NotNull]
        public static IDisposable Apply([NotNull] this IContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            return container.Apply((IEnumerable<IConfiguration>) configurations);
        }

        /// <summary>
        /// Applies configurations for the target container.
        /// </summary>
        /// <param name="container">The target container.</param>
        /// <param name="configurations">The configurations.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using([NotNull] this IContainer container, [NotNull] [ItemNotNull] params IConfiguration[] configurations)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurations == null) throw new ArgumentNullException(nameof(configurations));
            if (configurations.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurations));
            container.Resolve<IResourceStore>().AddResource(container.Apply(configurations));
            return container;
        }

        /// <summary>
        /// Applies configuration for the target container.
        /// </summary>
        /// <typeparam name="T">The type of configuration.</typeparam>
        /// <param name="container">The target container.</param>
        /// <returns>The target container.</returns>
        [NotNull]
        public static IContainer Using<T>([NotNull] this IContainer container)
            where T : IConfiguration, new()
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return container.Using(new T());
        }

        [NotNull]
        private static IDisposable ApplyData<T>([NotNull] this IContainer container, [NotNull] [ItemNotNull] params T[] configurationData)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (configurationData == null) throw new ArgumentNullException(nameof(configurationData));
            if (configurationData.Length == 0) throw new ArgumentException("Value cannot be an empty collection.", nameof(configurationData));
            return container.Apply(configurationData.Select(configurationItem => container.Resolve<IConfiguration>(configurationItem)).ToArray());
        }

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