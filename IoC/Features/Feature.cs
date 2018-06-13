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
        /// The enumeration of default features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> BasicSet = new[]
        {
            CoreFeature.Default
        };

        /// <summary>
        /// The enumeration of default features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> DefaultSet = Combine(
            BasicSet,
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
        /// The enumeration of default features.
        /// </summary>
        public static readonly IEnumerable<IConfiguration> HighPerformanceSet = Combine(
            BasicSet,
            new[]
            {
                CollectionFeature.HighPerformance,
                FuncFeature.HighPerformance,
                TaskFeature.Default,
                TupleFeature.HighPerformance,
                LazyFeature.Default
            }
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP2_0 && !NETCOREAPP2_1 && !WINDOWS_UWP
            , new []
            {
                HighPerformanceFeature.Default,
            }
#endif
            );

        private static IEnumerable<IConfiguration> Combine(params IEnumerable<IConfiguration>[] configurations)
        {
            return configurations.SelectMany(i => i);
        }
    }
}
