namespace IoC.Core
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal static class PrepareUtils
    {
        private static readonly MethodInfo TryGetResolverInternalMethodInfo = typeof(PrepareUtils).Info().DeclaredMethods.Single(i => i.Name == nameof(TryGetResolverInternal));

        public static PrepareResult Prepare([NotNull] this IContainer container)
        {
            var resolvedKeys = new List<Key>();
            var unresolvedKeys = new List<Key>();
            foreach (var key in container)
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

            return new PrepareResult(resolvedKeys, unresolvedKeys);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public static bool TryGetResolverInternal<T>(IContainer container, Key key)
        {
            return container.TryGetResolver<T>(key, out var _);
        }
    }
}
