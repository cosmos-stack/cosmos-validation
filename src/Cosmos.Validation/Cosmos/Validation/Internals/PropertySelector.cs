using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cosmos.Validation.Internals
{
    internal static class PropertySelector
    {
        public static PropertyInfo GetPropertyInfo<T>(Expression<Func<T, object>> selector)
        {
            if (selector.NodeType is not ExpressionType.Lambda)
            {
                throw new ArgumentException("Selector must be lambda expression.", nameof(selector));
            }

            var lambda = (LambdaExpression) selector;

            var memberExpression = ExtractMemberExpression(lambda.Body);

            if (memberExpression is null)
            {
                throw new ArgumentException("Selector must be member access expression.", nameof(selector));
            }

            if (memberExpression.Member.DeclaringType is null)
            {
                throw new InvalidOperationException("Property does not have declaring type.");
            }

            return memberExpression.Member.DeclaringType.GetProperty(memberExpression.Member.Name);
        }

        public static PropertyInfo GetPropertyInfo<T, TValue>(Expression<Func<T, TValue>> selector)
        {
            if (selector.NodeType is not ExpressionType.Lambda)
            {
                throw new ArgumentException("Selector must be lambda expression.", nameof(selector));
            }

            var lambda = (LambdaExpression) selector;

            var memberExpression = ExtractMemberExpression(lambda.Body);

            if (memberExpression is null)
            {
                throw new ArgumentException("Selector must be member access expression.", nameof(selector));
            }

            if (memberExpression.Member.DeclaringType is null)
            {
                throw new InvalidOperationException("Property does not have declaring type.");
            }

            return memberExpression.Member.DeclaringType.GetProperty(memberExpression.Member.Name);
        }

        public static string GetPropertyName<T>(Expression<Func<T, object>> selector)
        {
            if (selector.NodeType is not ExpressionType.Lambda)
            {
                throw new ArgumentException("Selector must be lambda expression.", nameof(selector));
            }

            var lambda = (LambdaExpression) selector;

            var memberExpression = ExtractMemberExpression(lambda.Body);

            if (memberExpression is null)
            {
                throw new ArgumentException("Selector must be member access expression.", nameof(selector));
            }

            return memberExpression.Member.Name;
        }

        public static string GetPropertyName<T, TValue>(Expression<Func<T, TValue>> selector)
        {
            if (selector.NodeType is not ExpressionType.Lambda)
            {
                throw new ArgumentException("Selector must be lambda expression.", nameof(selector));
            }

            var lambda = (LambdaExpression) selector;

            var memberExpression = ExtractMemberExpression(lambda.Body);

            if (memberExpression is null)
            {
                throw new ArgumentException("Selector must be member access expression.", nameof(selector));
            }

            return memberExpression.Member.Name;
        }

        private static MemberExpression ExtractMemberExpression(Expression expression)
        {
            if (expression.NodeType is ExpressionType.MemberAccess)
            {
                return ((MemberExpression) expression);
            }

            if (expression.NodeType is ExpressionType.Convert)
            {
                var operand = ((UnaryExpression) expression).Operand;
                return ExtractMemberExpression(operand);
            }

            return null;
        }
    }
}