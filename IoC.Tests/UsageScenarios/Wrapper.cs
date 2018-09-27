namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class Wrapper
    {
        [Fact]
        // $visible=true
        // $tag=samples
        // $priority=00
        // $description=Wrapper
        // {
        public void Run()
        {
            var console = new Mock<IConsole>();

            // Create and configure the root container
            using (var rootContainer = Container.Create("root"))
            using (rootContainer.Bind<IConsole>().To(ctx => console.Object))
            using (rootContainer.Bind<ILogger>().To<Logger>())
            {
                // Create and configure the child container
                using (var childContainer = rootContainer.CreateChild("child"))
                // Bind IConsole
                using (childContainer.Bind<IConsole>().To(ctx => console.Object))
                // Bind 'ILogger' to the instance creation, actually represented as an expression tree
                using (childContainer.Bind<ILogger>().To<TimeLogger>(
                    // Inject the logger from the parent container to an instance of type TimeLogger
                    ctx => new TimeLogger(ctx.Container.Parent.Inject<ILogger>())))
                {
                    // Create a logger
                    var logger = childContainer.Resolve<ILogger>();

                    // Log the message
                    logger.Log("Hello");
                }
            }

            // Check the console output
            console.Verify(i => i.WriteLine(It.IsRegex(".+: Hello")));
        }

        public interface IConsole
        {
            // Writes a text
            void WriteLine(string text);
        }

        public interface ILogger
        {
            // Logs a message
            void Log(string message);
        }

        public class Logger : ILogger
        {
            private readonly IConsole _console;

            // Stores console to field
            public Logger(IConsole console) => _console = console;

            // Logs a message to console
            public void Log(string message) => _console.WriteLine(message);
        }

        public class TimeLogger: ILogger
        {
            private readonly ILogger _baseLogger;

            public TimeLogger(ILogger baseLogger) => _baseLogger = baseLogger;

            // Adds current time before a message and writes it to console
            public void Log(string message) => _baseLogger.Log(DateTimeOffset.Now + ": " + message);
        }
        // }
    }
}
