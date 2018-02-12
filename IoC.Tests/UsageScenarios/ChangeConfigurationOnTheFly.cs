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
            // $group=02
            // $priority=01
            // $description=Change configuration on-the-fly
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            {
                // Configure the container using the Transient lifetime
                using (container.Bind<IService>().To<Service>())
                {
                    // Resolve instances
                    var instance1 = container.Get<IService>();
                    var instance2 = container.Get<IService>();

                    instance1.ShouldNotBe(instance2);
                }

                // Reconfigure the container using the Singleton lifetime
                using (container.Bind<IService>().Lifetime(Lifetime.Singleton).To<Service>())
                {
                    // Resolve the instance twice
                    var instance1 = container.Get<IService>();
                    var instance2 = container.Get<IService>();

                    instance1.ShouldBe(instance2);
                }
            }
            // }
        }
    }
}
