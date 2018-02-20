namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a container's validation result.
    /// </summary>
    [PublicAPI]
    public struct ValidationResult
    {
        /// <summary>
        /// Keys that were resolved successfully.
        /// </summary>
        [NotNull] public readonly IEnumerable<Key> ResolvedKey;

        /// <summary>
        /// Keys that were not resolved successfully.
        /// </summary>
        [NotNull] public readonly IEnumerable<Key> UnresolvedKeys;

        internal ValidationResult([NotNull] List<Key> resolvedKey, [NotNull] List<Key> unresolvedKeys)
        {
            ResolvedKey = resolvedKey ?? throw new ArgumentNullException(nameof(resolvedKey));
            UnresolvedKeys = unresolvedKeys ?? throw new ArgumentNullException(nameof(unresolvedKeys));
        }

        /// <summary>
        /// True if the container could be used successfully.
        /// </summary>
        public bool IsValid => !UnresolvedKeys.Any();
    }
}
