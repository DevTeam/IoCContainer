namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class Validation
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=08
            // $description=Validation
            // {
            // Create and configure the container
            using (var container = Container.Create())
            using (container.Bind<IService>().To<Service>())
            {
                // Try getting a resolver of the interface `IService`
                var canBeResolved = container.TryGetResolver<IService>(out _);

                // 'Service' has the mandatory dependency 'IDependency' in the constructor,
                // which was not registered and cannot be resolved
                canBeResolved.ShouldBeFalse();
            }
            // }
        }
    }
}
