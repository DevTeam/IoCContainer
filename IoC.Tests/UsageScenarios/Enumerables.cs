namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using Xunit;

    public class Enumerables
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=3 BCL types
            // $priority=01
            // $description=Enumerables
            // $header=To resolve all possible instances of any tags of the specific type as an _enumerable_ just use the injection _IEnumerable<T>_.
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
            var instances = container.Resolve<IEnumerable<IService>>().ToList();

            // Check the number of resolved instances
            instances.Count.ShouldBe(3);
            // }
            // Check each instance
            instances.ForEach(instance => instance.ShouldBeOfType<Service>());
        }
    }
}
