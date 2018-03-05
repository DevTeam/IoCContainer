namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Allows to resolve Tasks.
    /// </summary>
    [PublicAPI]
    public sealed  class TaskFeature : IConfiguration
    {
        /// The default instance.
        public static readonly IConfiguration Default = new TaskFeature();

        private TaskFeature()
        {
        }

        /// <inheritdoc />
        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => TaskScheduler.Current);
            yield return container.Register(ctx => StartTask(new Task<TT>(ctx.Container.Inject<Func<TT>>()), ctx.Container.Inject<TaskScheduler>()), null, Feature.AnyTag);
#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NET40 && !NET45 && !NET46 && !NET47
            yield return container.Register(ctx => new ValueTask<TT>(ctx.Container.Inject<TT>()), null, Feature.AnyTag);
#endif
        }

        internal static Task<T> StartTask<T>(Task<T> task, TaskScheduler taskScheduler)
        {
            task.Start(taskScheduler);
            return task;
        }
    }
}
