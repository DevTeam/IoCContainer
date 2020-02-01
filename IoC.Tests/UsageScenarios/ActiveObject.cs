#if !NET40
// ReSharper disable PossibleNullReferenceException
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantTypeArgumentsOfMethod
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    using static Lifetime;

    public class ActiveObject
    {
        [Fact]
        // $visible=true
        // $tag=injection
        // $priority=05
        // $description=Active Object
        // $header=In [object-oriented programming](https://en.wikipedia.org/wiki/Object-oriented_programming) an object is active when its state depends on clock. Usually an active object encapsulates a task that updates the object's state.
        // {
        public void Run()
        {
            // Create the parent container
            var container = Container
                .Create()
                // This singleton instance "Activation" of type "CancellationTokenSource" was registered as singleton and it implements "IDisposable"
                // So it will be automatically disposed during container disposing
                // And every related "Activation" tasks will be finished automatically
                .Bind<CancellationTokenSource>().Tag("Activation").As(Singleton).To<CancellationTokenSource>()
                // Bind some type of Active Object
                .Bind<IAnotherService>().Bind<IActiveObject>().To<MyActiveObject>(ctx
                    // While resolving an Active Object the method "Activate" will be invoked for each instance
                    // And a singleton instance "Activation" of type "CancellationTokenSource" is passed as an argument to deactivate every Active Objects
                    => ctx.It.Activate(ctx.Container.Inject<CancellationTokenSource>("Activation")))
                .Container;

            using (container)
            {
                // Resolves an Active Object
                container.Resolve<IAnotherService>();
            }

            // Every Active Objects will be deactivated here
        }

        public interface IActiveObject
        {
             void Activate(CancellationTokenSource cancellationTokenSource);
        }

        public class MyActiveObject: IAnotherService, IActiveObject
        {
            public void Activate(CancellationTokenSource cancellationTokenSource)
            {
                // Do some actions
                Task.Delay(TimeSpan.FromDays(1), cancellationTokenSource.Token);
            }
        }
        // }
    }
}
#endif
