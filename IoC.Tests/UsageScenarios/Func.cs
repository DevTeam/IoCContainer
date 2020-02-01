namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class Func
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=binding
            // $priority=05
            // $description=Func
            // $header=No comments. Everything is very simple!
            // {
            Func<IService> func = () => new Service(new Dependency());

            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IService>().To(ctx => func())
                .Container;

            // Resolve an instance
            var instance = container.Resolve<IService>();
            // }
            // Check the instance's type
            instance.ShouldBeOfType<Service>();

        }
    }
}
