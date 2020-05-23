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
    public class DefaultParamsInjection
    {
        [Fact]
        // $visible=true
        // $tag=basic
        // $priority=06
        // $description=Injection of default parameters
        // {
        public void Run()
        {
            using var container = Container.Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<SomeService>()
                .Container;

            // Resolve an instance
            var instance = container.Resolve<IService>();

            // Check the optional dependency
            instance.State.ShouldBe("empty");
        }

        public class SomeService: IService
        {
            // "state" dependency is not resolved here but it has the default value "empty"
            public SomeService(IDependency dependency, string state = "empty")
            {
                Dependency = dependency;
                State = state;
            }

            public IDependency Dependency { get; }

            public string State { get; }
        }
        // }
    }
}
#endif