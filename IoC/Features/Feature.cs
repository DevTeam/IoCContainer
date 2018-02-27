namespace IoC.Features
{
    using System.Collections.Generic;

    /// <summary>
    /// Provides defaults for features.
    /// </summary>
    internal static class Feature
    {
        /// <summary>
        /// The enumeration of default features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> Defaults = new[]
        {
            CoreFeature.Shared,
            EnumerableFeature.Shared,
            FuncFeature.Shared,
            TaskFeature.Shared,
            TupleFeature.Shared,
            LazyFeature.Shared,
            ConfigurationFeature.Shared
        };
    }
}
