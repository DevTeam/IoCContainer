namespace IoC.Features
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides defaults for features.
    /// </summary>
    internal static class Feature
    {
        public static readonly object[] AnyTag = { Key.AnyTag };

        /// <summary>
        /// The enumeration of default features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> BasicSet = new[]
        {
            CoreFeature.Shared
        };

        /// <summary>
        /// The enumeration of default features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> DefaultSet = new[]
        {
            CoreFeature.Shared,
            CollectionFeature.Shared,
            FuncFeature.Shared,
            TaskFeature.Shared,
            TupleFeature.Shared,
            LazyFeature.Shared,
            ConfigurationFeature.Shared
        };
    }
}
