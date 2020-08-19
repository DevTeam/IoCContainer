namespace XamarinXaml
{
    using System.Collections.Generic;
    using IoC;
    using Views;
    using Xamarin.Forms;
    using static IoC.Lifetime;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    internal class AppConfiguration: IConfiguration
    {
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return container
                .Bind<Page>().As(Singleton).To<MainPage>();
        }
    }
}
