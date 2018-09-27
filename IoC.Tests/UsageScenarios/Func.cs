﻿namespace IoC.Tests.UsageScenarios
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
            // $priority=02
            // $description=Func
            // {
            Func<IService> func = () => new Service(new Dependency());

            // Create and configure the container
            using (var container = Container.Create())
            // Bind to result of function invocation
            using (container.Bind<IService>().To(ctx => func()))
            {
                // Resolve an instance
                var instance = container.Resolve<IService>();

                // Check the instance's type
                instance.ShouldBeOfType<Service>();
            }
            // }
        }
    }
}
