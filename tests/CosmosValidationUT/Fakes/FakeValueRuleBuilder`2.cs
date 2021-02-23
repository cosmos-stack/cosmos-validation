using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cosmos.Validation;
using Cosmos.Validation.Objects;
#pragma warning disable 108,114

namespace CosmosValidationUT.Fakes
{
    public class FakeValueRuleBuilder<T, TVal> : FakeValueRuleBuilder<T>, IValueRuleBuilder<T, TVal>
    {
        public FakeValueRuleBuilder(ObjectValueContract contract) : base(contract) { }
        public IValueRuleBuilder<T, TVal> AppendRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> OverwriteRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Empty()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> NotEmpty()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Required()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Null()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> NotNull()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Range(TVal @from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal @from, TVal to)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal @from, TVal to)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Length(int min, int max)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> MinLength(int min)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> MaxLength(int max)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> AtLeast(int count)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Equal(TVal value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> NotEqual(TVal value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> LessThan(TVal value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> GreaterThan(TVal value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Matches(Regex regex)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Matches(string regexExpression)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Matches(string regexExpression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Matches(Func<object, Regex> regexFunc)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Matches(Func<object, string> regexExpressionFunc)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Func(Func<TVal, bool> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Predicate(Predicate<TVal> predicate)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Must(Func<TVal, CustomVerifyResult> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> In(ICollection<TVal> collection)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> In(params TVal[] objects)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> NotIn(params TVal[] objects)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> InEnum(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> InEnum<TEnum>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> IsEnumName(Type enumType, bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> IsEnumName<TEnum>(bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredType(Type type)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            throw new NotImplementedException();
        }
    }
}