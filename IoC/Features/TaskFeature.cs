namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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
                .Map(typeof(Task<>))
                .To(CreateTask);
        }

        private static object CreateTask(Context ctx)
        {
            Type[] genericTypeArguments;
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (ctx.ContractType.IsConstructedGenericType)
            {
                genericTypeArguments = ctx.ContractType.GenericTypeArguments;
            }
            else
            {
                genericTypeArguments = ctx.ResolvingContainer.Get<IIssueResolver>().CannotGetGenericTypeArguments(ctx.ContractType);
            }

            var instanceType = typeof(InstanceTask<>).MakeGenericType(genericTypeArguments);
            return Activator.CreateInstance(instanceType, ctx);
        }

        private sealed class InstanceTask<T> : Task<T>
        {
            public InstanceTask(Context ctx)
                :base(CreateFunction(ctx))
            {
            }

            private static Func<T> CreateFunction(Context ctx)
            {
                var key = new Key(new Contract(typeof(T)), ctx.Key.Tag);
                if (!ctx.ResolvingContainer.TryGetResolver(key, out var resolver))
                {
                    resolver = ctx.ResolvingContainer.Get<IIssueResolver>().CannotGetResolver(ctx.ResolvingContainer, key);
                }

                return () => (T) resolver.Resolve(ctx.ResolvingContainer, typeof(T));
            }
        }
    }
}
