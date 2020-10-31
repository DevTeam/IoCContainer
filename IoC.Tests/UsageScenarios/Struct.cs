// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedVariable
// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable UnusedMember.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Shouldly;
    using Xunit;
    using static Lifetime;

    public class Struct
    {
        [Fact]
        // $visible=true
        // $tag=1 Basics
        // $priority=05
        // $description=Struct
        // $header=Value types are fully supported avoiding any boxing/unboxing or cast operations, so the performance does not suffer!
        // {
        public void Run()
        {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Register the tracing builder
                .Bind<TracingBuilder, IBuilder>().As(Singleton).To<TracingBuilder>()
                // Register a struct
                .Bind<MyStruct>().To<MyStruct>()
                .Container;

            // Resolve an instance
            var instance = container.Resolve<MyStruct>();

            // Check the expression which was used to create an instances of MyStruct
            var expressions = container.Resolve<TracingBuilder>().Expressions;
            var structExpression = expressions[new Key(typeof(MyStruct))].ToString();
            // The actual code is "new MyStruct(new Dependency())"!
            structExpression.ShouldBe("new MyStruct(new Dependency())");
            // Obvious there are no any superfluous operations like a `boxing`, `unboxing` or `cast`,
            // just only what is really necessary to create an instance
        }

        public struct MyStruct
        {
            public MyStruct(IDependency dependency) { }
        }

        // This builder saves expressions that used to create resolvers into a map
        public class TracingBuilder : IBuilder
        {
            public readonly IDictionary<Key, Expression> Expressions = new Dictionary<Key, Expression>();

            public Expression Build(IBuildContext context, Expression expression)
            {
                Expressions[context.Key] = expression;
                return expression;
            }
        }
        // }
    }
}
