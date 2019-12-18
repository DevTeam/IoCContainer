namespace XamarinXaml
{
    using System.Collections.Generic;
    using IoC;
    using SampleModels.VewModels;
    using Views;
    using Xamarin.Forms;
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
                .Bind<Page>().As(Singleton).To<MainPage>();
        }
    }
}
