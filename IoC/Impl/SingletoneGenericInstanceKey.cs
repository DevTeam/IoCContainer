namespace IoC.Impl
{
    using System;

    internal struct SingletoneGenericInstanceKey : IInstanceKey
    {
        private readonly long _contextRegistrationId;
        private readonly Type[] _contractTypeGenericTypeArguments;

        public SingletoneGenericInstanceKey(long contextRegistrationId, Type[] contractTypeGenericTypeArguments)
        {
            _contextRegistrationId = contextRegistrationId;
            _contractTypeGenericTypeArguments = contractTypeGenericTypeArguments;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SingletoneGenericInstanceKey key && Equals(key);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)_contextRegistrationId * 397) ^ (_contractTypeGenericTypeArguments != null ? GetHashCode(_contractTypeGenericTypeArguments) : 0);
            }
        }
        private bool Equals(SingletoneGenericInstanceKey other)
        {
            return _contextRegistrationId == other._contextRegistrationId && SequenceEqual(_contractTypeGenericTypeArguments, other._contractTypeGenericTypeArguments);
        }

        private static int GetHashCode<T>([NotNull] T[] array)
        {
            return ((System.Collections.IStructuralEquatable)array).GetHashCode(System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }

        private static bool SequenceEqual<T>([NotNull] T[] array1, [NotNull] T[] array2)
        {
            return ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }
    }
}
