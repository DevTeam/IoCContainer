// ReSharper disable PossibleNullReferenceException
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantTypeArgumentsOfMethod
namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class ContainersInjection
    {
        [Fact]
        // $visible=true
        // $tag=binding
        // $priority=04
        // $description=Containers Injection
        // {
        public void Run()
        {
            // Create the parent container
            using var currentContainer = Container
                .Create("root")
                .Bind<MyClass>()
                .To<MyClass>()
                .Container;

            var instance = currentContainer.Resolve<MyClass>();
            instance.CurrentContainer.ShouldBe(currentContainer);
            instance.ChildContainer.Parent.ShouldBe(currentContainer);
            instance.NamedChildContainer.Parent.ShouldBe(currentContainer);
            instance.NamedChildContainer.ToString().ShouldBe("//root/Some name");
        }

        public class MyClass
        {
            public MyClass(
                IContainer currentContainer,
                Func<IContainer> childContainerFactory,
                Func<string, IContainer> nameChildContainerFactory)
            {
                CurrentContainer = currentContainer;
                ChildContainer = childContainerFactory();
                NamedChildContainer = nameChildContainerFactory("Some name");
            }

            public IContainer CurrentContainer { get; }

            public IContainer ChildContainer { get; }

            public IContainer NamedChildContainer { get; }
        }
        // }
    }
}
