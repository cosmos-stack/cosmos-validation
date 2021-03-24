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

        public IValueRuleBuilder Empty()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder NotEmpty()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Required()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Null()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder NotNull()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Range(object @from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RangeWithOpenInterval(object @from, object to)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RangeWithCloseInterval(object @from, object to)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Length(int min, int max)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder MinLength(int min)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder MaxLength(int max)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder AtLeast(int count)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Equal(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Equal(object value, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder NotEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder NotEqual(object value, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder LessThan(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder LessThanOrEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder GreaterThan(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder GreaterThanOrEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Matches(Regex regex)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Matches(string regexExpression)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Matches(string regexExpression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Matches(Func<object, Regex> regexFunc)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Matches(Func<object, string> regexExpressionFunc)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Matches(Expression<Func<object, Regex>> expression)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Matches(Expression<Func<object, string>> expression)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Matches(Expression<Func<object, string>> expression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Func(Func<object, CustomVerifyResult> func)
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

        public IValueRuleBuilder Must(Func<object, CustomVerifyResult> func)
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

        public IValueRuleBuilder Satisfies(Func<object, bool> func, string message)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Any(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder All(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder NotAny(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder NotAll(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder None(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder In(ICollection<object> collection)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder In(params object[] objects)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder NotIn(ICollection<object> collection)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder NotIn(params object[] objects)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder InEnum(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder InEnum<TEnum>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder IsEnumName(Type enumType, bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder IsEnumName<TEnum>(bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredType(Type type)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredString()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredBoolean()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder RequiredGuid()
        {
            throw new NotImplementedException();
        }
    }
}