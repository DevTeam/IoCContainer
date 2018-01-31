namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Linq.Expressions;
    using Lifetimes;
    using Shouldly;
    using Xunit;

    public class CustomDependency
    {
        [Fact]
        // $visible=true
        // $group=08
        // $priority=00
        // $description=Custom Dependency
        // {
        public void Run()
        {
            // Create the container
            using (var container = Container.Create())
            // Configure the container
            using (container.Bind<string>().To(new ConstDependency<string>("abc")))
            {
                // Resolve the instance
                var contsVal = container.Get<string>();

                contsVal.ShouldBe("abc");
            }
        }

        // Custom dependency should implement 2 interfaces.
        public class ConstDependency<T> : IoC.IDependency, IEmitable
        {
            private readonly T _constValue;

            public ConstDependency(T constValue)
            {
                _constValue = constValue;
            }

            public Type Type => typeof(T);

            public Expression Emit(Expression baseExpression)
            {
                return Expression.Constant(_constValue);
            }
        }
        // }
    }
}
