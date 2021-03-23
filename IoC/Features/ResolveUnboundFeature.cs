namespace IoC.Features
{
    using System;
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
        IDependency,
        ICannotGetResolver,
        ICannotResolveDependency
    {

        /// The default instance.
        public static readonly IConfiguration Set = new ResolveUnboundFeature();

        [NotNull] private readonly Func<Key, Key> _keyResolver;
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
        /// <param name="keyResolver">Use this resolver to replace a resolving key.</param>
        public ResolveUnboundFeature([NotNull] Func<Key, Key> keyResolver)
            : this(Options.ResolveArgs | Options.ResolveDefaults, keyResolver)

        {
            _keyResolver = keyResolver;
        }

        /// <summary>
        /// Creates an instance of feature.
        /// </summary>
        /// <param name="options">Options for unbound resolve.</param>
        /// <param name="keyResolver">Use this resolver to replace a resolving key.</param>
        public ResolveUnboundFeature(Options options, [NotNull] Func<Key, Key> keyResolver)
        {
            _options = options;
            _keyResolver = keyResolver;
        }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            yield return container.Bind<ICannotResolveDependency>().Bind<ICannotGetResolver>().To(ctx => this);
        }

        Resolver<T> ICannotGetResolver.Resolve<T>(IContainer container, Key key, Exception error)
        {
            if (KeyResolver(key, out var resolvedKey) && _registeredKeys.Add(resolvedKey))
            {
                if (IsValidType(resolvedKey.Type) && container is IMutableContainer mutableContainer && mutableContainer.TryRegisterDependency(new[] {resolvedKey}, this, null, out var token))
                {
                    mutableContainer.RegisterResource(token);
                    return resolvedKey.Tag != null
                        ? container.GetResolver<T>(resolvedKey.Type, resolvedKey.Tag.AsTag())
                        : container.GetResolver<T>(resolvedKey.Type);
                }
            }

            return CannotGetResolver.Shared.Resolve<T>(container, key, error);
        }

        DependencyDescription ICannotResolveDependency.Resolve(IBuildContext buildContext)
        {
            if (KeyResolver(buildContext.Key, out var resolvedKey) && IsValidType(resolvedKey.Type))
            {
                return new DependencyDescription(this, null);
            }

            return CannotResolveDependency.Shared.Resolve(buildContext);
        }

        public bool TryBuildExpression(IBuildContext buildContext, ILifetime lifetime, out Expression expression, out Exception error)
        {
            var resolvedKey = _keyResolver(buildContext.Key);
            buildContext.CreateChild(resolvedKey, buildContext.Container);
            var type = resolvedKey.Type;
            var autowired = new AutowiringDependency(type, buildContext.AutowiringStrategy).TryBuildExpression(buildContext, lifetime, out expression, out error);
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
                                    new InvalidOperationException($"Cannot find a dependency for {buildContext.Key} in {buildContext.Container}. Try passing an argument of corresponding type assignable to {type.GetShortName()} via one of a Func<> arguments..\n{buildContext}"))),
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

        private static bool IsValidType(Type type)
        {
            var typeDescriptor = type.Descriptor();
            return !typeDescriptor.IsAbstract() && !typeDescriptor.IsInterface();
        }

        private bool KeyResolver(Key key, out Key newKey)
        {
            if (IsValidType(key.Type))
            {
                newKey = key;
                return true;
            }

            newKey = _keyResolver(key);
            if (!IsValidType(newKey.Type))
            {
                return false;
            }

            if (!key.Type.Descriptor().IsAssignableFrom(newKey.Type.Descriptor()))
            {
                throw new InvalidOperationException($"Type {newKey.Type} cannot be cast to {key.Type}.");
            }

            return true;
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
