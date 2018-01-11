﻿namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class ResolveAsync
    {
        [Fact]
        public async void Run()
        {
            // $visible=true
            // $group=01
            // $priority=02
            // $description=Asynchronous Resolve
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>())
            {
                // Resolve the instance asynchronously
                var instance = await container.AsyncGet<IService>(TaskScheduler.Default);

                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
