namespace XamarinXaml
{
    using Clock;
    using IoC;
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
