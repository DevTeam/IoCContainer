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
            // $description=Autowiring with initialization
            // $header=Auto-writing allows to perform some initializations.
            // {
            // Create the container and configure it, using full autowiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<INamedService>().To<InitializingNamedService>(ctx => ctx.It.Initialize("text", ctx.Container.Resolve<IDependency>()))
                .Container;
            // Resolve an instance of interface `IService`
            var instance = container.Resolve<INamedService>();
            // }
            // Check the instance's type
            instance.ShouldBeOfType<InitializingNamedService>();
            // Check the initialization
            instance.Name.ShouldBe("text");
        }
    }
}
