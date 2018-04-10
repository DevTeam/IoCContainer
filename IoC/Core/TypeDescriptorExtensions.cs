namespace IoC.Core
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using Collections;

    internal static class TypeDescriptorExtensions
    {
        private static readonly object LockObject = new object();
        private static Table<Type, TypeDescriptor> _typeDescriptors = Table<Type, TypeDescriptor>.Empty;

        [MethodImpl((MethodImplOptions)256)]
        public static TypeDescriptor Descriptor(this Type type)
        {
            lock (LockObject)
            {
                var hashCode = type.GetHashCode();
                var typeDescriptor = _typeDescriptors.FastGet(hashCode, type);
                if (typeDescriptor != null)
                {
                    return typeDescriptor;
                }
                
                typeDescriptor = new TypeDescriptor(type);
                _typeDescriptors = _typeDescriptors.Set(hashCode, type, typeDescriptor);
                return typeDescriptor;
            }
        }

        [MethodImpl((MethodImplOptions)256)]
        public static TypeDescriptor Descriptor<T>() => TypeDescriptor<T>.Shared;

        [MethodImpl((MethodImplOptions)256)]
        public static Assembly LoadAssembly(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(assemblyName));
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        [MethodImpl((MethodImplOptions)256)]
        [NotNull]
        public static Type ToGenericType([NotNull] this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var typeInfo = type.Descriptor();
            if (!typeInfo.IsConstructedGenericType())
            {
                return type;
            }

            if (typeInfo.GetGenericTypeArguments().Any(t => Descriptor(t).IsGenericTypeArgument()))
            {
                return typeInfo.GetGenericTypeDefinition();
            }

            return type;
        }

        private static class TypeDescriptor<T>
        {
            [NotNull] public static readonly TypeDescriptor Shared = typeof(T).Descriptor();
        }
    }
}
