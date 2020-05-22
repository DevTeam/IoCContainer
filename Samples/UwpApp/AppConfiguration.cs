// ReSharper disable RedundantTypeArgumentsOfMethod
namespace UwpApp
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
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return container
                .Bind<IUIDispatcher>().As(Singleton).To<UIDispatcher>()
                .Bind<MainPage>().As(Singleton).To<MainPage>();
        }
    }
}
