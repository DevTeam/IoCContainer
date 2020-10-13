namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
    internal sealed class DefaultAutowiringStrategy : IAutowiringStrategy
    {
        public static readonly IAutowiringStrategy Shared = new DefaultAutowiringStrategy();

        private DefaultAutowiringStrategy() { }

        public bool TryResolveType(IContainer container, Type registeredType, Type resolvingType, out Type instanceType)
        {
            instanceType = default(Type);
            return false;
        }

        public bool TryResolveConstructor(IContainer container, IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
        {
            var ctors =
                from ctor in constructors
                let isNotObsoleted = !ctor.Info.IsDefined(typeof(ObsoleteAttribute), false)
                let parameters = ctor.Info.GetParameters()
                let canBeResolved = parameters.All(parameter =>
                    parameter.IsOptional ||
                    container.IsBound(parameter.ParameterType) ||
                    container.CanResolve(parameter.ParameterType))
                let order = (parameters.Length + 1) * (canBeResolved ? 0xffff : 1) * (isNotObsoleted ? 0xff : 1)
                orderby order descending
                select ctor;

            constructor = ctors.FirstOrDefault();
            return constructor != null;
        }
        public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = Enumerable.Empty<IMethod<MethodInfo>>();
            return true;
        }
    }
}
