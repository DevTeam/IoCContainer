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

        private DefaultAutowiringStrategy()
        { }

        public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType)
        {
            instanceType = default(Type);
            return false;
        }

        public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
            => (constructor = constructors.OrderBy(i => GetOrder(i.Info)).FirstOrDefault()) != null;

        public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = Enumerable.Empty<IMethod<MethodInfo>>();
            return true;
        }

        [MethodImpl((MethodImplOptions)256)]
        private static int GetOrder(MethodBase method)
        {
            var order = method.GetParameters().Length + 1;

            if (method.GetCustomAttributes(typeof(ObsoleteAttribute), true).Any())
            {
                order <<= 4;
            }

            if (!method.IsPublic)
            {
                order <<= 8;
            }

            return order;
        }
    }
}
