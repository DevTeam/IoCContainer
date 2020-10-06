// ReSharper disable UnusedVariable
// ReSharper disable ArrangeTypeMemberModifiers
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Shouldly;
    using Xunit;
    using Expression = System.Linq.Expressions.Expression;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class OverrideAutowiringStrategy
    {
        [Fact]
        // $visible=true
        // $tag=advanced
        // $priority=10
        // $description=Custom autowiring strategy
        // {
        public void Run()
        {
            using var container = Container
                .Create()
                .Bind<IDependency>().To<Dependency>()
                // Additionally NamedService requires name in the constructor
                .Bind<INamedService>().To<NamedService>()
                // Overrides the previous autowiring strategy for the current and children containers
                .Bind<IAutowiringStrategy>().To<CustomAutowiringStrategy>()
                .Container;

            var service = container.Resolve<INamedService>();

            service.Name.ShouldBe("default name");
        }

        class CustomAutowiringStrategy : IAutowiringStrategy
        {
            private readonly IAutowiringStrategy _baseStrategy;

            public CustomAutowiringStrategy(IContainer container, IAutowiringStrategy baseStrategy) =>
                // Saves the previous autowiring strategy
                _baseStrategy = baseStrategy;

            public bool TryResolveType(IContainer container, Type registeredType, Type resolvingType, out Type instanceType) =>
                // Just uses a logic from the previous autowiring strategy as is
                _baseStrategy.TryResolveType(container, registeredType, resolvingType, out instanceType);

            // Overrides a logic to inject the constant "default name" to every constructors parameters named "name" of type String
            public bool TryResolveConstructor(IContainer container, IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
            {
                if (!_baseStrategy.TryResolveConstructor(container, constructors, out constructor))
                {
                    return false;
                }

                var selectedConstructor = constructor;
                selectedConstructor.Info.GetParameters()
                    // Filters constructor parameters
                    .Where(p => p.Name == "name" && p.ParameterType == typeof(string)).ToList()
                    // Overrides every parameters expression by the constant "default name"
                    .ForEach(p => selectedConstructor.SetExpression(p.Position, Expression.Constant("default name")));

                return true;
            }

            public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
                // Just uses a logic from the previous autowiring strategy as is
                => _baseStrategy.TryResolveInitializers(container, methods, out initializers);
        }
        // }
    }
}
