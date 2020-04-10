
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global
namespace IoC
{
    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS { }

/// <summary>
    /// Represents the generic type parameter marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable: System.IDisposable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable: System.IComparable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT1 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI1 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS1 { }

/// <summary>
    /// Represents the generic type parameter marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable1: System.IDisposable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable1: System.IComparable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable1<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable1<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable1<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator1<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection1<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList1<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet1<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer1<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer1<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary1<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable1<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver1<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT2 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI2 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS2 { }

/// <summary>
    /// Represents the generic type parameter marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable2: System.IDisposable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable2: System.IComparable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable2<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable2<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable2<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator2<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection2<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList2<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet2<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer2<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer2<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary2<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable2<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver2<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT3 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI3 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS3 { }

/// <summary>
    /// Represents the generic type parameter marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable3: System.IDisposable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable3: System.IComparable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable3<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable3<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable3<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator3<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection3<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList3<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet3<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer3<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer3<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary3<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable3<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver3<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT4 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI4 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS4 { }

/// <summary>
    /// Represents the generic type parameter marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable4: System.IDisposable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable4: System.IComparable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable4<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable4<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable4<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator4<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection4<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList4<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet4<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer4<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer4<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary4<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable4<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver4<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT5 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI5 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS5 { }

/// <summary>
    /// Represents the generic type parameter marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable5: System.IDisposable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable5: System.IComparable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable5<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable5<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable5<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator5<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection5<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList5<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet5<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer5<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer5<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary5<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable5<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver5<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT6 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI6 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS6 { }

/// <summary>
    /// Represents the generic type parameter marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable6: System.IDisposable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable6: System.IComparable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable6<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable6<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable6<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator6<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection6<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList6<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet6<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer6<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer6<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary6<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable6<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver6<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT7 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI7 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS7 { }

/// <summary>
    /// Represents the generic type parameter marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable7: System.IDisposable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable7: System.IComparable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable7<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable7<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable7<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator7<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection7<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList7<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet7<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer7<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer7<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary7<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable7<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver7<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT8 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI8 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS8 { }

/// <summary>
    /// Represents the generic type parameter marker for <c>System.IDisposable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDisposable8: System.IDisposable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable8: System.IComparable { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IComparable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparable8<in T>: System.IComparable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IEquatable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEquatable8<T>: System.IEquatable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerable8<out T>: System.Collections.Generic.IEnumerable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEnumerator[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEnumerator8<out T>: System.Collections.Generic.IEnumerator<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ICollection[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTCollection8<T>: System.Collections.Generic.ICollection<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IList[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTList8<T>: System.Collections.Generic.IList<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.ISet[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTSet8<T>: System.Collections.Generic.ISet<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTComparer8<in T>: System.Collections.Generic.IComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IEqualityComparer[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTEqualityComparer8<in T>: System.Collections.Generic.IEqualityComparer<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.Collections.Generic.IDictionary[TKey, TValue]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTDictionary8<TKey, TValue>: System.Collections.Generic.IDictionary<TKey, TValue> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObservable[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObservable8<out T>: System.IObservable<T> { }

    /// <summary>
    /// Represents the generic type parameter marker for <c>System.IObserver[T]</c>.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTObserver8<in T>: System.IObserver<T> { }

        /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT9 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI9 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS9 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT10 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI10 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS10 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT11 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI11 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS11 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT12 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI12 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS12 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT13 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI13 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS13 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT14 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI14 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS14 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT15 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI15 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS15 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT16 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI16 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS16 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT17 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI17 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS17 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT18 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI18 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS18 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT19 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI19 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS19 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT20 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI20 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS20 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT21 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI21 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS21 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT22 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI22 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS22 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT23 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI23 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS23 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT24 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI24 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS24 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT25 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI25 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS25 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT26 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI26 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS26 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT27 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI27 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS27 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT28 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI28 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS28 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT29 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI29 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS29 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT30 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI30 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS30 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT31 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI31 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS31 { }

    /// <summary>
    /// Represents the generic type parameter marker for a reference type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public abstract class TT32 { }

    /// <summary>
    /// Represents the generic type parameter marker for an interface.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public interface TTI32 { }

    /// <summary>
    /// Represents the generic type parameter marker for a value type.
    /// </summary>
    [PublicAPI, GenericTypeArgument]
    public struct TTS32 { }


    internal static class GenericTypeArguments
    {
        internal static readonly System.Type[] Arguments =
        {
            Core.TypeDescriptor<TT>.Type,
            Core.TypeDescriptor<TT1>.Type,
            Core.TypeDescriptor<TT2>.Type,
            Core.TypeDescriptor<TT3>.Type,
            Core.TypeDescriptor<TT4>.Type,
            Core.TypeDescriptor<TT5>.Type,
            Core.TypeDescriptor<TT6>.Type,
            Core.TypeDescriptor<TT7>.Type,
            Core.TypeDescriptor<TT8>.Type,
            Core.TypeDescriptor<TT9>.Type,
            Core.TypeDescriptor<TT10>.Type,
            Core.TypeDescriptor<TT11>.Type,
            Core.TypeDescriptor<TT12>.Type,
            Core.TypeDescriptor<TT13>.Type,
            Core.TypeDescriptor<TT14>.Type,
            Core.TypeDescriptor<TT15>.Type,
            Core.TypeDescriptor<TT16>.Type,
            Core.TypeDescriptor<TT17>.Type,
            Core.TypeDescriptor<TT18>.Type,
            Core.TypeDescriptor<TT19>.Type,
            Core.TypeDescriptor<TT20>.Type,
            Core.TypeDescriptor<TT21>.Type,
            Core.TypeDescriptor<TT22>.Type,
            Core.TypeDescriptor<TT23>.Type,
            Core.TypeDescriptor<TT24>.Type,
            Core.TypeDescriptor<TT25>.Type,
            Core.TypeDescriptor<TT26>.Type,
            Core.TypeDescriptor<TT27>.Type,
            Core.TypeDescriptor<TT28>.Type,
            Core.TypeDescriptor<TT29>.Type,
            Core.TypeDescriptor<TT30>.Type,
            Core.TypeDescriptor<TT31>.Type,
            Core.TypeDescriptor<TT32>.Type,
        };
    }
}
