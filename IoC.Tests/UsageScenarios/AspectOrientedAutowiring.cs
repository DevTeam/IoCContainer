// ReSharper disable RedundantTypeArgumentsOfMethod
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
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
        // $tag=binding
        // $priority=10
        // $description=Aspect Oriented Autowiring
        // $footer=Also you can specify your own aspect oriented autowiring by implementing the interface [_IAutowiringStrategy_](IoCContainer/blob/master/IoC/IAutowiringStrategy.cs).
        // {
        public void Run()
        {
            var console = new Mock<IConsole>();

            // Creates an aspect oriented autowiring strategy based on some custom `DependencyAttribute`
            var autowiringStrategy = AutowiringStrategies.AspectOriented()
                .Type<DependencyAttribute>(attribute => attribute.Type)
                .Order<DependencyAttribute>(attribute => attribute.Order)
                .Tag<DependencyAttribute>(attribute => attribute.Tag);

            // Create the root container
            using (var rootContainer = Container.Create("root"))
            // Configure the child container
            using (var childContainer = rootContainer.Create("child"))
            // Configure the child container by the custom aspect oriented autowiring strategy
            using (childContainer.Bind<IAutowiringStrategy>().To(ctx => autowiringStrategy))
            // Configure the child container
            using (childContainer.Bind<IConsole>().Tag("MyConsole").To(ctx => console.Object))
            using (childContainer.Bind<Clock>().To<Clock>())
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

        // Represents the dependency attribute to specify `tag`, 'order' or `type` for injection.
        [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
        public class DependencyAttribute : Attribute
        {
            // The tag, which will be used during an injection
            [CanBeNull] public readonly object Tag;

            // The order to be used to invoke a method
            public readonly int Order;

            // The tag, which will be used during an injection
            [CanBeNull] public readonly Type Type;

            public DependencyAttribute([CanBeNull] object tag = null, int order = 0, [CanBeNull] Type type = null)
            {
                Tag = tag;
                Order = order;
                Type = type;
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

            public Logger([Dependency(tag: "MyConsole")] IConsole console) => _console = console;

            // Method injection
            [Dependency(order: 1)]
            public void Initialize([Dependency(type: typeof(Clock))] IClock clock) => _clock = clock;

            // Property injection
            public string Prefix { get; [Dependency(tag: "Prefix", order: 2)] set; }

            // Adds current time and prefix before a message and writes it to console
            public void Log(string message) => _console?.WriteLine($"{_clock.Now} - {Prefix}: {message}");
        }
        // }
    }
}