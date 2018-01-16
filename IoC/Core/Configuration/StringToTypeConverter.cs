namespace IoC.Core.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    // ReSharper disable once ClassNeverInstantiated.Global
    internal sealed class StringToTypeConverter : IConverter<string, BindingContext, Type>
    {
        // ReSharper disable StringLiteralTypo
        private static readonly Dictionary<string, Type> PrimitiveTypes = new Dictionary<string, Type>
        {
            {"byte", typeof(byte)},
            {"sbyte", typeof(sbyte)},
            {"int", typeof(int)},
            {"uint", typeof(uint)},
            {"short", typeof(short)},
            {"ushort", typeof(ushort)},
            {"long", typeof(long)},
            {"ulong", typeof(ulong)},
            {"float", typeof(float)},
            {"double", typeof(double)},
            {"char", typeof(char)},
            {"object", typeof(object)},
            {"string", typeof(string)},
            {"decimal", typeof(decimal)}
        };

        public bool TryConvert(BindingContext context, string typeName, out Type type)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (string.IsNullOrWhiteSpace(typeName))
            {
                type = default(Type);
                return false;
            }

            if (TryResolveSimpleType(typeName, out type))
            {
                return true;
            }

            var typeDescription = new TypeDescription(context.Assemblies, context.Namespaces, typeName);
            if (!typeDescription.IsValid)
            {
                return false;
            }

            type = typeDescription.Type;
            return true;
        }

        private static bool TryResolveSimpleType(string typeName, out Type type)
        {
            if (typeName == null)
            {
                type = default(Type);
                return false;
            }

            if (PrimitiveTypes.TryGetValue(typeName, out type))
            {
                return true;
            }

            if (typeName.Contains("<") || typeName.Contains(">"))
            {
                return false;
            }

            if (ReflectionLoad(typeName, out type))
            {
                return true;
            }

            return false;
        }

        private static bool ReflectionLoad(string typeName, out Type type)
        {
            type = Type.GetType(typeName, false);
            return type != null;
        }

        private sealed class TypeDescription
        {
            private readonly ICollection<Assembly> _assemblies;
            private readonly ICollection<string> _namespaces;

            public TypeDescription(
                IEnumerable<Assembly> assemblies,
                IEnumerable<string> namespaces,
                string typeName)
            {
                _assemblies = assemblies as ICollection<Assembly> ?? assemblies.ToList();
                _namespaces = namespaces as ICollection<string> ?? namespaces.ToList();
                GenericTypeArgs = new List<TypeDescription>();

                if (typeName == null)
                {
                    return;
                }

                typeName = typeName.Trim();
                if (string.IsNullOrEmpty(typeName))
                {
                    IsValid = true;
                    IsUndefined = true;
                    return;
                }

                var genericStartIndex = typeName.IndexOf('<');
                var genericFinishIndex = typeName.LastIndexOf('>');
                if (genericStartIndex >= 0 && genericFinishIndex >= 0)
                {
                    if (genericStartIndex < genericFinishIndex)
                    {
                        var genericTypeArgsStr = typeName.Substring(genericStartIndex + 1, genericFinishIndex - genericStartIndex - 1);
                        typeName = typeName.Substring(0, genericStartIndex);
                        var isValid = true;
                        var nested = 0;
                        var index = 0;
                        var curIndex = 0;
                        var requiredArgs = 1;
                        var args = new List<string>();
                        foreach (var genericTypeArgsChar in genericTypeArgsStr)
                        {
                            switch (genericTypeArgsChar)
                            {
                                case '<':
                                    nested++;
                                    break;

                                case '>':
                                    nested--;
                                    if (nested < 0)
                                    {
                                        isValid = false;
                                    }

                                    break;

                                case ',':
                                    if (nested == 0)
                                    {
                                        args.Add(genericTypeArgsStr.Substring(index, curIndex - index).Trim());
                                        index = curIndex + 1;
                                        requiredArgs++;
                                    }

                                    break;
                            }

                            if (!isValid)
                            {
                                break;
                            }

                            curIndex++;
                        }

                        if (isValid && requiredArgs > args.Count)
                        {
                            args.Add(genericTypeArgsStr.Substring(index, curIndex - index).Trim());
                        }

                        foreach (var genericTypeArgStr in args)
                        {
                            var genericTypeDescriptor = new TypeDescription(_assemblies, _namespaces, genericTypeArgStr);
                            isValid &= genericTypeDescriptor.IsValid;
                            if (!isValid)
                            {
                                break;
                            }

                            GenericTypeArgs.Add(genericTypeDescriptor);
                        }

                        if (!isValid)
                        {
                            return;
                        }

                        if (GenericTypeArgs.Count > 0)
                        {
                            var cnt = 0;
                            var genericArgsSb = new StringBuilder($"`{GenericTypeArgs.Count}");
                            if (GenericTypeArgs.Any(i => !i.IsUndefined))
                            {
                                genericArgsSb.Append('[');
                                foreach (var genericTypeArg in GenericTypeArgs)
                                {
                                    if (cnt > 0)
                                    {
                                        genericArgsSb.Append(',');
                                    }

                                    cnt++;
                                    if (!genericTypeArg.IsUndefined)
                                    {
                                        genericArgsSb.Append('[');
                                        genericArgsSb.Append(genericTypeArg.Type.AssemblyQualifiedName);
                                        genericArgsSb.Append(']');
                                    }
                                    else
                                    {
                                        genericArgsSb.Append("[]");
                                    }
                                }
                                genericArgsSb.Append(']');
                            }

                            typeName = typeName + genericArgsSb;
                        }

                        IsValid = LoadTypeUsingVariants(typeName);
                        return;
                    }
                }

                if (genericStartIndex == -1 && genericFinishIndex == -1)
                {
                    IsValid = LoadTypeUsingVariants(typeName);
                }
            }

            public bool IsValid { get; }

            private IList<TypeDescription> GenericTypeArgs { get; }

            private bool IsUndefined { get; }

            public Type Type { get; private set; }

            private bool LoadTypeUsingVariants(string typeName)
            {
                if (LoadType(typeName))
                {
                    return true;
                }

                foreach (var usingName in _namespaces)
                {
                    var fullTypeName = $"{usingName}.{typeName}";
                    if (LoadType(fullTypeName))
                    {
                        return true;
                    }
                }

                return false;
            }

            private bool LoadType(string typeName)
            {
                if (ReflectionLoad(typeName, out Type type))
                {
                    OnTypeLoaded(type);
                    return true;
                }

                if (LoadTypeInternal(typeName))
                {
                    return true;
                }

                while (TryGetNestedTypeName(typeName, out var newTypeName))
                {
                    if (LoadTypeInternal(newTypeName))
                    {
                        return true;
                    }

                    typeName = newTypeName;
                }

                return false;
            }

            private bool TryGetNestedTypeName([NotNull] string typeName, out string nestedTypeName)
            {
                if (typeName == null) throw new ArgumentNullException(nameof(typeName));
                var pointIndex = -1;
                var nested = 0;
                for (var i = typeName.Length - 1; i >= 0; i--)
                {
                    switch (typeName[i])
                    {
                        case ']':
                            nested++;
                            continue;

                        case '[':
                            nested--;
                            continue;
                    }

                    if (nested > 0)
                    {
                        continue;
                    }

                    if (typeName[i] == '.')
                    {
                        pointIndex = i;
                        break;
                    }
                }

                if (pointIndex < 0)
                {
                    nestedTypeName = default(string);
                    return false;
                }

                nestedTypeName = typeName.Substring(0, pointIndex) + "+" + typeName.Substring(pointIndex + 1, typeName.Length - pointIndex - 1);
                return true;
            }

            private bool LoadTypeInternal(string typeName)
            {
                if (TryResolveSimpleType(typeName, out Type type))
                {
                    OnTypeLoaded(type);
                    return true;
                }

                foreach (var reference in _assemblies)
                {
                    var assemblyQualifiedName = $"{typeName}, {reference.GetName()}";
                    type = Type.GetType(assemblyQualifiedName);
                    if (type != null)
                    {
                        OnTypeLoaded(type);
                        return true;
                    }

                    type = reference.GetType(assemblyQualifiedName);
                    if (type != null)
                    {
                        OnTypeLoaded(type);
                        return true;
                    }
                }

                return false;
            }

            private void OnTypeLoaded(Type type)
            {
                Type = type;
            }
        }
    }
}