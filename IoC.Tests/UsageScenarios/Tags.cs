namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class Tags
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=01
            // $description=Tags
            // $header=Tags are useful while binding to several implementations.
            // {
            // Create and configure the container
            using (var container = Container.Create())
            // Bind some dependency
            using (container.Bind<IDependency>().To<Dependency>())
            // Bind using several tags
            using (container.Bind<IService>().Tag(10).Tag().Tag("abc").To<Service>())
            {
                // Resolve instances using tags
                var instance1 = container.Resolve<IService>("abc".AsTag());
                var instance2 = container.Resolve<IService>(10.AsTag());

                // Resolve the instance using the empty tag
                var instance3 = container.Resolve<IService>();
                // }
                // Check the instances' types
                instance1.ShouldBeOfType<Service>();
                instance2.ShouldBeOfType<Service>();
                instance3.ShouldBeOfType<Service>();
                // {
            }
            // }
        }
    }
}
