namespace IoC.Core.Emitters
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Dependencies;
    using IoC.Lifetimes;

    internal sealed class DependencyEmitter : IDependencyEmitter<IDependency>
    {
        public static readonly IDependencyEmitter<IDependency> Shared = new DependencyEmitter(ValueEmitter.Shared, ArgumentEmitter.Shared, FactoryMethodEmitter.Shared, StaticMethodEmitter.Shared, AutowiringEmitter.Shared);
        private static readonly Key ContextKey = Key.Create<Context>();
        private static readonly ITypeInfo ContextInfo = Type<Context>.Info;
        private static readonly ConstructorInfo ContextConstructor = ContextInfo.DeclaredConstructors.Single();
        [NotNull] private readonly IDependencyEmitter<Value> _valueEmitter;
        [NotNull] private readonly IDependencyEmitter<Argument> _argumentEmitter;
        [NotNull] private readonly IDependencyEmitter<FactoryMethod> _factoryMethodEmitter;
        [NotNull] private readonly IDependencyEmitter<StaticMethod> _staticMethodEmitter;
        [NotNull] private readonly IDependencyEmitter<Autowiring> _autowiringEmitter;

        public DependencyEmitter(
            [NotNull] IDependencyEmitter<Value> valueEmitter,
            [NotNull] IDependencyEmitter<Argument> argumentEmitter,
            [NotNull] IDependencyEmitter<FactoryMethod> factoryMethodEmitter,
            [NotNull] IDependencyEmitter<StaticMethod> staticMethodEmitter,
            [NotNull] IDependencyEmitter<Autowiring> autowiringEmitter)
        {
            _valueEmitter = valueEmitter ?? throw new ArgumentNullException(nameof(valueEmitter));
            _argumentEmitter = argumentEmitter ?? throw new ArgumentNullException(nameof(argumentEmitter));
            _factoryMethodEmitter = factoryMethodEmitter ?? throw new ArgumentNullException(nameof(factoryMethodEmitter));
            _staticMethodEmitter = staticMethodEmitter ?? throw new ArgumentNullException(nameof(staticMethodEmitter));
            _autowiringEmitter = autowiringEmitter ?? throw new ArgumentNullException(nameof(autowiringEmitter));
        }

        public void Emit(EmitContext ctx, IDependency dependency)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            switch (dependency)
            {
                case Value value:
                    _valueEmitter.Emit(ctx, value);
                    break;

                case Argument argument:
                    _argumentEmitter.Emit(ctx, argument);
                    break;

                case FactoryMethod function:
                    _factoryMethodEmitter.Emit(ctx, function);
                    break;

                case StaticMethod staticMethod:
                    _staticMethodEmitter.Emit(ctx, staticMethod);
                    break;

                case Autowiring autowiring:
                    _autowiringEmitter.Emit(ctx, autowiring);
                    break;

                case Dependency dep:
                    IContainer container;
                    switch (dep.Scope)
                    {
                        case Scope.Current:
                            container = ctx.Container;
                            break;

                        case Scope.Parent:
                            container = ctx.Container.Parent;
                            break;

                        case Scope.Child:
                            container = ctx.Container.CreateChild();
                            break;

                        default:
                            throw new NotSupportedException($"The scope {dep.Scope} is not supported.");
                    }

                    if (container == null)
                    {
                        container = ctx.Container;
                    }

                    if (container.TryGetDependency(dep.Key, out var nestedDependency, out var lifetime))
                    {
                        var reentrancy = ctx.Statistics.EmitDependency(dep.Key);
                        if (reentrancy >= 128)
                        {
                            ctx.Container.Get<IIssueResolver>().CyclicDependenceDetected(dep.Key, reentrancy);
                        }

                        ctx = new EmitContext(
                            dep.Key,
                            container,
                            ctx.DependencyEmitter,
                            ctx.LifetimeEmitter,
                            ctx.Emitter,
                            ctx.Statistics);

                        Emit(ctx, nestedDependency);
                        if (!(lifetime is null))
                        {
                            ctx.LifetimeEmitter.Emit(ctx, lifetime);
                        }

                        break;
                    }

                    if (dep.Key.HashCode == ContextKey.HashCode && dep.Key.Equals(ContextKey))
                    {
                        ctx.Emitter
                            .LoadConst(ctx.Key)
                            .LoadArg(Arguments.Container)
                            .LoadArg(Arguments.Args)
                            .Newobj(ContextConstructor);
                        break;
                    }

                    throw new InvalidOperationException($"Cannot resolve the dependency \"{dep.Key}\".");

                default:
                    // ReSharper disable once SuspiciousTypeConversion.Global
                    if (dependency is IEmitable emitable)
                    {
                        ctx.Emitter.Push(emitable.Emit(null));
                        break;
                    }

                    throw new NotSupportedException($"Unknow dependecy \"{dependency}\" or the interface \"{nameof(IEmitable)}\" is not supported.");
            }
        }
    }
}
