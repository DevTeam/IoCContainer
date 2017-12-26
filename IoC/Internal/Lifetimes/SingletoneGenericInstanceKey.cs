namespace IoC.Internal.Lifetimes
{
    using System;
    using System.Collections.Generic;

    internal struct SingletoneGenericInstanceKey<T> : IInstanceKey
    {
        private static readonly EqualityComparer<T> Comparer = EqualityComparer<T>.Default;
        private readonly T _id;
        private readonly Type[] _contractTypeGenericTypeArguments;
        private readonly int _hashCode;

        public SingletoneGenericInstanceKey(T id, Type[] contractTypeGenericTypeArguments)
        {
            _id = id;
            _contractTypeGenericTypeArguments = contractTypeGenericTypeArguments;
            unchecked
            {
                _hashCode = (id.GetHashCode() * 397) ^ (contractTypeGenericTypeArguments != null ? GetHashCode(_contractTypeGenericTypeArguments) : 0);
            }
        }

        public override bool Equals(object obj)
        {
            // if (ReferenceEquals(null, obj)) return false;
            return obj is SingletoneGenericInstanceKey<T> key && Comparer.Equals(_id, key._id) && SequenceEqual(_contractTypeGenericTypeArguments, key._contractTypeGenericTypeArguments);
        }

        public override int GetHashCode()
        {
            return _hashCode;
        }

#if !NET40
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
        private static int GetHashCode<TA>([NotNull] TA[] array)
        {
            return ((System.Collections.IStructuralEquatable)array).GetHashCode(System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }

#if !NET40
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
        private bool SequenceEqual<TA>([NotNull] TA[] array1, [NotNull] TA[] array2)
        {
            return ((System.Collections.IStructuralEquatable)array1).Equals(array2, System.Collections.StructuralComparisons.StructuralEqualityComparer);
        }
    }
}
