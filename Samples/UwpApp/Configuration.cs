namespace UwpApp
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using SampleModels;
    using SampleModels.Models;
    using SampleModels.VewModels;
    using Views;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    internal class Configuration: IConfiguration
    {
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            yield return container.Apply(Clock.Shared);
            yield return container.Bind<IUIDispatcher>().To<UIDispatcher>();
            yield return container.Bind<MainPage>().As(Lifetime.Singleton).To<MainPage>();           
        }
    }
}
