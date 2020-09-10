namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Core;

    /// <summary>
    /// Represents extensions to trace the container.
    /// </summary>
    [PublicAPI]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public static class FluentTrace
    {
        /// <summary>
        /// Gets a container trace source.
        /// </summary>
        /// <param name="container">The target container to trace.</param>
        /// <returns>The race source.</returns>
        public static IObservable<TraceEvent> ToTraceSource([NotNull] this IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            return Observable.Create<TraceEvent>(observer =>
            {
                IDictionary<IContainer, IDisposable> subscriptions = new Dictionary<IContainer, IDisposable>();
                Subscribe(container, subscriptions, observer, container.Resolve<IConverter<ContainerEvent, IContainer, string>>());
                return Disposable.Create(subscriptions.Values);
            });
        }

        /// <summary>
        /// Traces container actions through a handler.
        /// </summary>
        /// <param name="container">The target container to trace.</param>
        /// <param name="onTraceEvent">The trace handler.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IMutableContainer container, [NotNull] Action<TraceEvent> onTraceEvent)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            if (onTraceEvent == null) throw new ArgumentNullException(nameof(onTraceEvent));

            return new Token(
                container,
                container
                    .ToTraceSource()
                    .Subscribe(
                        onTraceEvent,
                        error => { },
                        () => { }));
        }

        /// <summary>
        /// Traces container actions through a handler.
        /// </summary>
        /// <param name="token">The token of target container to trace.</param>
        /// <param name="onTraceEvent">The trace handler.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IToken token, [NotNull] Action<TraceEvent> onTraceEvent) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Trace(onTraceEvent ?? throw new ArgumentNullException(nameof(onTraceEvent)));

#if !NETSTANDARD1_0 && !NETSTANDARD1_1 && !NETSTANDARD1_2 && !NETSTANDARD1_3 && !NETSTANDARD1_4 && !NETSTANDARD1_5 && !NETSTANDARD1_6 && !NETCOREAPP1_0&& !NETCOREAPP1_1 && !WINDOWS_UWP
        /// <summary>
        /// Traces container actions through a <c>System.Diagnostics.Trace</c>.
        /// </summary>
        /// <param name="container">The target container to trace.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IMutableContainer container) =>
            (container ?? throw new ArgumentNullException(nameof(container))).Trace(e => System.Diagnostics.Trace.WriteLine(e.Message));

        /// <summary>
        /// Traces container actions through a <c>System.Diagnostics.Trace</c>.
        /// </summary>
        /// <param name="token">The token of target container to trace.</param>
        /// <returns>The trace token.</returns>
        public static IToken Trace([NotNull] this IToken token) =>
            (token ?? throw new ArgumentNullException(nameof(token))).Container.Trace();
#endif

        private static void Subscribe(
            IContainer container,
            IDictionary<IContainer, IDisposable> subscriptions,
            IObserver<TraceEvent> observer,
            IConverter<ContainerEvent, IContainer, string> converter)
        {
            lock (subscriptions)
            {
                if (subscriptions.ContainsKey(container))
                {
                    return;
                }

                var subscription = container.Subscribe(
                    value =>
                    {
                        switch (value.EventType)
                        {
                            case EventType.CreateContainer:
                                if (value.Container.Parent != container) return;
                                Subscribe(value.Container, subscriptions, observer, converter);
                                break;

                            case EventType.DisposeContainer:
                                if (value.Container.Parent != container) return;
                                lock (subscriptions)
                                {
                                    if (subscriptions.TryGetValue(value.Container, out var subscriptionToDispose))
                                    {
                                        subscriptions.Remove(value.Container);
                                        subscriptionToDispose.Dispose();
                                    }
                                }

                                break;

                            default:
                                if (value.Container != container) return;
                                break;

                        }

                        if (!converter.TryConvert(value.Container, value, out var message))
                        {
                            message = value.ToString();
                        }

                        observer.OnNext(new TraceEvent(value, message ?? string.Empty));
                    },
                    observer.OnError,
                    observer.OnCompleted);

                subscriptions.Add(container, subscription);
            }
        }
    }
}
