namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class AutoWiringWithInitialization
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=00
            // $description=Auto-wiring with initialization
            // $header=Auto-writing allows to perform some initializations.
            // {
            // Create the container and configure it, using full auto-wiring
            using (var container = Container.Create())
            // Bind some dependency
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<INamedService>().To<InitializingNamedService>(ctx => ctx.It.Initialize("text", ctx.Container.Resolve<IDependency>())))
            {
                // Resolve an instance of interface `IService`
                var instance = container.Resolve<INamedService>();
                // }
                // Check the instance's type
                instance.ShouldBeOfType<InitializingNamedService>();
                // Check the initialization
                instance.Name.ShouldBe("text");
                // {
            }
            // }
        }
    }
}
