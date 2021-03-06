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
                // The Composition Root is an application infrastructure component
                // It should be as close as possible to the application's entry point
                using var composition =
                    // Creates the IoC container: a IoC Container should only be referenced to build a Composition Root
                    Container.Create()
                    // Configures the container
                    .Using<Configuration>()
                    // Creates the composition root: single location for object construction
                    .Build<Program>();

                // Runs a logic
                composition.Root.Run();
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
