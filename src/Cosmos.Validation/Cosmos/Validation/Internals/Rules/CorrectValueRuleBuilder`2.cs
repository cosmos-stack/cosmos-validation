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
            CurrentToken = new ValueEmptyToken(_contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> NotEmpty()
        {
            CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Required()
        {
            CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Null()
        {
            CurrentToken = new ValueNullToken(_contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> NotNull()
        {
            CurrentToken = new ValueNotNullToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            CurrentToken = new ValueRangeToken<TVal>(_contract, from, to, options);
            return this;
        }

        public IValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal from, TVal to)
        {
            CurrentToken = new ValueRangeToken<TVal>(_contract, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public IValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal from, TVal to)
        {
            CurrentToken = new ValueRangeToken<TVal>(_contract, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Length(int min, int max)
        {
            CurrentToken = new ValueLengthLimitedToken(_contract, min, max);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> MinLength(int min)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_contract, min);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> MaxLength(int max)
        {
            CurrentToken = new ValueMaxLengthLimitedToken(_contract, max);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> AtLeast(int count)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_contract, count);
            return this;
        }

        public IValueRuleBuilder<T, TVal> Equal(TVal value)
        {
            CurrentToken = new ValueEqualToken<TVal>(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer)
        {
            CurrentToken = new ValueEqualToken<TVal>(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T, TVal> NotEqual(TVal value)
        {
            CurrentToken = new ValueNotEqualToken<TVal>(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer)
        {
            CurrentToken = new ValueNotEqualToken<TVal>(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T, TVal> LessThan(TVal value)
        {
            CurrentToken = new ValueLessThanToken<TVal>(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value)
        {
            CurrentToken = new ValueLessThanOrEqualToken<TVal>(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T, TVal> GreaterThan(TVal value)
        {
            CurrentToken = new ValueGreaterThanToken<TVal>(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value)
        {
            CurrentToken = new ValueGreaterThanOrEqualToken<TVal>(_contract, value);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Matches(Regex regex)
        {
            CurrentToken = new ValueRegularExpressionToken(_contract, regex);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Matches(string regexExpression)
        {
            CurrentToken = new ValueRegularExpressionToken(_contract, regexExpression);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Matches(string regexExpression, RegexOptions options)
        {
            CurrentToken = new ValueRegularExpressionToken(_contract, regexExpression, options);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Matches(Func<T, Regex> regexFunc)
        {
            CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexFunc);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc)
        {
            CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpressionFunc);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc, RegexOptions options)
        {
            CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpressionFunc, options);
            return this;
        }

        public IValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func)
        {
            CurrentToken = new ValueFuncToken<TVal>(_contract, func);
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
            CurrentToken = new ValueFuncToken<TVal>(_contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, func);
        }

        public IValueRuleBuilder<T, TVal> In(ICollection<TVal> collection)
        {
            CurrentToken = new ValueInToken<TVal>(_contract, collection);
            return this;
        }

        public IValueRuleBuilder<T, TVal> In(params TVal[] objects)
        {
            CurrentToken = new ValueInToken<TVal>(_contract, objects);
            return this;
        }

        public IValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection)
        {
            CurrentToken = new ValueNotInToken<TVal>(_contract, collection);
            return this;
        }

        public IValueRuleBuilder<T, TVal> NotIn(params TVal[] objects)
        {
            CurrentToken = new ValueInToken<TVal>(_contract, objects);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> InEnum(Type enumType)
        {
            CurrentToken = new ValueEnumToken(_contract, enumType);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> InEnum<TEnum>()
        {
            CurrentToken = new ValueEnumToken<TEnum>(_contract);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> IsEnumName(Type enumType, bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken(_contract, enumType, caseSensitive);
            return this;
        }

        public new IValueRuleBuilder<T, TVal> IsEnumName<TEnum>(bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken<TEnum>(_contract, caseSensitive);
            return this;
        }

        /// <summary>
        /// Limit the scale and precision of the value.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="ignoreTrailingZeros"></param>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            CurrentToken = new ValueScalePrecisionToken(_contract, scale, precision, ignoreTrailingZeros);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredType(Type type)
        {
            CurrentToken = new ValueRequiredTypeToken(_contract, type);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes(params Type[] types)
        {
            CurrentToken = new ValueRequiredTypesToken(_contract, types);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1>()
        {
            CurrentToken = new ValueRequiredTypeToken<T1>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <typeparam name="T15"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <typeparam name="T15"></typeparam>
        /// <typeparam name="T16"></typeparam>
        /// <returns></returns>
        public new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(_contract);
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