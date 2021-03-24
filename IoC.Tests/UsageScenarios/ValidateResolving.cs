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
            // $tag=5 Advanced
            // $priority=08
            // $description=Check for possible resolving
            // $header=It is easy to validate the ability to resolve something without resolving it.
            // {
            // Create and configure a container
            using var container = Container
                .Create()
                .Bind<IService>().To<Service>()
                .Container;

            // _Service_ has the mandatory dependency _IDependency_ in the constructor,
            // which was not registered and that is why _IService_ cannot be resolved
            container.CanResolve<IService>().ShouldBeFalse();

            // Add the required binding for _Service_
            container.Bind<IDependency>().To<Dependency>();

            // Now it is possible to resolve _IService_
            container.CanResolve<IService>().ShouldBeTrue();
            // }
        }
    }
}
