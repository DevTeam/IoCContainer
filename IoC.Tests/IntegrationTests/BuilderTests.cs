namespace IoC.Tests.IntegrationTests
{
    using System.Linq.Expressions;
    using Shouldly;
    using Xunit;

    public class BuilderTests
    {
        [Fact]
        public void ContainerShouldUseBuilder()
        {
            // Given
            using var container = Container.Create();

            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            using (container.Bind<IBuilder>().To<MyBuilder>())
            {
                var instance1 = container.Resolve<IMyGenericService<string, int>>();
                var instance2 = container.Resolve<IMyGenericService<string, int>>();

                // Then
                instance1.ShouldBe(instance2);
            }
        }

        [Fact]
        public void ContainerShouldUseBuilderInChildContainer()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
            {
                using var childContainer = container.CreateChild();
                using (container.Bind<IBuilder>().To<MyBuilder>())
                {
                    var instance1 = childContainer.Resolve<IMyGenericService<string, int>>();
                    var instance2 = childContainer.Resolve<IMyGenericService<string, int>>();

                    // Then
                    instance1.ShouldBe(instance2);
                }
            }
        }

        [Fact]
        public void ContainerShouldUseBuilderAfterChildCreated()
        {
            // Given
            using var container = Container.Create();
            // When
            using (container.Bind<IBuilder>().To<MyBuilder>())
            {
                using var childContainer = container.CreateChild();
                using (childContainer.Bind<IMyGenericService<TT1, TT2>>().To<MyGenericService<TT1, TT2>>())
                {
                    var instance1 = childContainer.Resolve<IMyGenericService<string, int>>();
                    var instance2 = childContainer.Resolve<IMyGenericService<string, int>>();

                    // Then
                    instance1.ShouldBe(instance2);
                }
            }
        }

        public class MyBuilder : IBuilder
        {
            public Expression Build(Expression expression, IBuildContext buildContext)
            {
                return buildContext.AddLifetime(expression, new Lifetimes.SingletonLifetime());
            }
        }
    }
}
