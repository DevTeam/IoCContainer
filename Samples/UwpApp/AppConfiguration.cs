// ReSharper disable RedundantTypeArgumentsOfMethod
namespace UwpApp
{
    using System.Collections.Generic;
    using Clock.ViewModels;
    using IoC;
    using Views;
    using static IoC.Lifetime;

    /// <summary>
    /// IoC Configuration.
    /// </summary>
    internal class AppConfiguration: IConfiguration
    {
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return container
                .Bind<IDispatcher>().As(Singleton).To<Dispatcher>()
                .Bind<MainPage>().As(Singleton).To<MainPage>();
        }
    }
}
