namespace IoC.Features
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Adds a set of all bundled features.
    /// </summary>
    [PublicAPI]
    public class DefaultFeature: IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Set = new DefaultFeature();

        private static readonly IEnumerable<IConfiguration> Features = new[]
        {
            CoreFeature.Set,
            CollectionFeature.Set,
            FuncFeature.Set,
            TaskFeature.Set,
            TupleFeature.Default,
            CommonTypesFeature.Set,
            ConfigurationFeature.Set
        };

        private DefaultFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return (container ?? throw new ArgumentNullException(nameof(container))).Apply(Features);
        }
    }
}
