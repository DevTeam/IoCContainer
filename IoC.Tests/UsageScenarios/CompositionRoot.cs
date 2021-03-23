// ReSharper disable IdentifierTypo
// ReSharper disable RedundantTypeArgumentsOfMethod
// ReSharper disable EmptyConstructor
// ReSharper disable UnusedVariable
// ReSharper disable UnusedParameter.Local
// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public class CompositionRoot
    {
        [Fact]
        // $visible=true
        // $tag=1 Basics
        // $priority=00
        // $description=Composition Root
        // {
        public void Run()
        {
            // Host runs a program
            Program.TestMain();
        }

        class Program
        {
            // The application's entry point
            public static void TestMain()
            {
                using var container =
                    Container.Create()
                    .Using<Configuration>();

                // The Composition Root is a single location for objects construction
                // it should be as close as possible to the application's entry point
                var root = container.Resolve<Program>();

                // Runs a logic
                root.Run();
            }

            // Injects dependencies via a constructor
            internal Program(IService service)
            {
                 // Saves dependencies as internal fields
            }

            private void Run()
            {
                // Implements a logic using dependencies
            }
        }

        // Represents the IoC container configuration
        class Configuration: IConfiguration
        {
            public IEnumerable<IToken> Apply(IMutableContainer container)
            {
                yield return container
                    .Bind<IDependency>().To<Dependency>()
                    .Bind<IService>().To<Service>();
            }
        }
        // }
    }
}
