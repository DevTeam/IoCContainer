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
    public class AspectOrientedAutoWiring
    {
        [Fact]
        // $visible=true
        // $tag=binding
        // $priority=10
        // $description=Aspect Oriented Auto-wiring
        // $header=You can specify your own aspect oriented auto-wiring by implementing the interface [_IAutowiringStrategy_](IoCContainer/blob/master/IoC/IAutowiringStrategy.cs).
        // #footer=Where the [_Autowiring_] and [_Tag_] attributes are used to highlight and configure injecting points.
        // {
        public void Run()
        {
            var console = new Mock<IConsole>();

            // Create the root container
            using (var rootContainer = Container.Create("root"))
            // Configure еру child container
            using (var childContainer = rootContainer.CreateChild("child"))
            // Configure the child container by custom aspect oriented autowiring strategy
            using (childContainer.Bind<IAutowiringStrategy>().To<AspectOrientedAutowiringStrategy>())
            // Configure the child container
            using (childContainer.Bind<IConsole>().To(ctx => console.Object))
            using (childContainer.Bind<IClock>().Tag("MyClock").To<Clock>())
            using (childContainer.Bind<string>().Tag("Prefix").To(ctx => "info"))
            using (childContainer.Bind<ILogger>().To<Logger>())
            {
                // Create a logger
                var logger = childContainer.Resolve<ILogger>();

                // Log the message
                logger.Log("Hello");
            }

            // Check the console output
            console.Verify(i => i.WriteLine(It.IsRegex(".+ - info: Hello")));
        }

        // Represents the attribute to mark a constructor or a method that is ready for auto-wiring
        [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
        public class AutowiringAttribute : Attribute
        {
            public AutowiringAttribute(int order = 0, object defaultTag = null)
            {
                Order = order;
                DefaultTag = defaultTag;
            }

            // The order to be used to invoke a method
            public int Order { get; }

            // The default tag
            public object DefaultTag { get; }
        }

        // Represents the attribute to mark a parameters by a tag, which will be used during an injection
        [AttributeUsage(AttributeTargets.Parameter)]
        public class TagAttribute: Attribute
        {
            public TagAttribute([CanBeNull] object tag) => Tag = tag;

            // The tag, which will be used during an injection
            [CanBeNull] public object Tag { get; }
        }

        // Represents a custom aspect oriented autowiring strategy
        private class AspectOrientedAutowiringStrategy : IAutowiringStrategy
        {
            // Resolves type to create an instance
            public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType)
            {
                instanceType = default(Type);
                // Says that the default logic should be used
                return false;
            }

            // Resolves a constructor from a set of available constructors
            public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
            {
                constructor = (
                    // for each available constructor
                    from ctor in constructors
                    // tries to get 'AutowiringAttribute'
                    let autowiringAttribute = ctor.Info.GetCustomAttribute<AutowiringAttribute>()
                    // filters the constructor containing the attribute 'AutowiringAttribute'
                    where autowiringAttribute != null
                    // sorts constructors by 'Order' property
                    orderby autowiringAttribute.Order
                    select ctor)
                    // gets the first appropriate constructor
                    .First();

                // Says that current logic should be used
                return true;
            }

            // Resolves initializing methods from a set of available methods/setters in the order which will be used to invoke them
            public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
            {
                initializers =
                    // for each available method
                    from method in methods
                    // tries to get AutowiringAttribute
                    let autowiringAttribute = method.Info.GetCustomAttribute<AutowiringAttribute>()
                    // filters methods/property setters containing the attribute 'AutowiringAttribute'
                    where autowiringAttribute != null
                    // sorts methods/property setters by 'Order' property
                    orderby autowiringAttribute.Order
                    where (
                            // for each parameter
                            from parameter in method.Info.GetParameters()
                            // tries to get 'TagAttribute'
                            let injectAttribute = parameter.GetCustomAttribute<TagAttribute>()
                            // filters parameters containing a custom tag value to make a dependency injection
                            where injectAttribute?.Tag != null || autowiringAttribute.DefaultTag != null
                            // defines the dependency injection
                            select method.TryInjectDependency(parameter.Position, parameter.ParameterType, injectAttribute?.Tag ?? autowiringAttribute.DefaultTag))
                        // checks that each injection was successful
                        .All(isInjected => isInjected)
                    select method;

                // Says that current logic should be used
                return true;
            }
        }

        public interface IConsole
        {
            // Writes a text
            void WriteLine(string text);
        }

        public interface IClock
        {
            // Returns current time
            DateTimeOffset Now { get; }
        }

        public class Clock : IClock
        {
            [Autowiring] public Clock() { }

            public DateTimeOffset Now => DateTimeOffset.Now;
        }

        public interface ILogger
        {
            // Logs a message
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

            // Adds current time and prefix before a message and writes it to console
            public void Log(string message) => _console?.WriteLine($"{_clock.Now} - {Prefix}: {message}");
        }
        // }
    }
}
#endif