using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Cosmos.Validation;
using Cosmos.Validation.Objects;

namespace CosmosValidationUT.Fakes
{
    public class FakeValueRuleBuilder<T> : IValueRuleBuilder<T>
    {
        internal readonly VerifiableMemberContract Contract;

        public FakeValueRuleBuilder(VerifiableMemberContract contract)
        {
            Contract = contract;
        }

        public IValueRuleBuilder<T> AppendRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> OverwriteRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> And()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Or()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Empty()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> NotEmpty()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Required()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Null()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> NotNull()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Range(object @from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RangeWithOpenInterval(object @from, object to)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RangeWithCloseInterval(object @from, object to)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Length(int min, int max)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> MinLength(int min)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> MaxLength(int max)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> AtLeast(int count)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Equal(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> NotEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> LessThan(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> LessThanOrEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> GreaterThan(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> GreaterThanOrEqual(object value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Regex regex)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(string regexExpression)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(string regexExpression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Func<T, Regex> regexFunc)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Expression<Func<T, Regex>> expression)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Expression<Func<T, string>> expression)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Expression<Func<T, string>> expression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Func<object, Regex> regexFunc)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Func<object, string> regexExpressionFunc)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T> Func(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T> Predicate(Predicate<object> predicate)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Must(Func<object, CustomVerifyResult> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> In(ICollection<object> collection)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> In(params object[] objects)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> NotIn(ICollection<object> collection)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> NotIn(params object[] objects)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> InEnum(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> InEnum<TEnum>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> IsEnumName(Type enumType, bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> IsEnumName<TEnum>(bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredType(Type type)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            throw new NotImplementedException();
        }
    }
}