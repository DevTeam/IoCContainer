namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using Extensibility;

    internal class DefaultAutowiringStrategy: IAutowiringStrategy
    {
        [NotNull] private readonly IContainer _container;

        public DefaultAutowiringStrategy([NotNull] IContainer container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public IMethod<ConstructorInfo> SelectConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors)
        {
            return constructors.FirstOrDefault() ?? _container.Resolve<IIssueResolver>().CannotFindConstructor(Enumerable.Empty<IMethod<ConstructorInfo>>());
        }

        public IEnumerable<IMethod<MethodInfo>> GetInitializers(IEnumerable<IMethod<MethodInfo>> methods)
        {
            yield break;
        }
    }
}
