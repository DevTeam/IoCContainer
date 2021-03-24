#if NET5_0_OR_GREATER || NETCOREAPP3_1
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
            // $tag=4 Async
            // $priority=02
            // $description=Async Enumerables
            // $header=It is easy to resolve an enumerator [IAsyncEnumerable<>](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1) that provides asynchronous iteration over values of a type for every tag.
            // {
            using var container = Container
                .Create()
                .Using(CollectionFeature.Set)
                .Bind<IDependency>().To<Dependency>()
                // Bind to the default implementation
                .Bind<IService>().To<Service>()
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
            items.Count.ShouldBe(4);

            // }
            // Check instances
            items.ForEach(instance => instance.ShouldBeOfType<Service>());
        }
    }
}
#endif