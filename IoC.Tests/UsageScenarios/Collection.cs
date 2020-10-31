namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using Shouldly;
    using Xunit;

    public class Collection
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=3 BCL types
            // $priority=01
            // $description=Collection
            // $header=To resolve all possible instances of any tags of the specific type as a _collection_ just use the injection _ICollection<T>_
            // {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind to the implementation #1
                .Bind<IService>().Tag(1).To<Service>()
                // Bind to the implementation #2
                .Bind<IService>().Tag(2).Tag("abc").To<Service>()
                // Bind to the implementation #3
                .Bind<IService>().Tag(3).To<Service>()
                .Container;

            // Resolve all appropriate instances
            var instances = container.Resolve<ICollection<IService>>();

            // Check the number of resolved instances
            instances.Count.ShouldBe(3);
            // }
            foreach (var instance in instances)
            {
                // Check the instance
                instance.ShouldBeOfType<Service>();
            }
        }
    }
}
