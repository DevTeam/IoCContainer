namespace IoC.Core
{
    using System;
    using System.Collections.Generic;

    internal sealed class ConfigurationFromDelegate : IConfiguration
    {
        [NotNull] private readonly Func<IContainer, IToken> _configurationFactory;

        public ConfigurationFromDelegate([NotNull] Func<IContainer, IToken> configurationFactory) => 
            _configurationFactory = configurationFactory ?? throw new ArgumentNullException(nameof(configurationFactory));

        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return _configurationFactory(container);
        }
    }
}
