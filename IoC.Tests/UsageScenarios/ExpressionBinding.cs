namespace IoC.Tests.UsageScenarios
{
    using Shouldly;
    using Xunit;

    public class ExpressionBinding
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=03
            // $description=Expression binding
            // $header=A specific type is bound as a part of an expression tree. This dependency will be introduced as is, without any additional overhead like _lambda call_ or _type cast_.
            // {
            using var container = Container
                .Create()
                .Bind<IService>().To(ctx => new Service(new Dependency()))
                .Container;

            // Resolve an instance
            var instance = container.Resolve<IService>();
            // }
            // Check the instance
            instance.ShouldBeOfType<Service>();
        }
    }
}
