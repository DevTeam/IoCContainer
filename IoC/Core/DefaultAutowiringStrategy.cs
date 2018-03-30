namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
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
            var ctors = constructors.ToArray();
            return ctors.FirstOrDefault() ?? _container.Resolve<IIssueResolver>().CannotFindConstructor(ctors);
        }

        public IEnumerable<IMethod<MethodInfo>> GetMethods(IEnumerable<IMethod<MethodInfo>> methods)
        {
            yield break;
        }
    }
}
