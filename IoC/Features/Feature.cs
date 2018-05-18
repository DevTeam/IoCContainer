namespace IoC.Features
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides defaults for features.
    /// </summary>
    internal static class Feature
    {
        public static readonly object[] AnyTag = { Key.AnyTag };

        /// <summary>
        /// Core features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> CoreSet = new[]
        {
            CoreFeature.Default
        };

        /// <summary>
        /// Default features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> DefaultSet = Combine(
            CoreSet,
            new[]
            {
                CollectionFeature.Default,
                FuncFeature.Default,
                TaskFeature.Default,
                TupleFeature.Default,
                LazyFeature.Default,
                ConfigurationFeature.Default
            });

        /// <summary>
        /// The light set of features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> LightSet = Combine(
            CoreSet,
            new[]
            {
                CollectionFeature.Light,
                FuncFeature.Light,
                TaskFeature.Default,
                TupleFeature.Light,
                LazyFeature.Default,
                ConfigurationFeature.Default
            });

        private static IEnumerable<IConfiguration> Combine(params IEnumerable<IConfiguration>[] configurations)
        {
            return configurations.SelectMany(i => i);
        }
    }
}
