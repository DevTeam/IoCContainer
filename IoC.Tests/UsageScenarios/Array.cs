namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Array
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=basic
            // $priority=05
            // $description=Array
            // $header=To resolve all possible instances of any tags of the specific type as an _array_ just use the injection _T[]_
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
            var instances = container.Resolve<IService[]>();
            // }
            // Check the number of resolved instances
            instances.Length.ShouldBe(3);

            foreach (var instance in instances)
            {
                // Check the instance
                instance.ShouldBeOfType<Service>();
            }
        }
    }
}
