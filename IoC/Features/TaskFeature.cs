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
                .Bind<TaskScheduler>()
                .AnyTag()
                .To(ctx => TaskScheduler.Current);

            yield return container
                .Bind<Task<TT>>()
                .AnyTag()
                .To(ctx => StartTask(new Task<TT>(ctx.Container.Inject<Func<TT>>()), ctx.Container.Inject<TaskScheduler>()));
        }

        private Task<T> StartTask<T>(Task<T> task, TaskScheduler taskScheduler)
        {
            task.Start(taskScheduler);
            return task;
        }
    }
}
