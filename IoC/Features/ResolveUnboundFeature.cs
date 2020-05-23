namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Core;
    using Dependencies;
    using Issues;

    /// <summary>
    /// Allows to resolve unbound dependencies.
    /// </summary>
    public class ResolveUnboundFeature:
        IConfiguration,
        IDisposable,
        IEnumerable<Key>,
        ICannotGetResolver,
        ICannotResolveDependency,
        IDependency
    {
        private readonly bool _supportDefaults;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        private readonly IList<IDisposable> _tokens = new List<IDisposable>();
        private readonly IList<Key> _registeredKeys = new List<Key>();

        /// <summary>
        /// Creates an instance of feature.
        /// </summary>
        public ResolveUnboundFeature()
            : this(true)
        {
        }

        /// <summary>
        /// Creates an instance of feature.
        /// </summary>
        /// <param name="supportDefaults"><c>True</c> to resolve default(T) for unresolved value types.</param>
        /// <param name="autowiringStrategy">The autowiring strategy.</param>
        public ResolveUnboundFeature(bool supportDefaults, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            _supportDefaults = supportDefaults;
            _autowiringStrategy = autowiringStrategy;
        }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return container.Bind<ICannotResolveDependency>().Bind<ICannotGetResolver>().To(ctx => this);
        }

        Resolver<T> ICannotGetResolver.Resolve<T>(IContainer container, Key key, Exception error)
        {
            if (IsValidType(key.Type) && container is IMutableContainer mutableContainer && mutableContainer.TryRegisterDependency(new[] {key}, this, null, out var token))
            {
                _registeredKeys.Add(key);
                _tokens.Add(token);
                return container.GetResolver<T>(key.Type, key.Tag.AsTag());
            }

            return (container.Parent ?? throw new InvalidOperationException("Parent container should not be null.")).Resolve<ICannotGetResolver>().Resolve<T>(container, key, error);
        }

        DependencyDescription ICannotResolveDependency.Resolve(IBuildContext buildContext)
        {
            if (IsValidType(buildContext.Key.Type))
            {
                return new DependencyDescription(this, null);
            }

            return (buildContext.Container.Parent ?? throw new InvalidOperationException("Parent container should not be null.")).Resolve<ICannotResolveDependency>().Resolve(buildContext);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            foreach (var token in _tokens)
            {
                token.Dispose();
            }
        }

        /// <inheritdoc />
        public IEnumerator<Key> GetEnumerator() => _registeredKeys.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        bool IDependency.TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            var type = buildContext.Key.Type;
            if (_supportDefaults)
            {
                if (type.Descriptor().IsValueType())
                {
                    expression = Expression.Default(type);
                    error = default(Exception);
                    return true;
                }
            }

            return
                new AutowiringDependency(type, _autowiringStrategy)
                .TryBuildExpression(buildContext, lifetime, out expression, out error);
        }

        private static bool IsValidType(Type type)
        {
            var typeDescriptor = type.Descriptor();
            return !typeDescriptor.IsAbstract() && !typeDescriptor.IsInterface();
        }
    }
}
