using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRuleBuilder<T, TVal> : CorrectValueRuleBuilder<T>, IValueRuleBuilder<T, TVal>
    {
        public CorrectValueRuleBuilder(VerifiableMemberContract contract) : base(contract) { }

        public CorrectValueRuleBuilder(VerifiableMemberContract contract, ValueRuleMode mode) : base(contract, mode) { }

        public new IValueRuleBuilder<T, TVal> AppendRule()
        {
            Mode = CorrectValueRuleMode.Append;
            return this;
        }

        public new IValueRuleBuilder<T, TVal> OverwriteRule()
        {
            Mode = CorrectValueRuleMode.Overwrite;
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Empty()
        {
            CurrentToken = new ValueEmptyToken(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> NotEmpty()
        {
            CurrentToken = new ValueNotEmptyToken(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Required()
        {
            CurrentToken = new ValueNotEmptyToken(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Null()
        {
            CurrentToken = new ValueNullToken(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> NotNull()
        {
            CurrentToken = new ValueNotNullToken(Contract);
            return this;
        }

        public IValueRuleBuilder<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            CurrentToken = new ValueRangeToken<TVal>(Contract, from, to, options);
            return this;
        }

        public IValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal from, TVal to)
        {
            CurrentToken = new ValueRangeToken<TVal>(Contract, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public IValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal from, TVal to)
        {
            CurrentToken = new ValueRangeToken<TVal>(Contract, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Length(int min, int max)
        {
            CurrentToken = new ValueLengthLimitedToken(Contract, min, max);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> MinLength(int min)
        {
            CurrentToken = new ValueMinLengthLimitedToken(Contract, min);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> MaxLength(int max)
        {
            CurrentToken = new ValueMaxLengthLimitedToken(Contract, max);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> AtLeast(int count)
        {
            CurrentToken = new ValueMinLengthLimitedToken(Contract, count);
            return this;
        }

        public IValueRuleBuilder<T, TVal> Equal(TVal value)
        {
            CurrentToken = new ValueEqualToken<TVal>(Contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer)
        {
            CurrentToken = new ValueEqualToken<TVal>(Contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T, TVal> NotEqual(TVal value)
        {
            CurrentToken = new ValueNotEqualToken<TVal>(Contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer)
        {
            CurrentToken = new ValueNotEqualToken<TVal>(Contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T, TVal> LessThan(TVal value)
        {
            CurrentToken = new ValueLessThanToken<TVal>(Contract, value);
            return this;
        }

        public IValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value)
        {
            CurrentToken = new ValueLessThanOrEqualToken<TVal>(Contract, value);
            return this;
        }

        public IValueRuleBuilder<T, TVal> GreaterThan(TVal value)
        {
            CurrentToken = new ValueGreaterThanToken<TVal>(Contract, value);
            return this;
        }

        public IValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value)
        {
            CurrentToken = new ValueGreaterThanOrEqualToken<TVal>(Contract, value);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Matches(Regex regex)
        {
            CurrentToken = new ValueRegularExpressionToken(Contract, regex);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Matches(string regexExpression)
        {
            CurrentToken = new ValueRegularExpressionToken(Contract, regexExpression);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Matches(string regexExpression, RegexOptions options)
        {
            CurrentToken = new ValueRegularExpressionToken(Contract, regexExpression, options);
            return this;
        }

        // public new IValueRuleBuilder<T, TVal> Matches(Func<object, Regex> regexFunc)
        // {
        //     CurrentToken = new ValueRegularExpressionToken(Contract, regexFunc);
        //     return this;
        // }
        //
        // public new IValueRuleBuilder<T, TVal> Matches(Func<object, string> regexExpressionFunc)
        // {
        //     CurrentToken = new ValueRegularExpressionToken(Contract, regexExpressionFunc);
        //     return this;
        // }
        //
        // public new IValueRuleBuilder<T, TVal> Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        // {
        //     CurrentToken = new ValueRegularExpressionToken(Contract, regexExpressionFunc, options);
        //     return this;
        // }

        public IValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func)
        {
            CurrentToken = new ValueFuncToken<TVal>(Contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Func(Func<TVal, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, func);
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Predicate(Predicate<TVal> predicate)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, predicate);
        }

        public IValueRuleBuilder<T, TVal> Must(Func<TVal, CustomVerifyResult> func)
        {
            CurrentToken = new ValueFuncToken<TVal>(Contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, func);
        }

        public IValueRuleBuilder<T, TVal> In(ICollection<TVal> collection)
        {
            CurrentToken = new ValueInToken<TVal>(Contract, collection);
            return this;
        }

        public IValueRuleBuilder<T, TVal> In(params TVal[] objects)
        {
            CurrentToken = new ValueInToken<TVal>(Contract, objects);
            return this;
        }

        public IValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection)
        {
            CurrentToken = new ValueNotInToken<TVal>(Contract, collection);
            return this;
        }

        public IValueRuleBuilder<T, TVal> NotIn(params TVal[] objects)
        {
            CurrentToken = new ValueInToken<TVal>(Contract, objects);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> InEnum(Type enumType)
        {
            CurrentToken = new ValueEnumToken(Contract, enumType);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> InEnum<TEnum>()
        {
            CurrentToken = new ValueEnumToken<TEnum>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> IsEnumName(Type enumType, bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken(Contract, enumType, caseSensitive);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> IsEnumName<TEnum>(bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken<TEnum>(Contract, caseSensitive);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            CurrentToken = new ValueScalePrecisionToken(Contract, scale, precision, ignoreTrailingZeros);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredType(Type type)
        {
            CurrentToken = new ValueRequiredTypeToken(Contract, type);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes(params Type[] types)
        {
            CurrentToken = new ValueRequiredTypesToken(Contract, types);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1>()
        {
            CurrentToken = new ValueRequiredTypeToken<T1>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Contract);
            return this;
        }

        public new CorrectValueRule<TVal> Build()
        {
            ClearCurrentToken();

            return new CorrectValueRule<TVal>
            {
                MemberName = MemberName,
                Mode = Mode,
                Tokens = _valueTokens,
            };
        }
    }
}