namespace IoC.Core.Emiters
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Dependencies;

    internal sealed class DependencyEmitter : IEmitter<IDependency>
    {
        public static readonly IEmitter<IDependency> Shared = new DependencyEmitter(ValueEmitter.Shared, ArgumentEmitter.Shared, FactoryMethodEmitter.Shared, StaticMethodEmitter.Shared, AutowiringEmitter.Shared);
        private static readonly Key ContextKey = Key.Create<Context>();
        private static readonly ITypeInfo ContextInfo = Type<Context>.Info;
        private static readonly ConstructorInfo ContextConstructor = ContextInfo.DeclaredConstructors.Single();
        [NotNull] private readonly IEmitter<Value> _valueEmitter;
        [NotNull] private readonly IEmitter<Argument> _argumentEmitter;
        [NotNull] private readonly IEmitter<FactoryMethod> _factoryMethodEmitter;
        [NotNull] private readonly IEmitter<StaticMethod> _staticMethodEmitter;
        [NotNull] private readonly IEmitter<Autowiring> _autowiringEmitter;

        public DependencyEmitter(
            [NotNull] IEmitter<Value> valueEmitter,
            [NotNull] IEmitter<Argument> argumentEmitter,
            [NotNull] IEmitter<FactoryMethod> factoryMethodEmitter,
            [NotNull] IEmitter<StaticMethod> staticMethodEmitter,
            [NotNull] IEmitter<Autowiring> autowiringEmitter)
        {
            _valueEmitter = valueEmitter ?? throw new ArgumentNullException(nameof(valueEmitter));
            _argumentEmitter = argumentEmitter ?? throw new ArgumentNullException(nameof(argumentEmitter));
            _factoryMethodEmitter = factoryMethodEmitter ?? throw new ArgumentNullException(nameof(factoryMethodEmitter));
            _staticMethodEmitter = staticMethodEmitter ?? throw new ArgumentNullException(nameof(staticMethodEmitter));
            _autowiringEmitter = autowiringEmitter ?? throw new ArgumentNullException(nameof(autowiringEmitter));
        }

        public EmitResult Emit(EmitContext ctx, IDependency dependency)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            if (dependency == null) throw new ArgumentNullException(nameof(dependency));
            EmitResult emitResult;
            switch (dependency)
            {
                case Value value:
                    emitResult = _valueEmitter.Emit(ctx, value);
                    break;

                case Argument argument:
                    emitResult = _argumentEmitter.Emit(ctx, argument);
                    break;

                case FactoryMethod function:
                    emitResult = _factoryMethodEmitter.Emit(ctx, function);
                    break;

                case StaticMethod staticMethod:
                    emitResult = _staticMethodEmitter.Emit(ctx, staticMethod);
                    break;

                case Autowiring autowiring:
                    emitResult = _autowiringEmitter.Emit(ctx, autowiring);
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

                    if (container.TryGetDependency(dep.Key, out var nestedDependency))
                    {
                        var reentrancy = ctx.Statistics.EmitDependency(dep.Key);
                        if (reentrancy >= 128)
                        {
                            ctx.Container.Get<IIssueResolver>().CyclicDependenceDetected(dep.Key, reentrancy);
                        }

                        ctx = new EmitContext(dep.Key, container, ctx.DependencyEmitter, ctx.Emitter, ctx.Statistics);
                        emitResult = Emit(ctx, nestedDependency);
                        break;
                    }

                    if (dep.Key.Equals(ContextKey))
                    {
                        ctx.Emitter
                            .Ldobj(ctx.Key)
                            .Ldarg(Arguments.Container)
                            .Ldarg(Arguments.Args)
                            .Newobj(ContextConstructor);

                        emitResult = new EmitResult(ContextInfo);
                        break;
                    }

                    throw new InvalidOperationException($"Cannot resolve the dependency \"{dep.Key}\".");

                default:
                    throw new NotSupportedException();
            }

            return emitResult;
        }
    }
}
