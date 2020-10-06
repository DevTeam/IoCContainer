namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

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
            => (constructor = constructors.OrderBy(i => GetOrder(container, i.Info)).FirstOrDefault()) != null;

        public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = Enumerable.Empty<IMethod<MethodInfo>>();
            return true;
        }

        [MethodImpl((MethodImplOptions)0x100)]
        private static int GetOrder(IContainer container, MethodBase method)
        {
            var order = 1;
            var parameters = method.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                var parameter = parameters[i];
                if (!container.IsBound(parameter.ParameterType) && !container.CanResolve(parameter.ParameterType))
                {
                    return int.MaxValue;
                }

                if (parameter.IsOut)
                {
                    return int.MaxValue;
                }

                if (!parameter.ParameterType.Descriptor().IsPublic())
                {
                    order += 2;
                }

                order += 1;
            }

            if (method.GetCustomAttributes(typeof(ObsoleteAttribute), true).Any())
            {
                order <<= 4;
            }

            return order;
        }
    }
}
