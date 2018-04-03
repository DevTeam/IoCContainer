namespace WpfApp
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using VewModels;

    /// <summary>
    /// Design time IoC configuration.
    /// </summary>
    internal class ConfigurationDesignTime: IConfiguration
    {
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            // Design time View Models
            yield return container.Bind<IClockViewModel>().To<ClockViewModelDesignTime>();
        }
    }
}
