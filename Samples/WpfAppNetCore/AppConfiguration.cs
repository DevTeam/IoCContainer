namespace WpfAppNetCore
{
    using System.Collections.Generic;
    using IoC;
    using SampleModels.VewModels;
    using Views;
    using static IoC.Lifetime;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    internal class AppConfiguration: IConfiguration
    {
        public IEnumerable<IToken> Apply(IContainer container)
        {
            yield return container
                .Bind<IUIDispatcher>().As(Singleton).To<UIDispatcher>()
                .Bind<IMainWindowView>().As(Singleton).To<MainWindow>();
        }
    }
}
