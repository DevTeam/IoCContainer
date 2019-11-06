namespace SampleModels
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using VewModels;

    /// <summary>
    /// Design time IoC configuration.
    /// </summary>
    public class ClockDesignTimeConfiguration: IConfiguration
    {
        public static readonly IConfiguration Shared = new ClockDesignTimeConfiguration();

        private ClockDesignTimeConfiguration() { }

        public IEnumerable<IToken> Apply(IContainer container)
        {
            // Design time View Models
            yield return container.Bind<IClockViewModel>().To<ClockViewModelDesignTime>();
        }
    }
}
