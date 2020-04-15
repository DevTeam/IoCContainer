namespace IoC
{
    using System;
    using Core;

    /// <summary>
    /// Provides autowiring strategies.
    /// </summary>
    public static class AutowiringStrategies
    {
        /// <summary>
        /// Create an aspect oriented autowiring strategy.
        /// </summary>
        /// <returns>The instance of aspect oriented autowiring strategy.</returns>
        public static IAutowiringStrategy AspectOriented() => AspectOrientedMetadata.Empty;

        /// <summary>
        /// Specify a type selector for an aspect oriented autowiring strategy.
        /// </summary>
        /// <typeparam name="TTypeAttribute">The type metadata attribute.</typeparam>
        /// <param name="strategy">The base aspect oriented autowiring strategy.</param>
        /// <param name="typeSelector">The type selector.</param>
        /// <returns>The instance of aspect oriented autowiring strategy.</returns>
        public static IAutowiringStrategy Type<TTypeAttribute>(this IAutowiringStrategy strategy, [NotNull] Func<TTypeAttribute, Type> typeSelector)
            where TTypeAttribute : Attribute =>
            AspectOrientedMetadata.Type(
                GuardAspectOrientedMetadata(strategy ?? throw new ArgumentNullException(nameof(strategy))),
                typeSelector ?? throw new ArgumentNullException(nameof(typeSelector)));

        /// <summary>
        /// Specify an order selector for an aspect oriented autowiring strategy.
        /// </summary>
        /// <typeparam name="TOrderAttribute">The order metadata attribute.</typeparam>
        /// <param name="strategy">The base aspect oriented autowiring strategy.</param>
        /// <param name="orderSelector">The type selector.</param>
        /// <returns>The instance of aspect oriented autowiring strategy.</returns>
        public static IAutowiringStrategy Order<TOrderAttribute>(this IAutowiringStrategy strategy, [NotNull] Func<TOrderAttribute, IComparable> orderSelector)
            where TOrderAttribute : Attribute =>
            AspectOrientedMetadata.Order(
                GuardAspectOrientedMetadata(strategy ?? throw new ArgumentNullException(nameof(strategy))),
                orderSelector ?? throw new ArgumentNullException(nameof(orderSelector)));

        /// <summary>
        /// Specify a tag selector for an aspect oriented autowiring strategy.
        /// </summary>
        /// <typeparam name="TTagAttribute">The tag metadata attribute.</typeparam>
        /// <param name="strategy">The base aspect oriented autowiring strategy.</param>
        /// <param name="tagSelector">The tag selector.</param>
        /// <returns>The instance of aspect oriented autowiring strategy.</returns>
        public static IAutowiringStrategy Tag<TTagAttribute>(this IAutowiringStrategy strategy, [NotNull] Func<TTagAttribute, object> tagSelector)
            where TTagAttribute : Attribute =>
            AspectOrientedMetadata.Tag(
                GuardAspectOrientedMetadata(strategy ?? throw new ArgumentNullException(nameof(strategy))),
                tagSelector ?? throw new ArgumentNullException(nameof(tagSelector)));

        private static AspectOrientedMetadata GuardAspectOrientedMetadata([NotNull] IAutowiringStrategy strategy)
        {
            switch (strategy)
            {
                case AspectOrientedMetadata aspectOrientedMetadata:
                    return aspectOrientedMetadata;
                default:
                    throw new ArgumentException($"{nameof(strategy)} should be an aspect oriented autowiring strategy.", nameof(strategy));
            }
        }
    }
}