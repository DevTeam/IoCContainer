// ReSharper disable AccessToDisposedClosure
namespace IoC.Tests.UsageScenarios
{
    using System.Threading;
    using Lifetimes;
    using Shouldly;
    using Xunit;

    public class ThreadSingletonLifetime
    {
        [Fact]
        // $visible=true
        // $tag=2 Lifetimes
        // $priority=10
        // $description=Thread Singleton lifetime
        // $header=Sometimes it is useful to have a [singleton](https://en.wikipedia.org/wiki/Singleton_pattern) instance per a thread (or more generally a singleton per something else). There is no special "lifetime" type in this framework to achieve this requirement, but it is quite easy create your own "lifetime" type for that using base type [_KeyBasedLifetime<>_](IoC/Lifetimes/KeyBasedLifetime.cs).
        // {
        public void Run()
        {
            var finish = new ManualResetEvent(false);
            
            var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Bind an interface to an implementation using the singleton per a thread lifetime
                .Bind<IService>().Lifetime(new ThreadLifetime()).To<Service>()
                .Container;

            // Resolve the singleton twice
            var instance1 = container.Resolve<IService>();
            var instance2 = container.Resolve<IService>();
            IService instance3 = null;
            IService instance4 = null;

            var newThread = new Thread(() =>
            {
                instance3 = container.Resolve<IService>();
                instance4 = container.Resolve<IService>();
                finish.Set();
            });

            newThread.Start();
            finish.WaitOne();

            // Check that instances resolved in a main thread are equal
            instance1.ShouldBe(instance2);
            // Check that instance resolved in a new thread is not null
            instance3.ShouldNotBeNull();
            // Check that instances resolved in different threads are not equal
            instance1.ShouldNotBe(instance3);
            // Check that instances resolved in a new thread are equal
            instance4.ShouldBe(instance3);
        }

        // Represents the custom thead singleton lifetime based on the KeyBasedLifetime
        public sealed class ThreadLifetime : KeyBasedLifetime<int>
        {
            // Creates a clone of the current lifetime (for the case with generic types)
            public override ILifetime CreateLifetime() =>
                new ThreadLifetime();

            // Provides a key of an instance
            // If a key the same an instance is the same too
            protected override int CreateKey(IContainer container, object[] args) =>
                Thread.CurrentThread.ManagedThreadId;
        }
        // }
    }
}
