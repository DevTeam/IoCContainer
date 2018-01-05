namespace IoC.Features
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Internal;

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
            private static readonly Key ResolvingKey = new Key(typeof(T));
            private static readonly bool IsGenericResolvingType = typeof(T).IsConstructedGenericType();

            public InstanceTask(ResolvingContext context)
                :base(CreateFunction(context))
            {
            }

            private static Func<T> CreateFunction(ResolvingContext context)
            {
                var resolvingKey = context.ResolvingKey.Tag == null ? ResolvingKey : new Key(typeof(T), context.ResolvingKey.Tag);
                var resolvingContext = new ResolvingContext(context.RegistrationContext)
                {
                    ResolvingKey = resolvingKey,
                    ResolvingContainer = context.ResolvingContainer,
                    Args = context.Args,
                    IsGenericResolvingType = IsGenericResolvingType
                };

                if (!resolvingContext.ResolvingContainer.TryGetResolver(resolvingKey, out var resolver))
                {
                    resolver = resolvingContext.ResolvingContainer.Get<IIssueResolver>().CannotGetResolver(resolvingContext.ResolvingContainer, resolvingKey);
                }

                return () => (T) resolver.Resolve(resolvingContext.ResolvingKey, resolvingContext.ResolvingContainer);
            }
        }
    }
}
