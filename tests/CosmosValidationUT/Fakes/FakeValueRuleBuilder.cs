using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Cosmos.Reflection;
using Cosmos.Validation;
using Cosmos.Validation.Objects;

namespace CosmosValidationUT.Fakes
{
    public class FakeValueRuleBuilder : IValueRuleBuilder
    {
        internal readonly VerifiableMemberContract Contract;

        public FakeValueRuleBuilder(VerifiableMemberContract contract)
        {
            Contract = contract;
        }

        public IValueRuleBuilder AppendRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder OverwriteRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder And()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Or()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Empty()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder NotEmpty()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Required()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Null()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder NotNull()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Range(object @from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RangeWithOpenInterval(object @from, object to)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RangeWithCloseInterval(object @from, object to)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Length(int min, int max)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder MinLength(int min)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder MaxLength(int max)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder AtLeast(int count)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Equal(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Equal(object value, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder NotEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder NotEqual(object value, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder LessThan(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder LessThanOrEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder GreaterThan(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder GreaterThanOrEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Matches(Regex regex)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Matches(string regexExpression)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Matches(string regexExpression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Matches(Func<object, Regex> regexFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Matches(Func<object, string> regexExpressionFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Matches(Expression<Func<object, Regex>> expression)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Matches(Expression<Func<object, string>> expression)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Matches(Expression<Func<object, string>> expression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Func(Func<object, CustomVerifyResult> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder Func(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder Predicate(Predicate<object> predicate)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Must(Func<object, CustomVerifyResult> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder Must(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder Satisfies(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Satisfies(Func<object, bool> func, string message)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder Any(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder All(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder NotAny(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder NotAll(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder None(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder In(ICollection<object> collection)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder In(params object[] objects)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder NotIn(ICollection<object> collection)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder NotIn(params object[] objects)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder InEnum(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder InEnum<TEnum>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder IsEnumName(Type enumType, bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder IsEnumName<TEnum>(bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredType(Type type)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredString()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredBoolean()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredGuid()
        {
            throw new NotImplementedException();
        }
    }
}