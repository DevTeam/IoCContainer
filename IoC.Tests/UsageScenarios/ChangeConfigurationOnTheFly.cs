namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ChangeConfigurationOnTheFly
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=01
            // $description=Change configuration on-the-fly
            // {
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Container;
            
            // Configure `IService` as Transient
            using (container.Bind<IService>().To<Service>())
            {
                container.IsBound<IService>().ShouldBeTrue();

                // Resolve instances
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                // Check that instances are not equal
                instance1.ShouldNotBe(instance2);
            }

            container.IsBound<IService>().ShouldBeFalse();

            // Reconfigure `IService` as Singleton
            using (container.Bind<IService>().As(Lifetime.Singleton).To<Service>())
            {
                // Resolve the singleton twice
                var instance1 = container.Resolve<IService>();
                var instance2 = container.Resolve<IService>();

                // Check that instances are equal
                instance1.ShouldBe(instance2);
            }

            // }
        }
    }
}
