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
            // $header=It is easy to validate any binding without actual creation of instances.
            // {
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IService>().To<Service>()
                .Container;

            // Try getting a resolver of the interface `IService`
            // Also there is other method overload allowing to get the detailed error about issue why this instance cannot be resolved
            var canBeResolved = container.TryGetResolver<IService>(out _);

            // 'Service' has the mandatory dependency 'IDependency' in the constructor,
            // which was not registered and that is why it cannot be resolved
            canBeResolved.ShouldBeFalse();

            // Add the required binding to fix the the issue above
            container.Bind<IDependency>().To<Dependency>();

            canBeResolved = container.TryGetResolver<IService>(out _);

            // Everything is ok now
            canBeResolved.ShouldBeTrue();

            // }
        }
    }
}
