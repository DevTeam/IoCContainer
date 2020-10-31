namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ValidateBinding
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=5 Advanced
            // $priority=08
            // $description=Check a binding
            // $header=It is easy to validate that binding is already exists.
            // {
            using var container = Container.Create();

            var isBound = container.IsBound<IService>();
            // _IService_ is not bound yet
            isBound.ShouldBeFalse();

            container.Bind<IService>().To<Service>();
            // _IService_ is already bound
            isBound = container.IsBound<IService>();

            isBound.ShouldBeTrue();
            // }
        }
    }
}
