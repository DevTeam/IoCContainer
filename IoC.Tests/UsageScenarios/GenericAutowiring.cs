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
            // $priority=02
            // $description=Generic Auto-wiring
            // {
            // Create a container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            // Configure auto-wiring
            using (container.Bind<IService<TT>>().To<Service<TT>>(
                // Select the constructor
                ctx => new Service<TT>(ctx.Container.Inject<IDependency>())))
            {
                // Resolve a generic instance
                var instance = container.Resolve<IService<int>>();

                instance.ShouldBeOfType<Service<int>>();
            }
            // }
        }
    }
}
