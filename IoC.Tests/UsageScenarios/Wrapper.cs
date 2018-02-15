namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Wrapper
    {
        [Fact]
        // $visible=true
        // $group=08
        // $priority=00
        // $description=Wrapper
        // {
        public void Run()
        {
            var console = new Mock<IConsole>();

            // Create a base container
            using (var baseContainer = Container.Create("base"))
            // Configure it for base logger
            using (baseContainer.Bind<IConsole>().To(ctx => console.Object))
            using (baseContainer.Bind<ILogger>().To<Logger>())
            {
                // Configure some child container
                using (var childContainer = baseContainer.CreateChild("child"))
                // Configure console
                using (childContainer.Bind<IConsole>().To(ctx => console.Object))
                using (childContainer.Bind<ILogger>().To<TimeLogger>(
                    // Inject the logger from the parent container to our new logger
                    ctx => new TimeLogger(ctx.Container.Parent.Inject<ILogger>())))
                {
                    var logger = childContainer.Get<ILogger>();

                    // Log message
                    logger.Log("Hello");
                }
            }

            // Check the console output
            console.Verify(i => i.WriteLine(It.IsRegex(".+: Hello")));
        }

        public interface IConsole
        {
            void WriteLine(string test);
        }

        public interface ILogger
        {
            void Log(string message);
        }

        public class Logger : ILogger
        {
            private readonly IConsole _console;

            public Logger(IConsole console)
            {
                _console = console;
            }

            public void Log(string message)
            {
                _console.WriteLine(message);
            }
        }

        public class TimeLogger: ILogger
        {
            private readonly ILogger _baseLogger;

            public TimeLogger(ILogger baseLogger)
            {
                _baseLogger = baseLogger;
            }

            public void Log(string message)
            {
                _baseLogger.Log(DateTimeOffset.Now + ": " + message);
            }
        }
        // }
    }
}
