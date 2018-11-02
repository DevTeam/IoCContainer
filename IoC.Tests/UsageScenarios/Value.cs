namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Value
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=01
            // $description=Value
            // $header=In this case the specific type is binded to the manually created instance based on an expression tree. This dependency will be introduced as is, without any additional overhead like _lambda call_ or _type cast_.
            // {
            // Create and configure the container
            using (var container = Container.Create())
            using (container.Bind<IService>().To(ctx => new Service(new Dependency())))
            {
                // Resolve an instance
                var instance = container.Resolve<IService>();
                // }
                // Check the instance's type
                instance.ShouldBeOfType<Service>();
                // {
            }
            // }
        }
    }
}
