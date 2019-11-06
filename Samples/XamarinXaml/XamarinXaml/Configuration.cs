namespace XamarinXaml
{
    using System.Collections.Generic;
    using IoC;
    using SampleModels;
    using SampleModels.VewModels;
    using Views;
    using Xamarin.Forms;
    using static IoC.Lifetime;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    internal class Configuration: IConfiguration
    {
        public IEnumerable<IToken> Apply(IContainer container)
        {
            yield return container.Apply(ClockConfiguration.Shared)
                .Bind<IUIDispatcher>().As(Singleton).To<UIDispatcher>()
                .Bind<Page>().As(Singleton).To<MainPage>();
        }
    }
}
