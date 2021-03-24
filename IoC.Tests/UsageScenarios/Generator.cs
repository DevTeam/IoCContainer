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
        // $tag=9 Samples
        // $priority=05
        // $description=Generator sample
        // {
        public void Run()
        {
            // Create and configure the container using a configuration class 'Generators'
            using var container = Container.Create().Using<Generators>();
            using (container.Bind<(int, int)>().To(
                // Uses a function to create a tuple because the expression trees have a limitation in syntax
                ctx => System.ValueTuple.Create(
                    // The first one is of sequential number generator
                    ctx.Container.Inject<int>(GeneratorType.Sequential),
                    // The second one is of random number generator
                    ctx.Container.Inject<int>(GeneratorType.Random))))
            {
                // Generate sequential numbers
                var sequential1 = container.Resolve<int>(GeneratorType.Sequential.AsTag());
                var sequential2 = container.Resolve<int>(GeneratorType.Sequential.AsTag());

                // Check numbers
                sequential2.ShouldBe(sequential1 + 1);

                // Generate a random number
                var random = container.Resolve<int>(GeneratorType.Random.AsTag());

                // Generate a tuple of numbers
                var setOfValues = container.Resolve<(int, int)>();

                // Check sequential numbers
                setOfValues.Item1.ShouldBe(sequential2 + 1);
            }
        }

        // Represents tags for generators
        public enum GeneratorType
        {
            Sequential, Random
        }

        // Represents IoC configuration
        public class Generators: IConfiguration
        {
            public IEnumerable<IToken> Apply(IMutableContainer container)
            {
                var value = 0;
                // Define a function to get sequential integer value
                Func<int> generator = () => Interlocked.Increment(ref value);
                // Bind this function using the corresponding tag 'Sequential'
                yield return container.Bind<int>().Tag(GeneratorType.Sequential).To(ctx => generator());

                var random = new Random();
                // Define a function to get random integer value
                Func<int> randomizer = () => random.Next();
                // Bind this function using the corresponding tag 'Random'
                yield return container.Bind<int>().Tag(GeneratorType.Random).To(ctx => randomizer());
            }
        }
        // }
    }
}
