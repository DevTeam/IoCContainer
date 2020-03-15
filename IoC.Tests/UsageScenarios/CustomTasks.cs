// ReSharper disable IdentifierTypo
// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable EmptyConstructor
// ReSharper disable UnusedVariable
#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;
    using static Lifetime;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class CustomTask
    {
        [Fact]
        // $visible=true
        // $tag=customization
        // $priority=00
        // $description=Custom Tasks
        // {
        public async void Run()
        {
            // Create the container and configure it
            using var container = Container.Create()
                .Using<CustomTasksFeature>()
                // Bind some dependency
                .Bind<IDependency>().To<SomeDependency>()
                .Bind<Consumer>().To<Consumer>()
                .Container;

            // Resolve an instance asynchronously
            var instance = await container.Resolve<Task<Consumer>>();

            // Check the instance's type
            instance.ShouldBeOfType<Consumer>();
        }

        internal class CustomTasksFeature: IConfiguration
        {
            public IEnumerable<IToken> Apply(IContainer container)
            {
                yield return container
                    // Bind cancellation token source
                    .Bind<CancellationTokenSource>().To<CancellationTokenSource>()
                    // Bind the class responsible for tasks creation
                    .Bind<TaskFactory<TT>>().As(Singleton).To<TaskFactory<TT>>()
                    // Bind the task factory for any tags
                    .Bind<Task<TT>>().Tag(Key.AnyTag).To(ctx => ctx.Container.Inject<TaskFactory<TT>>(ctx.Key.Tag).Create());
            }

            internal class TaskFactory<T>
            {
                private readonly Func<T> _factory;
                private readonly CancellationTokenSource _cancellationTokenSource;

                public TaskFactory(Func<T> factory, CancellationTokenSource cancellationTokenSource)
                {
                    _factory = factory;
                    _cancellationTokenSource = cancellationTokenSource;
                }

                public Task<T> Create()
                {
                    var task = new Task<T>(_factory, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
                    task.Start(TaskScheduler.Default);
                    return task;
                }
            }
        }

        public class SomeDependency: IDependency
        {
            // Time-consuming logic constructor
            public SomeDependency() { }
        }

        public class Consumer
        {
            public Consumer(Task<IDependency> dependency1, Task<IDependency> dependency2)
            {
                // Time-consuming logic
                var dep1 = dependency1.Result;
                var dep2 = dependency2.Result;
            }
        }
        // }
    }
}
#endif