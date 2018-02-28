﻿namespace IoC.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class TypeReplacerExpressionVisitor: ExpressionVisitor
    {
        [NotNull] private readonly IDictionary<Type, Type> _typesMap;
        [NotNull] private readonly Dictionary<string, ParameterExpression> _parameters = new Dictionary<string, ParameterExpression>();

        public TypeReplacerExpressionVisitor([NotNull] IDictionary<Type, Type> typesMap)
        {
            _typesMap = typesMap ?? throw new ArgumentNullException(nameof(typesMap));
        }

        protected override Expression VisitNew(NewExpression node)
        {
            var newTypeInfo = ReplaceType(node.Type).Info();
            var newConstructor = newTypeInfo.DeclaredConstructors.Single(i => !i.IsPrivate && Match(node.Constructor.GetParameters(), i.GetParameters()));
            var newArgs = ReplaceAll(node.Arguments).ToList();
            return Expression.New(newConstructor, newArgs);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            var newType = ReplaceType(node.Type);
            switch (node.NodeType)
            {
                case ExpressionType.Convert:
                    var newOperand = Visit(node.Operand);
                    if (newOperand == null)
                    {
                        return base.VisitUnary(node);
                    }

                    return newOperand.Convert(newType);

                default:
                    return base.VisitUnary(node);
            }
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var newDeclaringType = ReplaceType(node.Method.DeclaringType).Info();
            var newMethod = newDeclaringType.DeclaredMethods.SingleOrDefault(i => i.Name == node.Method.Name && Match(node.Method.GetParameters(), i.GetParameters()));
            if (newMethod == null)
            {
                throw new BuildExpressionException(new InvalidOperationException($"Cannot find method {node.Method} in the {node.Method.DeclaringType}."));
            }

            if (newMethod.IsGenericMethod)
            {
                newMethod = newMethod.MakeGenericMethod(ReplaceTypes(node.Method.GetGenericArguments()));
            }

            var newArgs = ReplaceAll(node.Arguments).ToList();
            return node.Object != null ? Expression.Call(Visit(node.Object), newMethod, newArgs) : Expression.Call(newMethod, newArgs);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var newDeclaringTypeInfo = ReplaceType(node.Member.DeclaringType).Info();
            if (newDeclaringTypeInfo.IsConstructedGenericType)
            {
                newDeclaringTypeInfo = ReplaceType(newDeclaringTypeInfo.Type).Info();
            }

            var newMember = newDeclaringTypeInfo.DeclaredMembers.Single(i => i.Name == node.Member.Name);
            var newExpression = Visit(node.Expression);
            if (newExpression == null)
            {
                return base.VisitMember(node);
            }

            return Expression.MakeMemberAccess(newExpression, newMember);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parameters.TryGetValue(node.Name, out var newNode))
            {
                return newNode;
            }

            if (node.IsByRef)
            {
                newNode = Expression.Parameter(ReplaceType(node.Type).MakeByRefType(), node.Name);
            }
            else
            {
                newNode = Expression.Parameter(ReplaceType(node.Type), node.Name);
            }

            _parameters[newNode.Name] = newNode;
            return newNode;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node.Type == typeof(Type))
            {
                return Expression.Constant(ReplaceType((Type)node.Value), node.Type);
            }

            return Expression.Constant(node.Value, ReplaceType(node.Type));
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var parameters = node.Parameters.Select(VisitParameter).Cast<ParameterExpression>();
            var body = Visit(node.Body);
            if (body == null)
            {
                return base.VisitLambda(node);
            }

            return Expression.Lambda(ReplaceType(node.Type), body, parameters);
        }

        protected override Expression VisitNewArray(NewArrayExpression node)
        {
            return Expression.NewArrayInit(ReplaceType(node.Type.GetElementType()), ReplaceAll(node.Expressions));
        }

        protected override Expression VisitListInit(ListInitExpression node)
        {
            var newExpression = (NewExpression)Visit(node.NewExpression);
            if (newExpression == null)
            {
                return node;
            }

            return Expression.ListInit(newExpression, node.Initializers.Select(VisitInitializer));
        }

        private ElementInit VisitInitializer(ElementInit node)
        {
            var newDeclaringType = ReplaceType(node.AddMethod.DeclaringType).Info();
            var newMethod = newDeclaringType.DeclaredMethods.SingleOrDefault(i => i.Name == node.AddMethod.Name && Match(node.AddMethod.GetParameters(), i.GetParameters()));
            if (newMethod == null)
            {
                throw new BuildExpressionException(new InvalidOperationException($"Cannot find method {node.AddMethod} in the {node.AddMethod.DeclaringType}."));
            }

            if (newMethod.IsGenericMethod)
            {
                newMethod = newMethod.MakeGenericMethod(ReplaceTypes(node.AddMethod.GetGenericArguments()));
            }

            var newArgs = ReplaceAll(node.Arguments).ToList();
            return Expression.ElementInit(newMethod, newArgs);
        }

        private bool Match(ParameterInfo[] baseParams, ParameterInfo[] newParams)
        {
            if (baseParams.Length != newParams.Length)
            {
                return false;
            }

            for (var i = 0; i < baseParams.Length; i++)
            {
                if (baseParams[i].Name != newParams[i].Name)
                {
                    return false;
                }

                var paramTypeInfo = newParams[i].ParameterType.Info();
                if (paramTypeInfo.IsGenericParameter)
                {
                    return true;
                }

                if (ReplaceType(baseParams[i].ParameterType).Info().Id != paramTypeInfo.Id)
                {
                    return false;
                }
            }

            return true;
        }

        private Type[] ReplaceTypes(Type[] types)
        {
            return types.Select(ReplaceType).ToArray();
        }

        private Type ReplaceType(Type type)
        {
            var baseTypeInfo = type.Info();
            if (!baseTypeInfo.IsConstructedGenericType)
            {
                if (_typesMap.TryGetValue(type, out var newType))
                {
                    return newType;
                }

                if (baseTypeInfo.IsArray)
                {
                    var elementType = baseTypeInfo.ElementType;
                    var newElementType = ReplaceType(baseTypeInfo.ElementType);
                    if (elementType != newElementType)
                    {
                        return newElementType.MakeArrayType();
                    }

                    return type;
                }

                return type;
            }

            var newGenericTypes = new Type[baseTypeInfo.GenericTypeArguments.Length];
            var genericTypes = ReplaceTypes(baseTypeInfo.GenericTypeArguments);
            for (var i = 0; i < genericTypes.Length; i++)
            {
                var genericType = genericTypes[i];
                if (_typesMap.TryGetValue(genericType, out var newGenericType))
                {
                    newGenericTypes[i] = newGenericType;
                }
                else
                {
                    newGenericTypes[i] = ReplaceType(genericType);
                }
            }

            return baseTypeInfo.GetGenericTypeDefinition().Info().MakeGenericType(newGenericTypes);
        }

        private IEnumerable<Expression> ReplaceAll(IEnumerable<Expression> expressions)
        {
            return expressions.Select(Visit);
        }
    }
}