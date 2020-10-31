namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class FuncDependency
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=1 Basics
            // $priority=05
            // $description=Func dependency
            // $header=No comments. Everything is very simple!
            // {
            Func<IService> func = () => new Service(new Dependency());

            using var container = Container
                .Create()
                .Bind<IService>().To(ctx => func())
                .Container;

            var instance = container.Resolve<IService>();
            // }
            instance.ShouldBeOfType<Service>();

        }
    }
}
