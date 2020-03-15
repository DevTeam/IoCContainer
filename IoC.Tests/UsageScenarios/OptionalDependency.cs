// ReSharper disable IdentifierTypo
// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable EmptyConstructor
// ReSharper disable UnusedVariable
#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class OptionalDependency
    {
        [Fact]
        // $visible=true
        // $tag=binding
        // $priority=06
        // $description=Optional Dependency
        // {
        public void Run()
        {
            // Create the container and configure it
            using var container = Container.Create()
                .Using<OptionalFeature>()
                .Bind<IDependency>().To<Dependency>()
                .Bind<IService>().To<SomeService>()
                .Container;

            // Resolve an instance
            var instance = container.Resolve<IService>();

            // Check the optional dependency
            instance.State.ShouldBe("empty");
        }

        public class OptionalFeature: IConfiguration
        {
            public IEnumerable<IToken> Apply(IContainer container)
            {
                yield return container
                    // Bind factory for Optional for any tags
                    .Bind<Optional<TT>>().Tag(Key.AnyTag).To(ctx => Create<TT>(ctx));
            }

            private static Optional<T> Create<T>(Context ctx) => 
                ctx.Container.TryGetResolver<T>(typeof(T), ctx.Key.Tag, out var resolver, out _)
                    ? new Optional<T>(resolver(ctx.Container, ctx.Args))
                    : Optional<T>.Empty;
        }

        public struct Optional<T>
        {
            public static readonly Optional<T> Empty = new Optional<T>();
            public readonly T Value;
            public readonly bool HasValue;

            public Optional(T value) : this()
            {
                Value = value;
                HasValue = true;
            }
        }

        public class SomeService: IService
        {
            public SomeService(IDependency dependency, Optional<string> state)
            {
                Dependency = dependency;
                State = state.HasValue ? state.Value : "empty";
            }

            public IDependency Dependency { get; }

            public string State { get; }
        }
        // }
    }
}
#endif