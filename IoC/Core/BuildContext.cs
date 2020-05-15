namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using Issues;

    /// <summary>
    /// Represents build context.
    /// </summary>
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    [PublicAPI]
    internal sealed class BuildContext : IBuildContext
    {
        private static readonly ICollection<IBuilder> EmptyBuilders = new List<IBuilder>();
        [NotNull] private readonly IEnumerable<IBuilder> _builders;
        private readonly IDictionary<Type, Type> _typesMap = new Dictionary<Type, Type>();
        private readonly IList<ParameterExpression> _parameters = new List<ParameterExpression>();
        [NotNull] private readonly ICompiler _compiler;

        internal BuildContext(
            [CanBeNull] IBuildContext parent,
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IEnumerable<IBuilder> builders,
            [NotNull] IAutowiringStrategy defaultAutowiringStrategy,
            [NotNull] ICompiler compiler,
            [NotNull] ParameterExpression argsParameter,
            [NotNull] ParameterExpression containerParameter,
            int depth = 0)
        {
            Parent = parent;
            Key = key;
            Container = resolvingContainer ?? throw new ArgumentNullException(nameof(resolvingContainer));
            _builders = builders ?? throw new ArgumentNullException(nameof(builders));
            AutowiringStrategy = defaultAutowiringStrategy ?? throw new ArgumentNullException(nameof(defaultAutowiringStrategy));
            _compiler = compiler;
            ArgsParameter = argsParameter ?? throw new ArgumentNullException(nameof(argsParameter));
            ContainerParameter = containerParameter ?? throw new ArgumentNullException(nameof(containerParameter));
            Depth = depth;
        }

        public IBuildContext Parent { get; }

        public Key Key { get; }

        public IContainer Container { get; }

        public IAutowiringStrategy AutowiringStrategy { get; }
        
        public int Depth { get; }

        public ParameterExpression ArgsParameter { get; private set; }

        public ParameterExpression ContainerParameter { get; private set; }
        
        public IDisposable OverrideArgsParameter(ParameterExpression argsParameter)
        {
            var origin = ArgsParameter;
            ArgsParameter = argsParameter;
            return Disposable.Create(() => { ArgsParameter = origin; });
        }

        public IDisposable OverrideContainerParameter(ParameterExpression containerParameter)
        {
            var origin = ContainerParameter;
            ContainerParameter = containerParameter;
            return Disposable.Create(() => { ContainerParameter = origin; });
        }

        public IBuildContext CreateChild(Key key, IContainer container) => 
            CreateChildInternal(key, container ?? throw new ArgumentNullException(nameof(container)));

        public Expression ReplaceTypes(Expression baseExpression) =>
            TypeReplacerExpressionBuilder.Shared.Build(baseExpression, this, _typesMap);

        public void BindTypes(Type originalType, Type targetType) =>
            TypeMapper.Shared.Map(originalType, targetType, _typesMap);

        public bool TryReplaceType(Type originalType, out Type targetType) =>
            _typesMap.TryGetValue(originalType, out targetType);

        public void AddParameter(ParameterExpression parameterExpression)
            => _parameters.Add(parameterExpression ?? throw new ArgumentNullException(nameof(parameterExpression)));

        public Expression DeclareParameters(Expression baseExpression)
        {
            if (_parameters.Count > 0)
            {
                return Expression.Block(baseExpression.Type, _parameters, baseExpression);
            }

            return baseExpression;
        }

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
                    baseExpression = builder.Build(buildContext, baseExpression);
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

            return new BuildContext(this, key, container, forBuilders ? EmptyBuilders : _builders, AutowiringStrategy, _compiler, ArgsParameter, ContainerParameter, Depth + 1);
        }

        public Expression GetDependencyExpression(Expression defaultExpression = null)
        {
            var selectedContainer = Container;
            if (selectedContainer.Parent != null)
            {
                var parent = Parent;
                while (parent != null)
                {
                    if (
                        Key.Equals(parent.Key)
                        && Equals(parent.Container, selectedContainer))
                    {
                        selectedContainer = selectedContainer.Parent;
                        break;
                    }

                    parent = parent.Parent;
                }
            }

            if (!selectedContainer.TryGetDependency(Key, out var dependency, out var lifetime))
            {
                if (Container == selectedContainer || !Container.TryGetDependency(Key, out dependency, out lifetime))
                {
                    if (defaultExpression != null)
                    {
                        return defaultExpression;
                    }

                    try
                    {
                        var dependencyDescription = Container.Resolve<ICannotResolveDependency>().Resolve(this);
                        dependency = dependencyDescription.Dependency;
                        lifetime = dependencyDescription.Lifetime;
                    }
                    catch (Exception ex)
                    {
                        throw new BuildExpressionException(ex.Message, ex.InnerException);
                    }
                }
            }

            if (Depth >= 128)
            {
                Container.Resolve<IFoundCyclicDependency>().Resolve(this);
            }

            if (dependency.TryBuildExpression(this, lifetime, out var expression, out var error))
            {
                return expression;
            }

            return Container.Resolve<ICannotBuildExpression>().Resolve(this, dependency, lifetime, error);
        }

        public override string ToString()
        {
            var path = new List<IBuildContext>();
            IBuildContext context = this;
            while (context != null)
            {
                path.Add(context);
                context = context.Parent;
            }

            var text = new StringBuilder();
            for (var i = path.Count - 1; i >= 0; i--)
            {
                text.AppendLine($"{new string(' ', path[i].Depth << 1)} building {path[i].Key} in {path[i].Container}");
            }

            return text.ToString();
        }

        public bool TryCompile(LambdaExpression expression, out Delegate resolver, out Exception error) =>
            _compiler.TryCompile(this, expression, out resolver, out error);
    }
}
