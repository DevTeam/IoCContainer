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
        [NotNull] private readonly IEnumerable<IBuilder> _builders;
        [NotNull] private readonly IObserver<ContainerEvent> _eventObserver;
        private readonly IList<ParameterExpression> _parameters = new List<ParameterExpression>();
        [NotNull] private readonly IDictionary<Type, Type> _typesMap;

        internal BuildContext(
            [CanBeNull] BuildContext parent,
            Key key,
            [NotNull] IContainer resolvingContainer,
            [NotNull] IEnumerable<ICompiler> compilers,
            [NotNull] IEnumerable<IBuilder> builders,
            [NotNull] IAutowiringStrategy defaultAutowiringStrategy,
            [NotNull] ParameterExpression argsParameter,
            [NotNull] ParameterExpression containerParameter,
            [NotNull] IObserver<ContainerEvent> eventObserver,
            int depth = 0)
        {
            Parent = parent;
            Key = key;
            Container = resolvingContainer ?? throw new ArgumentNullException(nameof(resolvingContainer));
            Compilers = compilers ?? throw new ArgumentNullException(nameof(compilers));
            _builders = builders ?? throw new ArgumentNullException(nameof(builders));
            _eventObserver = eventObserver ?? throw new ArgumentNullException(nameof(eventObserver));
            AutowiringStrategy = defaultAutowiringStrategy ?? throw new ArgumentNullException(nameof(defaultAutowiringStrategy));
            ArgsParameter = argsParameter ?? throw new ArgumentNullException(nameof(argsParameter));
            ContainerParameter = containerParameter ?? throw new ArgumentNullException(nameof(containerParameter));
            Depth = depth;
            _typesMap = parent == null ? new Dictionary<Type, Type>() : new Dictionary<Type, Type>(parent._typesMap);
        }

        public IBuildContext Parent { get; private set; }

        public Key Key { get; }

        public IContainer Container { get; }

        public IEnumerable<ICompiler> Compilers { get; private set; }

        public IAutowiringStrategy AutowiringStrategy { get; }
        
        public int Depth { get; }

        public ParameterExpression ArgsParameter { get; private set; }

        public ParameterExpression ContainerParameter { get; private set; }

        public IBuildContext CreateChild(Key key, IContainer container) => 
            CreateInternal(key, container ?? throw new ArgumentNullException(nameof(container)));

        public Expression CreateExpression(Expression defaultExpression = null)
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

        public void AddParameter(ParameterExpression parameterExpression)
            => _parameters.Add(parameterExpression ?? throw new ArgumentNullException(nameof(parameterExpression)));

        public void MapType(Type fromType, Type toType) => _typesMap[fromType] = toType;

        public Expression FinalizeExpression(Expression baseExpression, ILifetime lifetime)
        {
            if (_parameters.Count > 0)
            {
                baseExpression = Expression.Block(baseExpression.Type, _parameters, baseExpression);
            }

            foreach (var builder in _builders)
            {
                baseExpression = baseExpression.Convert(Key.Type);
                baseExpression = builder.Build(this, baseExpression);
            }

            baseExpression = baseExpression.Convert(Key.Type);
            return LifetimeExpressionBuilder.Shared.Build(baseExpression, this, lifetime);
        }

        public bool TryCompile(LambdaExpression lambdaExpression, out Delegate lambdaCompiled, out Exception error)
        {
            error = default(Exception);
            try
            {
                foreach (var compiler in Compilers)
                {
                    if (compiler.TryCompile(this, lambdaExpression, out lambdaCompiled, out error))
                    {
                        _eventObserver.OnNext(ContainerEvent.Compilation(Container, new[] { Key }, lambdaExpression));
                        return true;
                    }

                    _eventObserver.OnNext(ContainerEvent.CompilationFailed(Container, new[] { Key }, lambdaExpression, error));
                }
            }
            catch (Exception ex)
            {
                error = ex;
                _eventObserver.OnNext(ContainerEvent.CompilationFailed(Container, new[] { Key }, lambdaExpression, ex));
            }

            lambdaCompiled = default(Delegate);
            return false;
        }

        private IBuildContext CreateInternal(Key key, IContainer container)
        {
            if (_typesMap.TryGetValue(key.Type, out var type))
            {
                key = new Key(type, key.Tag);
            }

            return new BuildContext(
                this,
                key,
                container,
                Compilers,
                _builders,
                AutowiringStrategy,
                ArgsParameter,
                ContainerParameter,
                _eventObserver,
                Depth + 1);
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
    }
}
