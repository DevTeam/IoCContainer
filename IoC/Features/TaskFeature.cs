namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Core;
    using static Core.FluentRegister;

    /// <summary>
    /// Allows to resolve Tasks.
    /// </summary>
    [PublicAPI]
    public sealed  class TaskFeature : IConfiguration
    {
        [CanBeNull] private readonly TaskScheduler _taskScheduler;

        /// The default instance.
        public static readonly IConfiguration Set = new TaskFeature(TaskScheduler.Current);

        public TaskFeature([CanBeNull] TaskScheduler taskScheduler = null) => _taskScheduler = taskScheduler;

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => CreateTask(ctx.Container.Inject<Func<TT>>(ctx.Key.Tag), ctx.Container.TryInject<TaskScheduler>() ?? _taskScheduler), null, AnyTag);
#if !NET40 && !NET403 && !NET45 && !NET45 && !NET451 && !NET452 && !NET46 && !NET461 && !NET462 && !NET47 && !NET48 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2&& !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !WINDOWS_UWP
            yield return container.Register(ctx => new ValueTask<TT>(ctx.Container.Inject<TT>(ctx.Key.Tag)), null, AnyTag);
#endif
        }

        private static Task<T> CreateTask<T>([NotNull] Func<T> factory, [CanBeNull] TaskScheduler taskScheduler)
        {
            var task = new Task<T>(factory);
            if (taskScheduler != null)
            {
                task.Start(taskScheduler);
            }

            return task;
        }
    }
}
