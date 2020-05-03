namespace IoC.Tests.UsageScenarios
{
    using System.Collections.Generic;
    using System.Linq;
    using Shouldly;
    using Xunit;

    public class Plugins
    {
        [Fact]
        // $visible=true
        // $tag=binding
        // $priority=02
        // $description=Plugins
        // {
        public void Run()
        {
            // Given
            var pluginTypes = new[] { typeof(Plugin1), typeof(Plugin2), typeof(Plugin3) };

            using var container = Container.Create();
            foreach (var pluginType in pluginTypes)
            {
                // Should ensure uniqueness of plugin
                var uniquePluginId = pluginType;

                // Bind several opened types by a tag which should ensure uniqueness of binding
                container.Bind(typeof(IPlugin)).Tag(uniquePluginId).To(pluginType);
            }

            // When

            // Resolve instances
            var plugins = container.Resolve<IEnumerable<IPlugin>>();

            // This also works when you cannot use a generic type like IEnumerable<IPlugin>
            // var plugins = container.Resolve<IEnumerable<object>>(typeof(IEnumerable<>).MakeGenericType(typeof(IPlugin)));

            // Then
            var resolvedPluginTypes = plugins.Select(i => i.GetType()).ToList();

            resolvedPluginTypes.Count.ShouldBe(3);

            // We cannot rely on order here
            resolvedPluginTypes.Contains(typeof(Plugin1)).ShouldBeTrue();
            resolvedPluginTypes.Contains(typeof(Plugin2)).ShouldBeTrue();
            resolvedPluginTypes.Contains(typeof(Plugin3)).ShouldBeTrue();
        }

        interface IPlugin { }

        class Plugin1 : IPlugin { }

        class Plugin2 : IPlugin { }

        class Plugin3 : IPlugin { }
        // }
    }
}
