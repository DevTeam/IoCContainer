namespace IoC.Features
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Internal;

    public sealed class EnumerableFeature : IConfiguration
    {
        public static readonly IConfiguration Shared = new EnumerableFeature();

        private EnumerableFeature()
        {
        }

        public IEnumerable<IDisposable> Apply(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            yield return container
                .Bind(typeof(IEnumerable<>))
                .To(CreateEnumerable);
        }

        private static object CreateEnumerable(Context context)
        {
            Type[] genericTypeArguments;
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (context.IsConstructedGenericResolvingContractType)
            {
                genericTypeArguments = context.ResolvingKey.ContractType.GenericTypeArguments();
            }
            else
            {
                genericTypeArguments = context.ResolvingContainer.Get<IIssueResolver>().CannotGetGenericTypeArguments(context.ResolvingKey.ContractType);
            }

            var targetContractType = genericTypeArguments[0];
            var keys =
                from key in context.ResolvingContainer as IEnumerable<Key> ?? Enumerable.Empty<Key>()
                where key.ContractType == targetContractType
                select key;

            var instanceType = typeof(InstanceEnumerable<>).MakeGenericType(genericTypeArguments);
            var resolvingKey = new Key(targetContractType, context.ResolvingKey.Tag);
            var newContext = new Context(context.RegistrationId, context.RegistrationKey, context.RegistrationContainer, resolvingKey, context.ResolvingContainer, context.Args, targetContractType.IsConstructedGenericType());
            return Activator.CreateInstance(instanceType, keys, newContext);
        }

        private class InstanceEnumerable<T> : IEnumerable<T>
        {
            private readonly IEnumerable<T> _instances;

            public InstanceEnumerable(IEnumerable<Key> key, Context context)
            {
                _instances = GetInstances(key, context);
            }

            public IEnumerator<T> GetEnumerator()
            {
                return _instances.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            private static IEnumerable<T> GetInstances(IEnumerable<Key> keys, Context context)
            {
                foreach (var key in keys)
                {
                    if (!context.ResolvingContainer.TryGetResolver(key, out var resolver))
                    {
                        continue;
                    }

                    yield return (T)resolver.Resolve(context.ResolvingKey, context.ResolvingContainer, 0, context.Args);
                }
            }
        }
    }
}
