namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
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
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().Tag(10).Tag().Tag("abc").To<Service>())
            {
                // Resolve instances using tags
                var instance1 = container.Resolve<IService>("abc".AsTag());
                var instance2 = container.Resolve<IService>(10.AsTag());

                // Resolve the instance using the empty tag
                var instance3 = container.Resolve<IService>();
            }
            // }
        }
    }
}
