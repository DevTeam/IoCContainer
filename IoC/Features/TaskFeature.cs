namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Internal;

    public sealed  class TaskFeature : IConfiguration
    {
        public static readonly IConfiguration Shared = new TaskFeature();

        private TaskFeature()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container
                .Bind(typeof(Task<>))
                .AnyTag()
                .To(CreateTask);
        }

        private static object CreateTask(Context context)
        {
            Type[] genericTypeArguments;
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (context.IsConstructedGenericResolvingContractType)
            {
                genericTypeArguments = context.ResolvingKey.ContractType.GenericTypeArguments();
            }
            else
            {
                genericTypeArguments = context.ResolvingContainer.Get<IIssueResolver>().CannotGetGenericTypeArguments(context.ResolvingKey.ContractType);
            }

            var targetContractType = genericTypeArguments[0];
            var instanceType = typeof(InstanceTask<>).MakeGenericType(targetContractType);
            var resolvingKey = new Key(targetContractType, context.ResolvingKey.Tag);
            var newContext = new Context(context.RegistrationId, context.RegistrationKey, context.RegistrationContainer, resolvingKey, context.ResolvingContainer, context.Args, targetContractType.IsConstructedGenericType());
            return Activator.CreateInstance(instanceType, newContext);
        }

        private sealed class InstanceTask<T> : Task<T>
        {
            public InstanceTask(Context context)
                :base(CreateFunction(context))
            {
            }

            private static Func<T> CreateFunction(Context context)
            {
                var key = new Key(typeof(T), context.RegistrationKey.Tag);
                if (!context.ResolvingContainer.TryGetResolver(key, out var resolver))
                {
                    resolver = context.ResolvingContainer.Get<IIssueResolver>().CannotGetResolver(context.ResolvingContainer, key);
                }

                return () => (T) resolver.Resolve(context.ResolvingKey, context.ResolvingContainer);
            }
        }
    }
}
