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
            // $header=Sometimes instances required some actions before you give them to use - some methods of initialization or fields which should be defined. You can solve these things easy.
            // {
            // Create the container and configure it using full autowiring
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<INamedService>().To<InitializingNamedService>(
                    // Configure the container to invoke method "Initialize" for every created instance of this type
                    ctx => ctx.It.Initialize("initialized !!!", ctx.Container.Resolve<IDependency>()))
                .Container;

            // Resolve an instance of interface `IService`
            var instance = container.Resolve<INamedService>();
            
            // Check the instance's type
            instance.ShouldBeOfType<InitializingNamedService>();

            // Check the initialization is ok
            instance.Name.ShouldBe("initialized !!!");
            // }
        }
    }
}
