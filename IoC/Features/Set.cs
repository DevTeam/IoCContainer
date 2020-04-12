namespace IoC.Features
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides defaults for features.
    /// </summary>
    [PublicAPI]
    public static class Set
    {
        internal static readonly object[] AnyTag = { Key.AnyTag };

        /// <summary>
        /// Core features.
        /// </summary>
        [PublicAPI]
        public static readonly IEnumerable<IConfiguration> Core = new[]
        {
            CoreFeature.Default
        };

        /// <summary>
        /// Default features.
        /// </summary>
        [PublicAPI]
        public static readonly IEnumerable<IConfiguration> Default = new[]
        {
            CoreFeature.Default,
            CollectionFeature.Default,
            FuncFeature.Default,
            TaskFeature.Default,
            TupleFeature.Default,
            LazyFeature.Default,
            ConfigurationFeature.Default
        };

        /// <summary>
        /// The light set of features.
        /// </summary>
        [PublicAPI]
        public static readonly IEnumerable<IConfiguration> Light = new[]
        {
            CoreFeature.Default,
            CollectionFeature.Default,
            FuncFeature.Light,
            TaskFeature.Default,
            TupleFeature.Light,
            LazyFeature.Default,
            ConfigurationFeature.Default
        };
    }
}
