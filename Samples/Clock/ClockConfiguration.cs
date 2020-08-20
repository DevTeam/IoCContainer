﻿namespace Clock
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using Models;
    using ViewModels;
    using static IoC.Lifetime;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    public class ClockConfiguration: IConfiguration
    {
       public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            // View Models
            yield return container
                .Bind<IClockViewModel>().As(Singleton).To<ClockViewModel>();

            // Models
            yield return container
                .Bind<ITimer>().As(Singleton).To<Timer>(ctx => new Timer(TimeSpan.FromSeconds(1)))
                .Bind<IClock>().As(Singleton).To<Clock>();
        }
    }
}
