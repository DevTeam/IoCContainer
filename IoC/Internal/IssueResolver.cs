namespace IoC.Internal
{
    using System;
    using System.Linq;
    using System.Reflection;

    internal sealed class IssueResolver : IIssueResolver
    {
        public static readonly IIssueResolver Shared = new IssueResolver();

        private IssueResolver()
        {
        }

        public object CannotResolve(IContainer container, Key key)
        {
            throw new InvalidOperationException($"Cannot resolve instance for the key \"{key}\" from  the container \"{container}\".");
        }

        public IResolver CannotGetResolver(IContainer container, Key key)
        {
            throw new InvalidOperationException($"Cannot get resolver for the key \"{key}\" from the container \"{container}\".");
        }

        public ConstructorInfo CannotFindConsructor(Type type, params Has[] dependencies)
        {
            throw new InvalidOperationException($"Cannot find an appropriate public constructor in the type \"{type.Name}\" using dependencies: {string.Join(",", dependencies)}.");
        }

        public MethodInfo CannotFindMethod(Type type, HasMethod method)
        {
            throw new InvalidOperationException($"Cannot find an appropriate public method {method.Name} in the type \"{type.Name}\" using dependencies: {string.Join(",", method.Dependencies)}.");
        }

        public Type[] CannotGetGenericTypeArguments(Type type)
        {
            throw new InvalidOperationException($"Cannot get generic type arguments from the type \"{type.Name}\".");
        }

        public IFactory CannotBeCeated(Type type)
        {
            throw new InvalidOperationException($"An instance of the type \"{type}\" cannot be created.");
        }

        public int CannotFindParameter(ParameterInfo[] parameters, Parameter parameter)
        {
            throw new InvalidOperationException($"The parameter \"{parameter}\" was not found.");
        }

        public void CyclicDependenceDetected(Context context, Type type, int reentrancy)
        {
            throw new InvalidOperationException($"The cyclic dependence detected during creating instance of type \"{type.Name}\". The reentrancy is {reentrancy}.");
        }

        public IDisposable CannotRegister(IContainer container, Key[] keys)
        {
            throw new InvalidOperationException($"Keys {string.Join(", ", keys.Select(i => i.ToString()))} cannot be registered in the container \"{container}\".");
        }
    }
}
