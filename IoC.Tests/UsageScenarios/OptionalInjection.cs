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
        // $tag=1 Basics
        // $priority=06
        // $description=Optional injection
        // {
        public void Run()
        {
            using var container = Container.Create()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<SomeService>(ctx => 
                    new SomeService(
                        ctx.Container.Inject<IDependency>(),
                        // Injects default(string) if the dependency cannot be resolved
                        ctx.Container.TryInject<string>(),
                        // Injects default(int) if the dependency cannot be resolved
                        ctx.Container.TryInject<int>(),
                        // Injects int? if the dependency cannot be resolved
                        ctx.Container.TryInjectValue<int>()))
                .Container;

            // Resolve an instance
            var instance = container.Resolve<IService>();

            // Check optional dependencies
            instance.State.ShouldBe("empty,True,False");
        }

        public class SomeService: IService
        {
            public SomeService(IDependency dependency, string state, int? val1, int? val2)
            {
                Dependency = dependency;
                State = state ?? $"empty,{val1.HasValue},{val2.HasValue}";
            }

            public IDependency Dependency { get; }

            public string State { get; }
        }
        // }
    }
}
#endif