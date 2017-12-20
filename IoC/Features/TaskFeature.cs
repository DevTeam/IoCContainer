namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    internal class TaskFeature: IConfiguration
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
            if (!ctx.ContractType.IsConstructedGenericType)
            {
                throw new InvalidOperationException();
            }

            var instanceType = typeof(InstanceTask<>).MakeGenericType(ctx.ContractType.GenericTypeArguments);
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
                    throw new InvalidOperationException();
                }

                return () => (T) resolver.Resolve(ctx.ResolvingContainer, typeof(T));
            }
        }
    }
}
