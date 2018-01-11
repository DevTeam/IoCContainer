namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class DependencyTag
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=05
            // $description=Dependency Tag
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().Tag("MyDep").To<Dependency>())
            using (container.Bind<IService>().To<Service>(Has.Ref("dependency", "MyDep")))
            {
                // Resolve the instance
                var instance = container.Get<IService>();
            }
            // }
        }
    }
}
