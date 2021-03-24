// ReSharper disable PossibleNullReferenceException
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable RedundantTypeArgumentsOfMethod
namespace IoC.Tests.UsageScenarios
{
    using System;
    using Shouldly;
    using Xunit;

    public class ContainerInjection
    {
        [Fact]
        // $visible=true
        // $tag=5 Advanced
        // $priority=04
        // $description=Container injection
        // $header=:warning: Please avoid injecting containers in non-infrastructure code. Keep your code in ignorance about a container framework.
        // {
        public void Run()
        {
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
