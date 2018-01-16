namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal sealed class BindingContext
    {
        public static readonly BindingContext Empty = new BindingContext(Enumerable.Empty<Assembly>(), Enumerable.Empty<string>(), Enumerable.Empty<Binding>());

        public BindingContext(
            [NotNull] IEnumerable<Assembly> assemblies,
            [NotNull] IEnumerable<string> namespaces,
            [NotNull] IEnumerable<Binding> bindings)
        {
            if (assemblies == null) throw new ArgumentNullException(nameof(assemblies));
            if (namespaces == null) throw new ArgumentNullException(nameof(namespaces));
            if (bindings == null) throw new ArgumentNullException(nameof(bindings));
            Assemblies = assemblies as Assembly[] ?? assemblies.ToArray();
            Namespaces = namespaces as string[] ?? namespaces.ToArray();
            Bindings = bindings as Binding[] ?? bindings.ToArray();
        }

        [NotNull] public IEnumerable<Assembly> Assemblies { get; }

        [NotNull] public IEnumerable<string> Namespaces { get; }

        [NotNull] public IEnumerable<Binding> Bindings { get; }
    }
}
