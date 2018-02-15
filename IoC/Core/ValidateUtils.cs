namespace IoC.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class ValidateUtils
    {
        private static readonly MethodInfo TryGetResolverInternalMethodInfo = typeof(ValidateUtils).Info().DeclaredMethods.Single(i => i.Name == nameof(TryGetResolverInternal));

        public static ValidationResult Validate([NotNull] this IContainer container)
        {
            var resolvedKeys = new List<Key>();
            var unresolvedKeys = new List<Key>();
            foreach (var key in container.SelectMany(i => i))
            {
                var curKey = new Key(ExpressionBuilder.Shared.CreateDefinedGenericType(key.Type), key.Tag);
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
        public static bool TryGetResolverInternal<T>(IContainer container, Key key)
        {
            return container.TryGetResolver<T>(container, key.Type, key.Tag, out var _);
        }
    }
}
