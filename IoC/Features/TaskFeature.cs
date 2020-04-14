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

        /// <summary>
        /// TaskFeature default tag
        /// </summary>
        public static readonly Tag Tag = new Tag(Guid.NewGuid());

        private TaskFeature() { }

        /// <inheritdoc />
        public IEnumerable<IToken> Apply(IMutableContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container.Register(ctx => TaskScheduler.Current);
            yield return container.Register(ctx => CreateTask(ctx.Container.Inject<Func<TT>>(ctx.Key.Tag), ctx.Container.Inject<TaskScheduler>()), null, Set.AnyTag);
#if !NET40 && !NET403 && !NET45 && !NET45 && !NET451 && !NET452 && !NET46 && !NET461 && !NET462 && !NET47 && !NET48 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2&& !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETSTANDARD2_0 && !WINDOWS_UWP
            yield return container.Register(ctx => new ValueTask<TT>(ctx.Container.Inject<TT>(ctx.Key.Tag)), null, Set.AnyTag);
#endif
        }

        private static Task<T> CreateTask<T>(Func<T> factory, TaskScheduler taskScheduler)
        {
            var task = new Task<T>(factory);
            task.Start(taskScheduler);
            return task;
        }
    }
}
