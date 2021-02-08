// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable RedundantAssignment
namespace IoC.Tests.IntegrationTests
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
    public class AspectOrientedAutowiringTests
    {
        [Fact]
        public void ShouldUseAspectOrientedAutowiringStrategy()
        {
            var console = new Mock<IConsole>();

            // Creates an aspect oriented autowiring strategy specifying which attributes should be used
            // and which properties should be used to configure DI
            var autowiringStrategy = AutowiringStrategies.AspectOriented()
                .Type<TypeAttribute>(attribute => attribute.Type)
                .Order<OrderAttribute>(attribute => attribute.Order)
                .Tag<TagAttribute>(attribute => attribute.Tag);

            // Create the root container
            using (var rootContainer = Container.Create("root"))
            // Configure the child container
            using (var childContainer = rootContainer.Create("Some child container"))
            // Configure the child container by the custom aspect oriented autowiring strategy
            using (childContainer.Bind<IAutowiringStrategy>().To(ctx => autowiringStrategy))
            // Configure the child container
            using (childContainer.Bind<IConsole>().Tag(Tags.MyConsole).To(ctx => console.Object))
            using (childContainer.Bind<Clock>().To<Clock>())
            using (childContainer.Bind<string>().Tag(Tags.Prefix).To(ctx => "info"))
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

        // This enum is not required and created just to manage all tags.
        // It is possible to use tags of any type.
        public enum Tags
        {
            MyConsole,
            Prefix
        }

        // Represents the tag attribute to specify `tag` for injection.
        [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]
        public class TypeAttribute : Attribute
        {
            // The tag, which will be used during an injection
            [CanBeNull] public readonly Type Type;

            public TypeAttribute([CanBeNull] Type type) => Type = type;
        }

        // Represents the order attribute and defines the sequence of calls to initialization methods.
        [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method)]
        public class OrderAttribute : Attribute
        {
            // The order to be used to invoke a method
            public readonly int Order;

            public OrderAttribute(int order = 0) => Order = order;
        }

        // Represents the tag attribute to specify `tag` for injection.
        [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Method)]        
        public class TagAttribute: Attribute
        {
            // The tag, which will be used during an injection
            [CanBeNull] public readonly object Tag;

            public TagAttribute([CanBeNull] object tag) => Tag = tag;
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

            public Logger([Tag(Tags.MyConsole)] IConsole console) => _console = console;

            // Method injection
            [Order(1)]
            public bool Initialize(
                [Type(typeof(Clock))] IClock clock,
                out Exception error,
                [Type(typeof(Clock))] ref IClock clockRef)
            {
                _clock = clock;
                error = new Exception("Some error");
                clockRef = null;
                return false;
            }

            // Property injection
            public string Prefix { get; [Order(2), Tag(Tags.Prefix)] set; }

            // Adds current time and prefix before a message and writes it to console
            public void Log(string message) => _console?.WriteLine($"{_clock.Now} - {Prefix}: {message}");
        }
    }
}