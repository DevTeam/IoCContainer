namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class GenericAutowiring
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=00
            // $description=Generic Auto-wiring
            // {
            // Create and configure the container using auto-wiring
            using (var container = Container.Create())
            using (container.Bind<IDependency>().To<Dependency>())
            // Bind to the instance creation, actually represented as an expression tree
            using (container.Bind<IService<TT>>().To<Service<TT>>(
                // Select the constructor and inject the dependency
                ctx => new Service<TT>(ctx.Container.Inject<IDependency>())))
            {
                // Resolve a generic instance
                var instance = container.Resolve<IService<int>>();

                // Check the instance's type
                instance.ShouldBeOfType<Service<int>>();
            }
            // }
        }
    }
}
