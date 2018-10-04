// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedParameter.Local
// ReSharper disable UnusedVariable
// ReSharper disable RedundantTypeArgumentsOfMethod
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
        // $tag=binding
        // $priority=04
        // $description=Struct
        // {
        public void Run()
        {
            // Create and configure the container
            using (var container = Container.Create())
            using (container.Bind<IDependency>().To<Dependency>())
            // Register the tracing builder
            using (container.Bind<TracingBuilder, IBuilder>().As(Singleton).To<TracingBuilder>())
            // Register a struct
            using (container.Bind<MyStruct>().To<MyStruct>())
            {
                // Resolve an instance
                var instance = container.Resolve<MyStruct>();

                // Check the expression which was used to create an instances of MyStruct
                var expressions = container.Resolve<TracingBuilder>().Expressions;
                var structExpression = expressions[new Key(typeof(MyStruct))].ToString();
                structExpression.ShouldBe("new MyStruct(new Dependency())");
                // Obvious there are no any superfluous operations like a `boxing`, `unboxing` or `cast`,
                // just only what is really necessary to create an instance
            }
        }

        public struct MyStruct
        {
            public MyStruct(IDependency dependency) { }
        }

        // This builder saves expressions that used to create resolvers into a map
        public class TracingBuilder : IBuilder
        {
            public readonly IDictionary<Key, Expression> Expressions = new Dictionary<Key, Expression>();

            public Expression Build(Expression expression, IBuildContext buildContext)
            {
                Expressions[buildContext.Key] = expression;
                return expression;
            }
        }
        // }
    }
}
