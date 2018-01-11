namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ResolveSingletone
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=03
            // $description=Singletone
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().Lifetime(Lifetime.Singletone).To<Service>())
            {
                // Resolve the instance twice
                var instance1 = container.Get<IService>();
                var instance2 = container.Get<IService>();

                instance1.ShouldBe(instance2);
            }

            // Other lifetimes are:
            // Transient - A new instance each time
            // Container - Singletone per container
            // Resolve - Singletone per resolve
            // }
        }
    }
}
