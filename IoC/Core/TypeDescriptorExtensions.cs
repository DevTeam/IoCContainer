﻿namespace IoC.Core
{
    using System;
    // ReSharper disable once RedundantUsingDirective
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class TypeDescriptorExtensions
    {
        [MethodImpl((MethodImplOptions)0x100)]
        public static TypeDescriptor Descriptor(this Type type) => new TypeDescriptor(type);

        [MethodImpl((MethodImplOptions)0x100)]
        public static TypeDescriptor Descriptor<T>() => TypeDescriptor<T>.Descriptor;

        [MethodImpl((MethodImplOptions)0x100)]
        public static Assembly LoadAssembly(string assemblyName)
        {
            if (string.IsNullOrWhiteSpace(assemblyName)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(assemblyName));
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static Type ToGenericType([NotNull] this Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            var typeDescriptor = type.Descriptor();
            if (typeDescriptor.IsArray())
            {
                var elementType = typeDescriptor.GetElementType();
                if (elementType.Descriptor().IsGenericTypeArgument())
                {
                    return typeof(IArray);
                }
            }

            if (!typeDescriptor.IsConstructedGenericType())
            {
                return type;
            }

            if (typeDescriptor.GetGenericTypeArguments().Any(t => t.Descriptor().IsGenericTypeArgument()))
            {
                return typeDescriptor.GetGenericTypeDefinition();
            }

            return type;
        }

        [MethodImpl((MethodImplOptions)0x100)]
        [NotNull]
        public static string GetShortName([NotNull] this Type type)
        {
            if (!TypeToStringConverter.Shared.TryConvert(type, type, out var typeName))
            {
                typeName = type.FullName;
            }

            return typeName ?? "UnknownType";
        }
    }
}
