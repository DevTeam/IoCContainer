namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;
    using static Lifetime;

    public class DisposingLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=2 Lifetimes
            // $priority=01
            // $description=Disposing lifetime
            // {
            var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Use the Disposing lifetime
                .Bind<IService>().As(Disposing).To<Service>()
                .Container;

            var instance = container.Resolve<IService>();
            
            // Dispose instances on disposing a container
            container.Dispose();
            ((Service)instance).DisposeCount.ShouldBe(1);
            // }
        }
    }
}
