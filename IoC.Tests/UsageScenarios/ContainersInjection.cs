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
        // $tag=injection
        // $priority=04
        // $description=Containers Injection
        // $header=:warning: Please avoid injecting containers in non-infrastructure code. Keep your general code in ignorance of a container.
        // {
        public void Run()
        {
            // Create the parent container
            using var currentContainer = Container
                .Create("root")
                .Bind<MyClass>().To<MyClass>()
                .Container;

            var instance = currentContainer.Resolve<MyClass>();
            instance.CurrentContainer.ShouldBe(currentContainer);
            instance.ChildContainer1.Parent.ShouldBe(currentContainer);
            instance.ChildContainer2.Parent.ShouldBe(currentContainer);
            instance.NamedChildContainer.Parent.ShouldBe(currentContainer);
            instance.NamedChildContainer.ToString().ShouldBe("//root/Some name");
        }

        public class MyClass
        {
            public MyClass(
                IContainer currentContainer,
                IMutableContainer newChildContainer,
                Func<IMutableContainer> childContainerFactory,
                Func<string, IMutableContainer> nameChildContainerFactory)
            {
                CurrentContainer = currentContainer;
                ChildContainer1 = newChildContainer;
                ChildContainer2 = childContainerFactory();
                NamedChildContainer = nameChildContainerFactory("Some name");
            }

            public IContainer CurrentContainer { get; }

            public IContainer ChildContainer1 { get; }

            public IContainer ChildContainer2 { get; }

            public IContainer NamedChildContainer { get; }
        }
        // }
    }
}
