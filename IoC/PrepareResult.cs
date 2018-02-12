namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [PublicAPI]
    public struct PrepareResult
    {
        [NotNull] public readonly IEnumerable<Key> ResolvedKey;
        [NotNull] public readonly IEnumerable<Key> UnresolvedKeys;

        internal PrepareResult([NotNull] List<Key> resolvedKey, [NotNull] List<Key> unresolvedKeys)
        {
            ResolvedKey = resolvedKey ?? throw new ArgumentNullException(nameof(resolvedKey));
            UnresolvedKeys = unresolvedKeys ?? throw new ArgumentNullException(nameof(unresolvedKeys));
        }

        public bool IsValid => !UnresolvedKeys.Any();
    }
}
