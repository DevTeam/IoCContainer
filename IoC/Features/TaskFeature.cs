namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [PublicAPI]
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
                .To(typeof(InstanceTask<>));
        }

        private sealed class InstanceTask<T> : Task<T>
        {
            public InstanceTask(Context context)
                :base(CreateFunction(context))
            {
            }

            private static Func<T> CreateFunction(Context context)
            {
                var resolvingKey = Key.Create<T>(context.Key.Tag);
                if (!context.Container.TryGetResolver<T>(resolvingKey, out var resolver, context.Container))
                {
                    resolver = context.Container.Get<IIssueResolver>().CannotGetResolver<T>(context.Container, resolvingKey);
                }

                return () => resolver(context.Container);
            }
        }
    }
}
