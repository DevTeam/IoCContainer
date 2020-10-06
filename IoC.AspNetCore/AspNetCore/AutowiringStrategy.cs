namespace IoC.Features.AspNetCore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    
    internal class AutowiringStrategy: IAutowiringStrategy
    {
        public bool TryResolveType(IContainer container, Type registeredType, Type resolvingType, out Type instanceType)
        {
            instanceType = default(Type);
            return false;
        }

        public bool TryResolveConstructor(IContainer container, IEnumerable<IMethod<ConstructorInfo>> constructors, out IMethod<ConstructorInfo> constructor)
        {
            var ctors =
                from ctor in constructors
                where !ctor.Info.GetCustomAttributes(typeof(ObsoleteAttribute), true).Any()
                let parameters = ctor.Info.GetParameters()
                orderby parameters.Length descending
                where parameters.All(parameter => container.IsBound(parameter.ParameterType) || container.CanResolve(parameter.ParameterType))
                select ctor;

            constructor = ctors.FirstOrDefault();
            return constructor != null;
        }

        public bool TryResolveInitializers(IContainer container, IEnumerable<IMethod<MethodInfo>> methods, out IEnumerable<IMethod<MethodInfo>> initializers)
        {
            initializers = default(IEnumerable<IMethod<MethodInfo>>);
            return false;
        }
    }
}
