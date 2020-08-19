namespace Clock
{
    using System.Collections.Generic;
    using IoC;
    using ViewModels;

    /// <summary>
    /// Design time IoC configuration.
    /// </summary>
    public class ClockDesignTimeConfiguration: IConfiguration
    {
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            // Design time View Models
            yield return container
                .Bind<IClockViewModel>().To<ClockViewModelDesignTime>();
        }
    }
}
