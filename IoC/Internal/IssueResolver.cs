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
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot resolve instance for the key \"{key}\" from  the container \"{container}\".");
        }

        public IResolver CannotGetResolver(IContainer container, Key key)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot get resolver for the key \"{key}\" from the container \"{container}\".");
        }

        public ConstructorInfo CannotFindConsructor(Type type, params Has[] dependencies)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            throw new InvalidOperationException($"Cannot find an appropriate public constructor in the type \"{type.Name}\" using dependencies: {string.Join(",", dependencies)}.");
        }

        public MethodInfo CannotFindMethod(Type type, HasMethod method)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            throw new InvalidOperationException($"Cannot find an appropriate public method {method.Name} in the type \"{type.Name}\" using dependencies: {string.Join(",", method.Dependencies)}.");
        }

        public Type[] CannotGetGenericTypeArguments(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            throw new InvalidOperationException($"Cannot get generic type arguments from the type \"{type.Name}\".");
        }

        public IFactory CannotBeCeated(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            throw new InvalidOperationException($"An instance of the type \"{type}\" cannot be created.");
        }

        public int CannotFindParameter(ParameterInfo[] parameters, Parameter parameter)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            throw new InvalidOperationException($"The parameter \"{parameter}\" was not found.");
        }

        public void CyclicDependenceDetected(ResolvingContext context, Type type, int reentrancy)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (reentrancy <= 0) throw new ArgumentOutOfRangeException(nameof(reentrancy));
            throw new InvalidOperationException($"The cyclic dependence detected during creating instance of type \"{type.Name}\". The reentrancy is {reentrancy}.");
        }

        public IDisposable CannotRegister(IContainer container, Key[] keys)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            throw new InvalidOperationException($"Keys {string.Join(", ", keys.Select(i => i.ToString()))} cannot be registered in the container \"{container}\".");
        }

        public object CannotResolveParameter(Type type, Has dependency, ParameterInfo parameter)
        {
            throw new InvalidOperationException($"Cannot resolve the parameter \"{parameter.ParameterType} {parameter.Name}\" for \"{parameter.Member.Name}\" creating an instance of type \"{type.Name}\" from args[{dependency.ArgIndex}].");
        }

        public Type CannotParseType(string statementText, int statementLineNumber, int statementPosition, string typeName)
        {
            throw new InvalidOperationException($"Cannot parse the type \"{typeName}\" in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }

        public Lifetime CannotParseLifetime(string statementText, int statementLineNumber, int statementPosition, string lifetimeName)
        {
            throw new InvalidOperationException($"Cannot parse the lifetime \"{lifetimeName}\" in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }

        public object CannotParseTag(string statementText, int statementLineNumber, int statementPosition, string tag)
        {
            throw new InvalidOperationException($"Cannot parse the tag \"{tag}\" in the line {statementLineNumber} for the statement \"{statementText}\" at the position {statementPosition}.");
        }
    }
}
