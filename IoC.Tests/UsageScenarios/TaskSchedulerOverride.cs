﻿#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class TaskSchedulerOverride
    {
        [Fact]
        public async void Run()
        {
            // $visible=true
            // $tag=4 Async
            // $priority=05
            // $description=Override the default task scheduler
            // $header=_TaskScheduler.Current_ is used by default for an asynchronous construction, but it is easy to override it, binding abstract class _TaskScheduler_ to required implementation in an IoC container.
            // {
            using var container = Container.Create()
                // Bind some dependency
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<Service>()
                // Override the default _TaskScheduler by your own one
                .Bind<TaskScheduler>().To(ctx => TaskScheduler.Default)
                .Container;

            // Resolve an instance asynchronously
            var instance = await container.Resolve<Task<IService>>();
            // }
            // Check the instance
            instance.ShouldBeOfType<Service>();
        }
    }
}
#endif