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
        // $priority=02
        // $description=Aspect-oriented DI
        // $header=This framework has no special predefined attributes to support aspect-oriented auto wiring because a non-infrastructure code should not have references to this framework. But this code may contain these attributes by itself. And it is quite easy to use these attributes for aspect-oriented auto wiring, see the sample below.
        // $footer=You can also specify your own aspect-oriented auto wiring by implementing the interface [_IAutowiringStrategy_](IoCContainer/blob/master/IoC/IAutowiringStrategy.cs).
        // {
        public void Run()
        {
            var console = new Mock<IConsole>();

            // Creates an aspect - oriented auto wiring strategy specifying
            // which attributes should be used and which properties should be used to configure DI
            var autowiringStrategy = AutowiringStrategies.AspectOriented()
                .Type<TypeAttribute>(attribute => attribute.Type)
                .Order<OrderAttribute>(attribute => attribute.Order)
                .Tag<TagAttribute>(attribute => attribute.Tag);

            using var container = Container
                .Create()
                // Configure the container to use DI aspects
                .Bind<IAutowiringStrategy>().To(ctx => autowiringStrategy)
                .Bind<IConsole>().Tag("MyConsole").To(ctx => console.Object)
                .Bind<string>().Tag("Prefix").To(ctx => "info")
                .Bind<ILogger>().To<Logger>()
                .Container;

            // Create a logger
            var logger = container.Resolve<ILogger>();

            // Log the message
            logger.Log("Hello");

            // Check the output has the appropriate format
            console.Verify(i => i.WriteLine(It.IsRegex(".+ - info: Hello")));
        }

        // Represents the dependency aspect attribute to specify a type for injection.
        [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
        public class TypeAttribute : Attribute
        {
            // A type, which will be used during an injection
            public readonly Type Type;

            public TypeAttribute(Type type) => Type = type;
        }

        // Represents the dependency aspect attribute to specify a tag for injection.
        [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
        public class TagAttribute : Attribute
        {
            // A tag, which will be used during an injection
            public readonly object Tag;

            public TagAttribute(object tag) => Tag = tag;
        }

        // Represents the dependency aspect attribute to specify an order for injection.
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

            // Constructor injection using the tag "MyConsole"
            public Logger([Tag("MyConsole")] IConsole console) => _console = console;

            // Method injection after constructor using specified type _Clock_
            [Order(1)] public void Initialize([Type(typeof(Clock))] IClock clock) => _clock = clock;

            // Setter injection after the method injection above using the tag "Prefix"
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
