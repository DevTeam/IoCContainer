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
            // $priority=02
            // $description=Dependency Tag
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            // Mark binding by tag "MyDep"
            using (container.Bind<IDependency>().Tag("MyDep").To<Dependency>())
            // Configure auto-wiring and inject dependency by tag "MyDep"
            using (container.Bind<IService>().To<Service>(
                ctx => new Service(ctx.Container.Inject<IDependency>("MyDep"))))
            {
                // Resolve an instance
                var instance = container.Get<IService>();
            }
            // }
        }
    }
}
