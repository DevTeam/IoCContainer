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
        // $header=You can specify your own aspect oriented autowiring by implementing the interface [_IAutowiringStrategy_](IoCContainer/blob/master/IoC/IAutowiringStrategy.cs).
        // #footer=Where the [_Type_], [_Order_] and [_Tag_] attributes are used to configure DI.
        // {
        public void Run()
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
            using (var childContainer = rootContainer.CreateChild("child"))
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

            public Logger([Tag("MyConsole")] IConsole console) => _console = console;

            // Method injection
            [Order(1)]
            public void Initialize([Type(typeof(Clock))] IClock clock) => _clock = clock;

            // Property injection
            public string Prefix { get; [Order(2), Tag("Prefix")] set; }

            // Adds current time and prefix before a message and writes it to console
            public void Log(string message) => _console?.WriteLine($"{_clock.Now} - {Prefix}: {message}");
        }
        // }
    }
}