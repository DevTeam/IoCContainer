﻿namespace WpfAppNetCore
{
    using System;
    using System.Collections.Generic;
    using IoC;
    using SampleModels;
    using SampleModels.VewModels;
    using Views;
    using static IoC.Lifetime;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    internal class Configuration: IConfiguration
    {
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            yield return container.Apply(ClockConfiguration.Shared);
            yield return container.Bind<IUIDispatcher>().As(Singleton).To<UIDispatcher>();
            yield return container.Bind<IMainWindowView>().As(Singleton).To<MainWindow>();            
        }
    }
}