namespace SampleModels
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using Models;
    using VewModels;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    public class Clock: IConfiguration
    {
        public static readonly IConfiguration Shared = new Clock();
        private Clock() { }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            // View Models
            yield return container.Bind<IClockViewModel>().As(Lifetime.Singleton).To<ClockViewModel>();

            // Models
            yield return container.Bind<ITimer>().As(Lifetime.Singleton).To(ctx => new Timer(TimeSpan.FromSeconds(1)));
            yield return container.Bind<IClock>().As(Lifetime.Singleton).To<SampleModels.Models.Clock>();
        }
    }
}
