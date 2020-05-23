namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ValidateResolving
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=advanced
            // $priority=08
            // $description=Check for possible resolving
            // $header=It is easy to validate without the actual operation.
            // {
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IService>().To<Service>()
                .Container;

            var canResolve = container.CanResolve<IService>();

            // _Service_ has the mandatory dependency _IDependency_ in the constructor,
            // which was not registered and that is why _IService_ cannot be resolved
            canResolve.ShouldBeFalse();

            // Add the required binding for _Service_
            container.Bind<IDependency>().To<Dependency>();

            // Now it is possible to resolve _IService_
            canResolve = container.CanResolve<IService>();

            // Everything is ok now
            canResolve.ShouldBeTrue();
            // }
        }
    }
}
