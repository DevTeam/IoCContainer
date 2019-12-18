﻿namespace WpfAppNetCore
{
    using System.Windows;
    using IoC;
    using SampleModels;
    using Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        internal readonly IContainer Container = IoC.Container
            .Create()
            .Using<ClockConfiguration>()
            .Using<AppConfiguration>();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container.Resolve<IMainWindowView>().Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Container.Dispose();
            base.OnExit(e);
        }
    }
}
