namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class GenericAutoWiring
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=00
            // $description=Generic Auto-wiring
            // $header=Auto-writing of generic types as simple as auto-writing of other types. Just use a generic parameters markers like _TT_, _TT1_ and etc. or bind open generic types.
            // {
            // Create and configure the container using auto-wiring
            using (var container = Container.Create())
            // Bind some dependency
            using (container.Bind<IDependency>().To<Dependency>())
            // Bind to the instance creation, actually represented as an expression tree
            using (container.Bind<IService<TT>>().To<Service<TT>>())
            // or the same: using (container.Bind(typeof(IService<>)).To(typeof(Service<>)))
            {
                // Resolve a generic instance
                var instance = container.Resolve<IService<int>>();
                // }
                // Check the instance's type
                instance.ShouldBeOfType<Service<int>>();
                // {
            }
            // }
        }
    }
}
