namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [PublicAPI]
    internal class BuildContext : IBuildContext
    {
        private static readonly ICollection<IBuilder> EmptyBuilders = new List<IBuilder>();
        private readonly IEnumerable<IDisposable> _resources;
        [NotNull] private readonly IEnumerable<IBuilder> _builders;
        private readonly IDictionary<Type, Type> _typesMap = new Dictionary<Type, Type>();

        internal BuildContext(
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IEnumerable<IDisposable> resources,
            [NotNull] IEnumerable<IBuilder> builders,
            [NotNull] IAutowiringStrategy defaultAutowiringStrategy,
            int depth = 0)
        {
            Key = key;
            Container = resolvingContainer ?? throw new ArgumentNullException(nameof(resolvingContainer));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _builders = builders ?? throw new ArgumentNullException(nameof(builders));
            AutowiringStrategy = defaultAutowiringStrategy ?? throw new ArgumentNullException(nameof(defaultAutowiringStrategy));
            Depth = depth;
        }

        public Key Key { get; }

        public IContainer Container { get; }

        public IAutowiringStrategy AutowiringStrategy { get; }

        public int Depth { get; }

        public IBuildContext CreateChild(Key key, IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            return CreateChildInternal(key, container);
        }

        public Expression ReplaceTypes(Expression baseExpression) =>
            TypeReplacerExpressionBuilder.Shared.Build(baseExpression, this, _typesMap);

        public void BindTypes(Type type, Type targetType) =>
            TypeMapper.Shared.Map(type, targetType, _typesMap);

        public bool TryReplaceType(Type type, out Type targetType) =>
            _typesMap.TryGetValue(type, out targetType);

        public Expression InjectDependencies(Expression baseExpression, ParameterExpression instanceExpression = null) =>
            DependencyInjectionExpressionBuilder.Shared.Build(baseExpression, this, instanceExpression);

        public Expression AddLifetime(Expression baseExpression, ILifetime lifetime)
        {
            if (_builders.Any())
            {
                var buildContext = CreateChildInternal(Key, Container, forBuilders: true);
                foreach (var builder in _builders)
                {
                    baseExpression = baseExpression.Convert(Key.Type);
                    baseExpression = builder.Build(baseExpression, buildContext);
                }
            }

            baseExpression = baseExpression.Convert(Key.Type);
            return LifetimeExpressionBuilder.Shared.Build(baseExpression, this, lifetime);
        }

        private IBuildContext CreateChildInternal(Key key, IContainer container, bool forBuilders = false)
        {
            if (_typesMap.TryGetValue(key.Type, out var type))
            {
                key = new Key(type, key.Tag);
            }

            return new BuildContext(key, container, _resources, forBuilders ? EmptyBuilders : _builders, AutowiringStrategy, Depth + 1);
        }

        public Expression DependencyExpression
        {
            get
            {
                if (!Container.TryGetDependency(Key, out var dependency, out var lifetime))
                {
                    try
                    {
                        var dependencyInfo = Container.Resolve<IIssueResolver>().CannotResolveDependency(Container, Key);
                        dependency = dependencyInfo.Item1;
                        lifetime = dependencyInfo.Item2;
                    }
                    catch (Exception ex)
                    {
                        throw new BuildExpressionException(ex.Message, ex.InnerException);
                    }
                }

                if (Depth >= 128)
                {
                    Container.Resolve<IIssueResolver>().CyclicDependenceDetected(Key, Depth);
                }

                if (dependency.TryBuildExpression(this, lifetime, out var expression, out var error))
                {
                    return expression;
                }

                try
                {
                    return Container.Resolve<IIssueResolver>().CannotBuildExpression(this, dependency, lifetime, error);
                }
                catch (Exception)
                {
                    throw error;
                }
            }
        }
    }
}
