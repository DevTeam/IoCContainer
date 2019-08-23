// ReSharper disable ClassNeverInstantiated.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Xunit;
    using Expression = System.Linq.Expressions.Expression;

    public class CustomBuilder
    {
        [Fact]
        // $visible=true
        // $tag=customization
        // $priority=00
        // $description=Custom Builder
        // {
        public void Run()
        {
            // Create and configure the container
            using var container = Container.Create();
            using (container.Bind<IDependency>().To<Dependency>())
            using (container.Bind<IService>().To<Service>(ctx => new Service(ctx.Container.Resolve<IDependency>(), ctx.Args[0] as string)))
            // Register the custom builder
            using (container.Bind<IBuilder>().To<NotNullGuardBuilder>())
            {
                // Resolve an instance passing null to the "state" parameter
                Assert.Throws<ArgumentNullException>(() => container.Resolve<IService>(null as string));
            }
        }

        // This custom builder adds the logic to check parameters of reference types injected via constructors on null
        private class NotNullGuardBuilder : IBuilder
        {
            public Expression Build(Expression expression, IBuildContext buildContext) =>
                expression is NewExpression newExpression && newExpression.Arguments.Count != 0
                    ? newExpression.Update(CheckedArgs(newExpression))
                    : expression;

            private static IEnumerable<Expression> CheckedArgs(NewExpression newExpression) =>
                from arg in newExpression.Constructor.GetParameters().Select((info, index) => (info, expression: newExpression.Arguments[index]))
                let typeDescriptor = arg.info.ParameterType.Descriptor()
                select !typeDescriptor.IsValueType()
                    // arg ?? throw new ArgumentNullException(nameof(arg), "The argument ...")
                    ? Expression.Coalesce(
                        arg.expression,
                        // throws an exception when an argument is null
                        Expression.Block(
                            Expression.Throw(Expression.Constant(new ArgumentNullException(arg.info.Name, $"The argument \"{arg.info.Name}\" is null while constructing the instance of type \"{newExpression.Type.Name}\"."))),
                            Expression.Default(typeDescriptor.Type)))
                    : arg.expression;
        }
        // }
    }
}
