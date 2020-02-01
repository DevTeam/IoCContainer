﻿namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class ResolveLazy
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=injection
            // $priority=02
            // $description=Resolve Lazy
            // $header=_Lazy_ dependency helps when a logic needs to inject some _lazy proxy_ to get instance once on demand.
            // {
            // Create and configure the container
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                .Container;

            // Resolve the instance of Lazy<IService>
            var lazy = container.Resolve<Lazy<IService>>();

            // Get the instance via Lazy
            var instance = lazy.Value;
            // }
            // Check the instance's type
            instance.ShouldBeOfType<Service>();
        }
    }
}
