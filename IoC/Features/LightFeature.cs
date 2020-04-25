namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Adds a set of all bundled features.
    /// </summary>
    [PublicAPI]
    public class LightFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new LightFeature();

        private static readonly IEnumerable<IConfiguration> Features = new[]
        {
            CoreFeature.Set,
            CollectionFeature.Set,
            FuncFeature.LightSet,
            TaskFeature.Set,
            TupleFeature.LightSet,
            CommonTypesFeature.Set,
            ConfigurationFeature.Set
        };

        private LightFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return (container ?? throw new ArgumentNullException(nameof(container))).Apply(Features);
        }
    }
}
