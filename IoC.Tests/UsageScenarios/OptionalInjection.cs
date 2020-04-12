// ReSharper disable IdentifierTypo
// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable EmptyConstructor
// ReSharper disable UnusedVariable
#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class OptionalInjection
    {
        [Fact]
        // $visible=true
        // $tag=binding
        // $priority=06
        // $description=Optional Injection
        // {
        public void Run()
        {
            // Create the container and configure it
            using var container = Container.Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<SomeService>(ctx => new SomeService(ctx.Container.Inject<IDependency>(), ctx.Container.TryInject<string>()))
                .Container;

            // Resolve an instance
            var instance = container.Resolve<IService>();

            // Check the optional dependency
            instance.State.ShouldBe("empty");
        }

        public class SomeService: IService
        {
            // "state" dependency is not resolved here but will be null value because it was injected optional
            public SomeService(IDependency dependency, string state)
            {
                Dependency = dependency;
                State = state ?? "empty";
            }

            public IDependency Dependency { get; }

            public string State { get; }
        }
        // }
    }
}
#endif