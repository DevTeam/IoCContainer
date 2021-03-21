namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
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
        ICannotResolveDependency
    {
        [NotNull] private readonly Func<Key, Key> _keyResolver;
        [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;
        [NotNull] private readonly IList<IDisposable> _tokens = new List<IDisposable>();
        [NotNull] private readonly ISet<Key> _registeredKeys = new HashSet<Key>();
        private readonly Options _options;

        /// <summary>
        /// Creates an instance of feature.
        /// </summary>
        public ResolveUnboundFeature()
            : this(Options.ResolveArgs | Options.ResolveDefaults, DefaultKeyResolver)
        {
        }

        /// <summary>
        /// Creates an instance of feature.
        /// </summary>
        /// <param name="options">Options for resolve.</param>
        /// <param name="keyResolver">Use this resolver to replace a resolving key.</param>
        /// <param name="autowiringStrategy">The autowiring strategy.</param>
        public ResolveUnboundFeature(Options options, [NotNull] Func<Key, Key> keyResolver, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
        {
            _options = options;
            _keyResolver = keyResolver;
            _autowiringStrategy = autowiringStrategy;
        }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return container.Bind<ICannotResolveDependency>().Bind<ICannotGetResolver>().To(ctx => this);
        }

        Resolver<T> ICannotGetResolver.Resolve<T>(IContainer container, Key key, Exception error)
        {
            var resolvedKey = KeyResolver(key);
            if (_registeredKeys.Add(resolvedKey))
            {
                if (IsValidType(resolvedKey.Type) && container is IMutableContainer mutableContainer && mutableContainer.TryRegisterDependency(new[] {resolvedKey}, new UnboundDependency(_options, _keyResolver, _autowiringStrategy), null, out var token))
                {
                    _tokens.Add(token);
                    return resolvedKey.Tag != null
                        ? container.GetResolver<T>(resolvedKey.Type, resolvedKey.Tag.AsTag())
                        : container.GetResolver<T>(resolvedKey.Type);
                }
            }

            return (container.Parent ?? throw new InvalidOperationException("Parent container should not be null.")).Resolve<ICannotGetResolver>().Resolve<T>(container, resolvedKey, error);
        }

        DependencyDescription ICannotResolveDependency.Resolve(IBuildContext buildContext)
        {
            var resolvedKey = KeyResolver(buildContext.Key);
            if (IsValidType(resolvedKey.Type))
            {
                return new DependencyDescription(new UnboundDependency(_options, _keyResolver, _autowiringStrategy), null);
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

        private static bool IsValidType(Type type)
        {
            var typeDescriptor = type.Descriptor();
            return !typeDescriptor.IsAbstract() && !typeDescriptor.IsInterface();
        }

        private Key KeyResolver(Key key)
        {
            if (IsValidType(key.Type))
            {
                return key;
            }

            var newKey = _keyResolver(key);
            if (!IsValidType(newKey.Type))
            {
                throw new InvalidOperationException($"Cannot resolve {newKey}.");
            }

            if (!key.Type.Descriptor().IsAssignableFrom(newKey.Type.Descriptor()))
            {
                throw new InvalidOperationException($"Type {newKey.Type} cannot be cast to {key.Type}.");
            }

            return newKey;
        }

        private class UnboundDependency : IDependency
        {
            private readonly Options _options;
            [NotNull] private readonly Func<Key, Key> _keyResolver;
            [CanBeNull] private readonly IAutowiringStrategy _autowiringStrategy;

            public UnboundDependency(Options options, [NotNull] Func<Key, Key> keyResolver, [CanBeNull] IAutowiringStrategy autowiringStrategy = null)
            {
                _options = options;
                _keyResolver = keyResolver ?? throw new ArgumentNullException(nameof(keyResolver));
                _autowiringStrategy = autowiringStrategy;
            }

            public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
            {
                var resolvedKey = _keyResolver(buildContext.Key);
                buildContext.CreateChild(resolvedKey, buildContext.Container);
                var type = resolvedKey.Type;
                var autowired = new AutowiringDependency(type, _autowiringStrategy).TryBuildExpression(buildContext, lifetime, out expression, out error);
                if (_options.HasFlag(Options.ResolveArgs))
                {
                    var fromArgsResolverType = typeof(FromArgsResolver<>).Descriptor().MakeGenericType(type);
                    var ctor = fromArgsResolverType.Descriptor().GetDeclaredConstructors().Single(i => i.GetParameters().Length == 1);
                    var resolverVar = Expression.Variable(fromArgsResolverType);

                    var fallbackExpression = expression;
                    if (!autowired)
                    {
                        if (_options.HasFlag(Options.ResolveDefaults) && type.Descriptor().IsValueType())
                        {
                            fallbackExpression = Expression.Default(type);
                        }
                        else
                        {
                            fallbackExpression = Expression.Block(
                                Expression.Throw(
                                    Expression.Constant(
                                        new InvalidOperationException($"Cannot resolve a dependency of type {type.GetShortName()} using a context arguments. Try passing an argument of corresponding type assignable to {type.GetShortName()} via one of a Func<> arguments."))),
                                Expression.Default(type));
                        }
                    }

                    expression = Expression.Block(
                        new[] { resolverVar },
                        Expression.Assign(resolverVar, Expression.New(ctor, buildContext.ArgsParameter)),
                        Expression.Condition(
                            Expression.Field(resolverVar, nameof(FromArgsResolver<object>.HasValue)),
                            Expression.Field(resolverVar, nameof(FromArgsResolver<object>.Value)),
                            fallbackExpression)
                        );

                    return true;
                }

                if (_options.HasFlag(Options.ResolveDefaults) && type.Descriptor().IsValueType())
                {
                    expression = Expression.Default(type);
                    error = default(Exception);
                    return true;
                }

                return true;
            }
        }

        private static Key DefaultKeyResolver(Key key) => key;

        private struct FromArgsResolver<T>
        {
            private static readonly TypeDescriptor TypeDescriptor = typeof(T).Descriptor();
            public readonly T Value;
            public readonly bool HasValue;

            public FromArgsResolver([NotNull] object[] args)
            {
                for (var index = 0; index < args.Length; index++)
                {
                    var element = args[index];
                    if (element == null)
                    {
                        continue;
                    }

                    if (TypeDescriptor.IsAssignableFrom(element.GetType().Descriptor()))
                    {
                        Value = (T)element;
                        HasValue = true;
                        return;
                    }
                }

                Value = default(T);
                HasValue = false;
            }
        }
    }

    [Flags]
    public enum Options
    {
        ResolveArgs = 1,

        ResolveDefaults = 2
    }
}
