// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using Moq;
    using Xunit;

    public class AspectOriented
    {
        [Fact]
        // $visible=true
        // $tag=1 Basics
        // $priority=04
        // $description=Aspect Oriented
        // $header=This framework has no special attributes to support aspect oriented autowiring because of a production code should not have references to these special attributes. But this code may contain these attributes by itself. And it is quite easy to use these attributes for aspect oriented autowiring, see the sample below.
        // $footer=Also you can specify your own aspect oriented autowiring by implementing the interface [_IAutowiringStrategy_](IoCContainer/blob/master/IoC/IAutowiringStrategy.cs).
        // {
        public void Run()
        {
            var console = new Mock<IConsole>();

            // Creates an aspect oriented autowiring strategy based the custom attribute `DependencyAttribute`
            var autowiringStrategy = AutowiringStrategies.AspectOriented()
                .Type<TypeAttribute>(attribute => attribute.Type)
                .Order<OrderAttribute>(attribute => attribute.Order)
                .Tag<TagAttribute>(attribute => attribute.Tag);

            // Create the root container
            using (var rootContainer = Container.Create("root"))
            // Configure the child container
            {
                using var childContainer = rootContainer
                    .Create("child")
                    // Configure the child container by the custom aspect oriented autowiring strategy
                    .Bind<IAutowiringStrategy>().To(ctx => autowiringStrategy)
                    .Bind<IConsole>().Tag("MyConsole").To(ctx => console.Object)
                    .Bind<Clock>().To<Clock>()
                    .Bind<string>().Tag("Prefix").To(ctx => "info")
                    .Bind<ILogger>().To<Logger>()
                    .Container;

                // Create a logger
                var logger = childContainer.Resolve<ILogger>();

                // Log the message
                logger.Log("Hello");
            }

            // Check the console output
            console.Verify(i => i.WriteLine(It.IsRegex(".+ - info: Hello")));
        }

        // Represents the dependency attribute to specify `type` for injection.
        [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
        public class TypeAttribute : Attribute
        {
            // A type, which will be used during an injection
            [NotNull] public readonly Type Type;

            public TypeAttribute([NotNull] Type type) => Type = type;
        }

        // Represents the dependency attribute to specify `tag` for injection.
        [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
        public class TagAttribute : Attribute
        {
            // A tag, which will be used during an injection
            [NotNull] public readonly object Tag;

            public TagAttribute([NotNull] object tag) => Tag = tag;
        }

        // Represents the dependency attribute to specify `order` for injection.
        [AttributeUsage(AttributeTargets.Method)]
        public class OrderAttribute : Attribute
        {
            // An order to be used to invoke a method
            public readonly int Order;

            public OrderAttribute(int order) => Order = order;
        }

        public interface IConsole { void WriteLine(string text); }

        public interface IClock { DateTimeOffset Now { get; } }

        public interface ILogger { void Log(string message); }

        public class Logger : ILogger
        {
            private readonly IConsole _console;
            private IClock _clock;

            // Constructor injection
            public Logger([Tag("MyConsole")] IConsole console) => _console = console;

            // Method injection
            [Order(1)]
            public void Initialize([Type(typeof(Clock))] IClock clock) => _clock = clock;

            // Property injection
            public string Prefix { get; [Tag("Prefix"), Order(2)] set; }

            // Adds current time and prefix before a message and writes it to console
            public void Log(string message) => _console?.WriteLine($"{_clock.Now} - {Prefix}: {message}");
        }

        public class Clock : IClock
        {
            // "clockName" dependency is not resolved here but has default value
            public Clock([Type(typeof(string)), Tag("ClockName")] string clockName = "SPb") { }

            public DateTimeOffset Now => DateTimeOffset.Now;
        }
        // }
    }
}
