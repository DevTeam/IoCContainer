namespace XamarinXaml
{
    using IoC;
    using SampleModels;
    using Xamarin.Forms;

    public partial class App
    {
        internal readonly IMutableContainer Container = IoC.Container
            .Create()
            .Using<ClockConfiguration>()
            .Using<AppConfiguration>();

        public App()
        {
            InitializeComponent();
            MainPage = Container.Resolve<Page>();
        }

        protected override void CleanUp()
        {
            Container.Dispose();
            base.CleanUp();
        }
    }
}
