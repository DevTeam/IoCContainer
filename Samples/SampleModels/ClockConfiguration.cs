namespace SampleModels
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using Models;
    using VewModels;
    using static IoC.Lifetime;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    public class ClockConfiguration: IConfiguration
    {
        public static readonly IConfiguration Shared = new ClockConfiguration();
        private ClockConfiguration() { }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            // View Models
            yield return container.Bind<IClockViewModel>().As(Singleton).To<ClockViewModel>();

            // Models
            yield return container.Bind<ITimer>().As(Singleton).To(ctx => new Timer(TimeSpan.FromSeconds(1)));
            yield return container.Bind<IClock>().As(Singleton).To<Clock>();
        }
    }
}
