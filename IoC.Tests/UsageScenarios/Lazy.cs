﻿namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Lazy
    {
        [Fact]
        public void Run()
        {
            // $visible=true
            // $tag=basic
            // $priority=05
            // $description=Lazy
            // $header=_Lazy_ dependency helps when a logic needs to inject Lazy<T> to get instance once on demand.
            // {
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
            // Check the instance
            instance.ShouldBeOfType<Service>();
        }
    }
}