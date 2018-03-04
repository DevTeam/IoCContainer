// ReSharper disable UnusedVariable
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using Shouldly;
    using Xunit;

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class Generator
    {
        [Fact]
        // $visible=true
        // $group=09
        // $priority=00
        // $description=Generator
        // {
        public void Run()
        {
            Func<int, int, (int, int)> valueGetter = (sequential, random) => (sequential, random);

            // Create a container and configure it using a configuration class
            using (var container = Container.Create().Using<Generators>())
            // Configure a binding to inject 2 number from different generators to result (int, int) of the Func.
            // Inject the dependency of sequential number to the first element
            // Inject the dependency of random number to the second element
            using (container.Bind<(int, int)>().To(
                ctx => valueGetter(
                    ctx.Container.Inject<int>(GeneratorType.Sequential),
                    ctx.Container.Inject<int>(GeneratorType.Random))))
            {
                // Generate sequential numbers
                var sequential1 = container.Tag(GeneratorType.Sequential).Get<int>();
                var sequential2 = container.Tag(GeneratorType.Sequential).Get<int>();

                sequential2.ShouldBe(sequential1 + 1);

                // Generate a random number
                var random = container.Tag(GeneratorType.Random).Get<int>();

                // Generate a set of numbers
                var setOfValues = container.Resolve<(int, int)>();

                setOfValues.Item1.ShouldBe(sequential2 + 1);
            }
        }

        public enum GeneratorType
        {
            Sequential, Random
        }

        public class Generators: IConfiguration
        {
            public IEnumerable<IDisposable> Apply(IContainer container)
            {
                var value = 0;
                // Define function to get sequential integer value
                Func<int> generator = () => Interlocked.Increment(ref value);
                yield return container.Bind<int>().Tag(GeneratorType.Sequential).To(ctx => generator());

                var random = new Random();
                // Define function to get random integer value
                Func<int> randomizer = () => random.Next();
                yield return container.Bind<int>().Tag(GeneratorType.Random).To(ctx => randomizer());
            }
        }
        // }
    }
}
