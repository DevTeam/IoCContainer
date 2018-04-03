namespace WpfApp
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using Models;
    using VewModels;
    using Views;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    internal class Configuration: IConfiguration
    {
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            // Windows
            yield return container.Bind<IMainWindowView>().As(Lifetime.Singleton).To<MainWindow>();

            // View Models
            yield return container.Bind<IClockViewModel>().As(Lifetime.Singleton).To<ClockViewModel>();

            // Services
            yield return container.Bind<ITimer>().As(Lifetime.Singleton).To(ctx => new Timer(TimeSpan.FromSeconds(1)));
            yield return container.Bind<IClock>().As(Lifetime.Singleton).To<Clock>();
        }
    }
}
