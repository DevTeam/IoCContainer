namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
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
            // Create the container and configure it using configuration class
            using (var container = Container.Create().Using<Generators>())
            {
                // Generate numbers
                var sequential1 = container.Tag(GeneratorType.Sequential).Get<int>();
                var sequential2 = container.Tag(GeneratorType.Sequential).Get<int>();

                sequential2.ShouldBeGreaterThan(sequential1);
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
                // The "value++" operation is tread safe
                yield return container.Bind<int>().Tag(GeneratorType.Sequential).To(() => value++);

                var random = new Random();
                yield return container.Bind<int>().Tag(GeneratorType.Random).To(() => random.Next());
            }
        }
        // }
    }
}
