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
                if (context.Container.TryGetResolver<TaskScheduler>(typeof(TaskScheduler), out var taskSchedulerResolver))
                {
                    Start(taskSchedulerResolver(context.Container));
                }
            }

            private static Func<T> CreateFunction(Context context)
            {
                if (!context.Container.TryGetResolver<T>(context.Container, typeof(T), context.Key.Tag, out var resolver))
                {
                    resolver = context.Container.Get<IIssueResolver>().CannotGetResolver<T>(context.Container, Key.Create<T>(context.Key.Tag));
                }

                return () => resolver(context.Container);
            }
        }
    }
}
