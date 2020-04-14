// ReSharper disable RedundantTypeArgumentsOfMethod
namespace WcfServiceLibrary
{
    using System.Collections.Generic;
    using IoC;

    internal class Configuration: IConfiguration
    {
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return container.Bind<Service, IService>().To<Service>();
        }
    }
}