#if NETCOREAPP3_1
namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;
    using System.Threading.Tasks;
    using Features;

    public class AsyncEnumerables
    {
        [Fact]
        public async ValueTask Run()
        {
            // $visible=true
            // $tag=async
            // $priority=05
            // $description=Resolve all appropriate instances as IAsyncEnumerable
            // {
            // Create and configure the container
            using var container = Container
                .CreateCore()
                .Using(CollectionFeature.Default)
                .Bind<IDependency>().To<Dependency>()
                // Bind to the implementation #1
                .Bind<IService>().Tag(1).To<Service>()
                // Bind to the implementation #2
                .Bind<IService>().Tag(2).Tag("abc").To<Service>()
                // Bind to the implementation #3
                .Bind<IService>().Tag(3).To<Service>()
                .Container;

            // Resolve all appropriate instances
            var instances = container.Resolve<IAsyncEnumerable<IService>>();
            var items = new List<IService>();
            await foreach (var instance in instances) { items.Add(instance); }
            
            // Check the number of resolved instances
            items.Count.ShouldBe(3);

            // }
            // Check the instances' type
            items.ForEach(instance => instance.ShouldBeOfType<Service>());
        }
    }
}
#endif