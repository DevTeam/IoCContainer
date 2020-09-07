namespace IoC.Tests.IntegrationTests
{
    using System.Linq.Expressions;
    using Lifetimes;
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
                using var childContainer = container.Create();
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
                using var childContainer = container.Create();
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
            public Expression Build(IBuildContext context, Expression expression)
            {
                var singletonLifetime = new SingletonLifetime();
                return singletonLifetime.Build(context, expression);
            }
        }
    }
}
