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
            // $group=01
            // $priority=02
            // $description=Generic Auto-wiring
            // {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<IDependency>().To<Dependency>())
            // Configure auto-wiring
            using (container.Bind<IService<TT>>().To<Service<TT>>(
                // Configure the constructor to use
                ctx => new Service<TT>(ctx.Container.Inject<IDependency>())))
            {
                // Resolve the generic instance
                var instance = container.Get<IService<int>>();

                instance.ShouldBeOfType<Service<int>>();
            }
            // }
        }
    }
}
