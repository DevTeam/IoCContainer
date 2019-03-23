namespace XamarinXaml
{
    using IoC;
    using Xamarin.Forms;

    public partial class App
    {
        internal readonly IContainer Container;

        public App()
        {
            InitializeComponent();
            Container = IoC.Container.Create().Using<Configuration>();
            MainPage = Container.Resolve<Page>();
        }
    }
}
