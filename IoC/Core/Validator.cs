namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Extensibility;

    internal class Validator : IValidator
    {
        public static readonly IValidator Shared = new Validator();
        private static readonly MethodInfo TryGetResolverInternalMethodInfo = typeof(Validator).Descriptor().GetDeclaredMethods().Single(i => i.Name == nameof(TryGetResolverInternal));

        private Validator()
        {
        }

        public ValidationResult Validate(IContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));
            var resolvedKeys = new List<Key>();
            var unresolvedKeys = new List<Key>();
            foreach (var key in container.SelectMany(i => i))
            {
                var curTypeDescriptor = key.Type.Descriptor();
                var curKey = new Key(curTypeDescriptor.ToDefinedGenericType(), key.Tag);
                var tryGetResolverInternal = TryGetResolverInternalMethodInfo.MakeGenericMethod(curKey.Type);
                var resolved = (bool)tryGetResolverInternal.Invoke(null, new object[] { container, curKey });
                if (resolved)
                {
                    resolvedKeys.Add(curKey);
                }
                else
                {
                    unresolvedKeys.Add(curKey);
                }
            }

            return new ValidationResult(resolvedKeys, unresolvedKeys);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        private static bool TryGetResolverInternal<T>(IContainer container, Key key)
            => container.TryGetResolver<T>(key.Type, key.Tag, out var _, container);
    }
}
