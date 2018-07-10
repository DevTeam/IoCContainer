namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Generics
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=02
            // $description=Generics
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            // Use full auto-wiring
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind(typeof(IService<>)).To(typeof(Service<>)))
            {
                // Resolve a generic instance
                var instance = container.Resolve<IService<int>>();

                instance.ShouldBeOfType<Service<int>>();
            }
            // }
        }
    }
}
