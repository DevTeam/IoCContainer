// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable UnusedParameter.Local
namespace IoC.Tests.UsageScenarios
{
    using System;
    using System.Linq;
    using Features;
    using Xunit;

    public class ResolveUnboundForAbstractions
    {
        [Fact]
        // $visible=true
        // $tag=5 Advanced
        // $priority=02
        // $description=Resolve Unbound for abstractions
        // $header=The feature _ResolveUnboundFeature_ allows you to resolve any implementation type from the container regardless of whether or not you specifically bound it and find appropriate implementations for abstractions using "key resolver".
        // {
        public void Run()
        {
            using var resolveUnboundFeature = new ResolveUnboundFeature(Options.ResolveArgs | Options.ResolveDefaults, KeyResolver);
            using var container = Container.Create().Using(resolveUnboundFeature);

            // Resolve an instance of unregistered type
            container.Resolve<IService>();
        }

        // Find an appropriate implementation
        private static Key KeyResolver(Key key) =>
            new Key(key.Type.Assembly.GetTypes()
                .Where(implementationType => !implementationType.IsInterface && !implementationType.IsAbstract)
                .FirstOrDefault(implementationType => key.Type.IsAssignableFrom(implementationType))
                    ?? throw new InvalidOperationException($"Cannot find a type assignable to {key}."),
                key.Tag);

        // }
    }
}
