namespace SampleModels
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using VewModels;

    /// <summary>
    /// Design time IoC configuration.
    /// </summary>
    public class ClockDesignTime: IConfiguration
    {
        public static readonly IConfiguration Shared = new ClockDesignTime();

        private ClockDesignTime() { }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            // Design time View Models
            yield return container.Bind<IClockViewModel>().To<ClockViewModelDesignTime>();
        }
    }
}
