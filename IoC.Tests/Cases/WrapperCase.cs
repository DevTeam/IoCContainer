namespace IoC.Tests.Cases
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Moq;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class WrapperCase
    {
        [Fact]
        public void Run()
        {
            var console = new Mock<IConsole>();

            // Configure base container and base logger
            using (var baseContainer = Container.Create("base"))
            using (baseContainer.Bind<IConsole>().To(ctx => console.Object))
            // And add base logger
            using (baseContainer.Bind<ILogger>().To(typeof(Logger)))
            {
                // Configure some new container
                using (var myContainer = baseContainer.CreateChild("my"))
                // And add some console
                using (myContainer.Bind<IConsole>().To(ctx => console.Object))
                // And add logger wrapper, specifing that resolving of the "logger" dependency should be done from the parent container
                using (myContainer.Bind<ILogger>().To(typeof(TimeLogger), Has.Ref("logger", Scope.Parent)))
                {
                    var logger = myContainer.Get<ILogger>();

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
            private readonly ILogger _logger;

            public TimeLogger(ILogger logger)
            {
                _logger = logger;
            }

            public void Log(string message)
            {
                _logger.Log(DateTimeOffset.Now + ": " + message);
            }
        }
    }
}
