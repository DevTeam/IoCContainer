namespace IoC
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Represents an abstraction for an autowirin method.
    /// </summary>
    /// <typeparam name="TMethodInfo">The type of method info.</typeparam>
    [PublicAPI]
    public interface IMethod<out TMethodInfo>
        where TMethodInfo: MethodBase
    {
        /// <summary>
        /// The method's information.
        /// </summary>
        [NotNull] TMethodInfo Info { get; }

        /// <summary>
        /// Parameter's expression at the position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [NotNull] Expression this[int position] { get; set; }

        // void DefineParameter<T>(int position, [NotNull] Expression<Func<Context, T>> expression);
    }
}
