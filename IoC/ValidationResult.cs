namespace IoC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a container's validation result.
    /// </summary>
    public struct ValidationResult
    {
        /// <summary>
        /// Keys that were resolved successfully.
        /// </summary>
        [NotNull] public readonly IEnumerable<Key> ResolvedKeys;

        /// <summary>
        /// Keys that were not resolved successfully.
        /// </summary>
        [NotNull] public readonly IEnumerable<Key> UnresolvedKeys;

        internal ValidationResult([NotNull] IEnumerable<Key> resolvedKeys, [NotNull] IEnumerable<Key> unresolvedKeys)
        {
            ResolvedKeys = (resolvedKeys ?? throw new ArgumentNullException(nameof(resolvedKeys))).ToList();
            UnresolvedKeys = (unresolvedKeys ?? throw new ArgumentNullException(nameof(unresolvedKeys))).ToList();
        }

        /// <summary>
        /// True if the container could be used successfully.
        /// </summary>
        public bool IsValid => !UnresolvedKeys.Any();
    }
}
