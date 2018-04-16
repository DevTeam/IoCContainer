#if !NET40
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    public class AspectOrientedAutowiring
    {
        [Fact]
        // $visible=true
        // $group=08
        // $priority=00
        // $description=Aspect Oriented Autowiring
        // {
        public void Run()
        {
            var console = new Mock<IConsole>();

            // Create a base container
            using (var baseContainer = Container.Create("base"))
            // Configure some child container
            using (var childContainer = baseContainer.CreateChild("child"))
            // Configure the child container by custom aspect oriented autowring strategy
            using (childContainer.Bind<IAutowiringStrategy>().To<AspectOrientedAutowiringStrategy>())
            // Configure the container
            using (childContainer.Bind<IConsole>().To(ctx => console.Object))
            using (childContainer.Bind<IClock>().Tag("MyClock").To<Clock>())
            using (childContainer.Bind<string>().Tag("Prefix").To(ctx => "info"))
            using (childContainer.Bind<ILogger>().To<Logger>())
            {
                var logger = childContainer.Resolve<ILogger>();

                // Log message
                logger.Log("Hello");
            }

            // Check the console output
            console.Verify(i => i.WriteLine(It.IsRegex(".+ - info: Hello")));
        }

        /// <summary>
        /// Attribute for constructor or methods that is ready for autowiring
        /// </summary>
        [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
        public class AutowiringAttribute : Attribute
        {
            public AutowiringAttribute(int order = 0, object defaultTag = null)
            {
                Order = order;
                DefaultTag = defaultTag;
            }

            public int Order { get; }

            public object DefaultTag { get; }
        }

        /// <summary>
        /// Parameters` attribute to define a tag value
        /// </summary>
        [AttributeUsage(AttributeTargets.Parameter)]
        public class TagAttribute: Attribute
        {
            public TagAttribute([CanBeNull] object tag) => Tag = tag;

            [CanBeNull] public object Tag { get; }
        }

        private class AspectOrientedAutowiringStrategy : IAutowiringStrategy
        {
            public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType)
            {
                instanceType = default(Type);
                return false;
            }

            public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
            {
                constructor = (
                    from ctor in constructors
                    let autowringAttribute = ctor.Info.GetCustomAttribute<AutowiringAttribute>()
                    // filters constructors containing the attribute Autowiring
                    where autowringAttribute != null
                    // sorts constructors by autowringAttribute.Order
                    orderby autowringAttribute.Order
                    select ctor)
                    // Get a first appropriate constructor
                    .First();

                return true;
            }

            public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
            {
                initializers =
                    from method in methods
                    let autowringAttribute = method.Info.GetCustomAttribute<AutowiringAttribute>()
                    // filters methods/property setters containing the attribute Autowiring
                    where autowringAttribute != null
                    // sorts methods/property setters by autowringAttribute.Order
                    orderby autowringAttribute.Order
                    where (
                            from parameter in method.Info.GetParameters()
                            let injectAttribute = parameter.GetCustomAttribute<TagAttribute>()
                            // filters parameters containing a custom tag value to make a dependency injection
                            where injectAttribute?.Tag != null || autowringAttribute.DefaultTag != null
                            // redefines the default dependency
                            select method.TryInjectDependency(parameter.Position, parameter.ParameterType, injectAttribute?.Tag ?? autowringAttribute.DefaultTag))
                        // checks that all custom injection were successfully
                        .All(isInjected => isInjected)
                    select method;

                return true;
            }
        }

        public interface IConsole
        {
            void WriteLine(string test);
        }

        public interface IClock
        {
            DateTimeOffset Now { get; }
        }

        public class Clock : IClock
        {
            [Autowiring] public Clock() { }

            public DateTimeOffset Now => DateTimeOffset.Now;
        }

        public interface ILogger
        {
            void Log(string message);
        }

        public class Logger : ILogger
        {
            private readonly IConsole _console;
            private IClock _clock;

            // Constructor injection
            [Autowiring]
            public Logger(IConsole console) => _console = console;

            // Method injection
            [Autowiring(1)]
            public void Initialize([Tag("MyClock")] IClock clock) => _clock = clock;

            // Property injection
            public string Prefix { get; [Autowiring(2, "Prefix")] set; }

            public void Log(string message) => _console?.WriteLine($"{_clock.Now} - {Prefix}: {message}");
        }
        // }
    }
}
#endif