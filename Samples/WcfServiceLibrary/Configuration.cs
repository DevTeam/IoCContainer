// ReSharper disable RedundantTypeArgumentsOfMethod
namespace WcfServiceLibrary
{
    using System;
    using System.Collections.Generic;
    using IoC;

    internal class Configuration: IConfiguration
    {
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            yield return container.Bind<Service, IService>().To<Service>();
        }
    }
}