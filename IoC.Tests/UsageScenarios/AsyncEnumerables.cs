﻿#if NETCOREAPP3_0
namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using Xunit;
    using System.Threading;
    using System.Threading.Tasks;
    using IoC.Features;

    public class AsyncEnumerables
    {
        [Fact]
        public async ValueTask Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=05
            // $description=Resolve all appropriate instances as IAsyncEnumerable
            // {
            // Create and configure the container
            using (var container = Container.CreateCore().Using(CollectionFeature.Default))
            // Bind some dependency
            using (container.Bind<IDependency>().To<Dependency>())
            // Bind to the implementation #1
            using (container.Bind<IService>().Tag(1).To<Service>())
            // Bind to the implementation #2
            using (container.Bind<IService>().Tag(2).Tag("abc").To<Service>())
            // Bind to the implementation #3
            using (container.Bind<IService>().Tag(3).To<Service>())
            {
                // Resolve all appropriate instances
                var instances = container.Resolve<IAsyncEnumerable<IService>>();
                var items = new List<IService>();
                await foreach (var instance in instances) { items.Add(instance); }
                
                // Check the number of resolved instances
                items.Count.ShouldBe(3);

                // }
                // Check the instances' type
                items.ForEach(instance => instance.ShouldBeOfType<Service>());
                // {
            }
            // }
        }
    }
}
#endif