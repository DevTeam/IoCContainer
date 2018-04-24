namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using Extensibility;

    internal sealed class IssueResolver : IIssueResolver
    {
        public static readonly IIssueResolver Shared = new IssueResolver();

        private IssueResolver() { }

        public Tuple<IDependency, ILifetime> CannotResolveDependency(IContainer container, Key key)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot find the dependency for the key \"{key}\" from  the container \"{container}\". Details:\n{GetContainerDetails(container)}");
        }

        public Resolver<T> CannotGetResolver<T>(IContainer container, Key key)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            throw new InvalidOperationException($"Cannot get resolver for the key \"{key}\" from the container \"{container}\". Details:\n{GetContainerDetails(container)}");
        }

        public Type[] CannotGetGenericTypeArguments(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            throw new InvalidOperationException($"Cannot get generic type arguments from the type \"{type.Name}\".");
        }

        public void CyclicDependenceDetected(Key key, int reentrancy)
        {
            if (reentrancy <= 0) throw new ArgumentOutOfRangeException(nameof(reentrancy));
            if (reentrancy >= 256)
            {
                throw new InvalidOperationException($"The cyclic dependence detected resolving the dependency \"{key}\". The reentrancy is {reentrancy}.");
            }
        }

        public IDisposable CannotRegister(IContainer container, Key[] keys)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (keys == null) throw new ArgumentNullException(nameof(keys));
            throw new InvalidOperationException($"Keys {string.Join(", ", keys.Select(i => i.ToString()))} cannot be registered in the container \"{container}\". Details:\n{GetContainerDetails(container)}");
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

        public Expression CannotBuildExpression(IBuildContext buildContext, IDependency dependency, ILifetime lifetime)
        {
            if (buildContext == null) throw new ArgumentNullException(nameof(buildContext));
            if (lifetime == null) throw new ArgumentNullException(nameof(lifetime));
            throw new InvalidOperationException($"Cannot build expression for the key \"{buildContext.Key}\" from the container \"{buildContext.Container}\". Details:\n{GetContainerDetails(buildContext.Container)}");
        }

        public IMethod<ConstructorInfo> CannotResolveConstructor(IEnumerable<IMethod<ConstructorInfo>> constructors)
        {
            if (constructors == null) throw new ArgumentNullException(nameof(constructors));
            var type = constructors.Single().Info.DeclaringType;
            throw new InvalidOperationException($"Cannot find a constructor for the type \"{type}\".");
        }

        public Type CannotResolveType(Type registeredType, Type resolvingType)
        {
            if (registeredType == null) throw new ArgumentNullException(nameof(registeredType));
            if (resolvingType == null) throw new ArgumentNullException(nameof(resolvingType));
            throw new InvalidOperationException($"Cannot resolve instance type based on the registered type \"{registeredType}\" for resolving type \"{registeredType}\".");
        }

        private static string GetContainerDetails(IContainer container)
        {
            return string.Join(Environment.NewLine, container.SelectMany(i => i).Select(i => i.ToString()));
        }
    }
}
