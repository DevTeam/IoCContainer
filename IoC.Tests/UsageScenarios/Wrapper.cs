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
        // $group=09
        // $priority=00
        // $description=Wrapper
        // {
        public void Run()
        {
            var console = new Mock<IConsole>();

            // Create the base container
            using (var baseContainer = Container.Create("base"))
            // Configure the base container for base logger
            using (baseContainer.Bind<IConsole>().To(ctx => console.Object))
            using (baseContainer.Bind<ILogger>().To(typeof(Logger)))
            {
                // Configure some new container
                using (var childContainer = baseContainer.CreateChild("child"))
                // And add some console
                using (childContainer.Bind<IConsole>().To(ctx => console.Object))
                // And add logger's wrapper, specifing that resolving of the "logger" dependency should be done from the parent container
                using (childContainer.Bind<ILogger>().To(typeof(TimeLogger), Has.Ref("logger", Scope.Parent)))
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
