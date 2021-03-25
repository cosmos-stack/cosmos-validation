using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Cosmos.Reflection;
using Cosmos.Validation;
using Cosmos.Validation.Objects;

#pragma warning disable 108,114

namespace CosmosValidationUT.Fakes
{
    public class FakeValueRuleBuilder<T, TVal> : FakeValueRuleBuilder<T>, IValueRuleBuilder<T, TVal>
    {
        public FakeValueRuleBuilder(VerifiableMemberContract contract) : base(contract) { }

        public IValueRuleBuilder<T, TVal> AppendRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> OverwriteRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> And()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Or()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Empty()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> NotEmpty()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Required()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Null()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> NotNull()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Range(TVal @from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal @from, TVal to)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal @from, TVal to)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Length(int min, int max)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> MinLength(int min)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> MaxLength(int max)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> AtLeast(int count)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Equal(TVal value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> NotEqual(TVal value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> LessThan(TVal value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> GreaterThan(TVal value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Regex regex)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(string regexExpression)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(string regexExpression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Func<T, Regex> regexFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Expression<Func<T, Regex>> expression)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Expression<Func<T, string>> expression)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Expression<Func<T, string>> expression, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Func<object, Regex> regexFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Func<object, string> regexExpressionFunc)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func)
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

        public IPredicateValueRuleBuilder<T, TVal> Must(Func<TVal, CustomVerifyResult> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func)
        {
            throw new NotImplementedException();
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Satisfies(Func<TVal, bool> func)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> Satisfies(Func<TVal, bool> func, string message)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> In(ICollection<TVal> collection)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> In(params TVal[] objects)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> NotIn(params TVal[] objects)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> InEnum(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> InEnum<TEnum>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> IsEnumName(Type enumType, bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> IsEnumName<TEnum>(bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredType(Type type)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredString()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredBoolean()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredGuid()
        {
            throw new NotImplementedException();
        }
    }
}