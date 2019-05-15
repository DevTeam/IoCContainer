namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "UnusedVariable")]
    public class DependencyTag
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=02
            // $description=Dependency Tag
            // $header=Use a _tag_ to inject specific dependency from several bindings of the same types.
            // {
            // Create and configure the container
            using var container = Container.Create();
            using (container.Bind<IDependency>().Tag("MyDep").To<Dependency>())
                // Configure autowiring and inject dependency tagged by "MyDep"
            using (container.Bind<IService>().To<Service>(
                ctx => new Service(ctx.Container.Inject<IDependency>("MyDep"))))
            {
                // Resolve an instance
                var instance = container.Resolve<IService>();
                // }
                // Check the instance's type
                instance.ShouldBeOfType<Service>();
                // {
            }

            // }
        }
    }
}
