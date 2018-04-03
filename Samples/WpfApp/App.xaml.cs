namespace WpfApp
{
    using System.Windows;
    using IoC;
    using Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        internal IContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Container = IoC.Container.Create().Using<Configuration>();
            Container.Resolve<IMainWindowView>().Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Container.Dispose();
            base.OnExit(e);
        }
    }
}
