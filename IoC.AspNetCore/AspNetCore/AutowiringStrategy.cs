namespace IoC.Features.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal class AutowiringStrategy: IAutowiringStrategy
    {
        private readonly IAutowiringStrategy _baseStrategy;

        public AutowiringStrategy(IAutowiringStrategy baseStrategy) =>
            _baseStrategy = baseStrategy;

        public bool TryResolveType(Type registeredType, Type resolvingType, out Type instanceType) =>
            _baseStrategy.TryResolveType(registeredType, resolvingType, out instanceType);

        public bool TryResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
            => (constructor = constructors.OrderBy(i => GetOrder(i.Info)).LastOrDefault()) != null;

        public bool TryResolveInitializers(IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers) =>
            _baseStrategy.TryResolveInitializers(methods, out initializers);

        [MethodImpl((MethodImplOptions)0x100)]
        private static int GetOrder(MethodBase method)
        {
            var parametersCount = method.GetParameters().Length;
            int order;
            switch (parametersCount)
            {
                case 1:
                    order = 200;
                    break;

                default:
                    order = 200 - parametersCount;
                    break;
            }

            if (!method.GetCustomAttributes(typeof(ObsoleteAttribute), true).Any())
            {
                order <<= 8;
            }

            if (method.IsPublic)
            {
                order <<= 8;
            }

            return order;
        }
    }
}
