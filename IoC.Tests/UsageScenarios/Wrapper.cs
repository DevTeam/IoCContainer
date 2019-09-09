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
            var clock = new Mock<IClock>();
            var now = new DateTimeOffset(2019, 9, 9, 12, 31, 34, TimeSpan.FromHours(3));
            clock.SetupGet(i => i.Now).Returns(now);

            // Create and configure the root container
            using (var rootContainer = Container.Create("root"))
            using (rootContainer.Bind<IConsole>().To(ctx => console.Object))
            using (rootContainer.Bind<ILogger>().To<Logger>())
            {
                // Create and configure the child container
                using var childContainer = rootContainer.Create("Some child container");
                using (childContainer.Bind<IConsole>().To(ctx => console.Object))
                using (childContainer.Bind<IClock>().To(ctx => clock.Object))
                // Bind 'ILogger' to the instance creation, actually represented as an expression tree
                using (childContainer.Bind<ILogger>().To<TimeLogger>(
                    // Inject the base logger from the parent container and the clock from the current container
                    ctx => new TimeLogger(ctx.Container.Parent.Inject<ILogger>(), ctx.Container.Inject<IClock>())))
                {
                    // Create a logger
                    var logger = childContainer.Resolve<ILogger>();

                    // Log the message
                    logger.Log("Hello");
                }
            }

            // Check the console output
            console.Verify(i => i.WriteLine($"{now}: Hello"));
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

        public interface IClock
        {
            DateTimeOffset Now { get; }
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
            private readonly IClock _clock;

            public TimeLogger(ILogger baseLogger, IClock clock)
            {
                _baseLogger = baseLogger;
                _clock = clock;
            }

            // Adds current time before a message and writes it to console
            public void Log(string message) => _baseLogger.Log($"{_clock.Now}: {message}");
        }
        // }
    }
}
