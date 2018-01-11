namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ResolveGeneric
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $group=01
            // $priority=01
            // $description=Generics
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind(typeof(IService<>)).To(typeof(Service<>)))
            {
                // Resolve the generic instance
                var instance = container.Get<IService<int>>();

                instance.ShouldBeOfType<Service<int>>();
            }
            // }
        }
    }
}
