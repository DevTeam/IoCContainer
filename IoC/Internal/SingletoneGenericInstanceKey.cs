namespace IoC.Internal
{
    using System;

    internal struct SingletoneGenericInstanceKey<T> : IInstanceKey
    {
        private readonly T _id;
        private readonly Type[] _contractTypeGenericTypeArguments;

        public SingletoneGenericInstanceKey(T id, Type[] contractTypeGenericTypeArguments)
        {
            _id = id;
            _contractTypeGenericTypeArguments = contractTypeGenericTypeArguments;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is SingletoneGenericInstanceKey<T> key && Equals(key);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_id.GetHashCode() * 397) ^ (_contractTypeGenericTypeArguments != null ? GetHashCode(_contractTypeGenericTypeArguments) : 0);
            }
        }
        private bool Equals(SingletoneGenericInstanceKey<T> other)
        {
            return Equals(_id, other._id) && SequenceEqual(_contractTypeGenericTypeArguments, other._contractTypeGenericTypeArguments);
        }

        private static int GetHashCode<TA>([NotNull] TA[] array)
        {
            return ((System.Collections.IStructuralEquatable)array).GetHashCode(System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }

        private static bool SequenceEqual<TA>([NotNull] TA[] array1, [NotNull] TA[] array2)
        {
            return ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }
    }
}
