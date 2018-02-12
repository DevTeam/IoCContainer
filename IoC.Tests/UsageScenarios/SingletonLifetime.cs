namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class SingletonLifetime
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=03
            // $description=Singleton lifetime
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().Lifetime(Lifetime.Singleton).To<Service>())
            {
                // Resolve the instance twice
                var instance1 = container.Get<IService>();
                var instance2 = container.Get<IService>();

                instance1.ShouldBe(instance2);
            }

            // Other lifetimes are:
            // Transient - A new instance each time
            // Container - Singleton per container
            // Scope - Singleton per scope
            // }
        }
    }
}
