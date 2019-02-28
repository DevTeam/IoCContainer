namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;

    /// <summary>
    /// Allows to resolve Tasks.
    /// </summary>
    [PublicAPI]
    public sealed  class TaskFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new TaskFeature();

        private TaskFeature() { }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => TaskScheduler.Current);
            yield return container.Register(ctx => StartTask(new Task<TT>(ctx.Container.Inject<Func<TT>>(ctx.Key.Tag)), ctx.Container.Inject<TaskScheduler>()), null, Feature.AnyTag);
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0
            yield return container.Register(ctx => new ValueTask<TT>(StartTask(new Task<TT>(ctx.Container.Inject<Func<TT>>(ctx.Key.Tag)), ctx.Container.Inject<TaskScheduler>())), null, Feature.AnyTag);
#endif
        }

        private static Task<T> StartTask<T>(Task<T> task, TaskScheduler taskScheduler)
        {
            task.Start(taskScheduler);
            return task;
        }
    }
}
