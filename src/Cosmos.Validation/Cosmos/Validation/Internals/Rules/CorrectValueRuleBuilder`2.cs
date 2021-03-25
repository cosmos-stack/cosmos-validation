using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRuleBuilder<T, TVal> : CorrectValueRuleBuilder<T>,
        IValueRuleBuilder<T, TVal>,
        IPredicateValueRuleBuilder<T, TVal>
    {
        public CorrectValueRuleBuilder(VerifiableMemberContract contract) : base(contract) { }

        public CorrectValueRuleBuilder(VerifiableMemberContract contract, ValueRuleMode mode) : base(contract, mode) { }

        #region Update Mode of ValueRule

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

        #endregion

        #region Condition

        public new IValueRuleBuilder<T, TVal> And()
        {
            State.MakeAndOps();
            return this;
        }

        public new IValueRuleBuilder<T, TVal> Or()
        {
            State.MakeOrOps();
            return this;
        }

        #endregion

        #region Activation Conditions

        public IValueRuleBuilder<T, TVal> When(Func<TVal, bool> condition)
        {
            if (condition is not null)
            {
                State.CurrentToken.ActivationConditions2 = o => condition.Invoke((TVal) o);
                State.CurrentToken.WithActivationConditions = true;
            }

            return this;
        }

        public IValueRuleBuilder<T, TVal> When(Func<T, TVal, bool> condition)
        {
            if (condition is not null)
            {
                State.CurrentToken.ActivationConditions3 = (o, v) => condition.Invoke((T) o, (TVal) v);
                State.CurrentToken.WithActivationConditions = true;
            }

            return this;
        }

        public IValueRuleBuilder<T, TVal> Unless(Func<TVal, bool> condition)
        {
            if (condition is not null)
            {
                State.CurrentToken.ActivationConditions2 = o => condition.Invoke((TVal) o);
                State.CurrentToken.WithActivationConditions = true;
            }

            return this;
        }

        public IValueRuleBuilder<T, TVal> Unless(Func<T, TVal, bool> condition)
        {
            if (condition is not null)
            {
                State.CurrentToken.ActivationConditions3 = (o, v) => condition.Invoke((T) o, (TVal) v);
                State.CurrentToken.WithActivationConditions = true;
            }

            return this;
        }

        #endregion

        #region Rules

        public new IPredicateValueRuleBuilder<T, TVal> Empty()
        {
            State.CurrentToken = new ValueEmptyToken(_contract);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> NotEmpty()
        {
            State.CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> Required()
        {
            State.CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> Null()
        {
            State.CurrentToken = new ValueNullToken(_contract);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> NotNull()
        {
            State.CurrentToken = new ValueNotNullToken(_contract);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            State.CurrentToken = new ValueRangeToken<TVal>(_contract, from, to, options);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal from, TVal to)
        {
            State.CurrentToken = new ValueRangeToken<TVal>(_contract, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal from, TVal to)
        {
            State.CurrentToken = new ValueRangeToken<TVal>(_contract, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> Length(int min, int max)
        {
            State.CurrentToken = new ValueLengthLimitedToken(_contract, min, max);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> MinLength(int min)
        {
            State.CurrentToken = new ValueMinLengthLimitedToken(_contract, min);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> MaxLength(int max)
        {
            State.CurrentToken = new ValueMaxLengthLimitedToken(_contract, max);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> AtLeast(int count)
        {
            State.CurrentToken = new ValueMinLengthLimitedToken(_contract, count);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> Equal(TVal value)
        {
            State.CurrentToken = new ValueEqualToken<TVal>(_contract, value, null);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer)
        {
            State.CurrentToken = new ValueEqualToken<TVal>(_contract, value, comparer);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> NotEqual(TVal value)
        {
            State.CurrentToken = new ValueNotEqualToken<TVal>(_contract, value, null);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer)
        {
            State.CurrentToken = new ValueNotEqualToken<TVal>(_contract, value, comparer);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> LessThan(TVal value)
        {
            State.CurrentToken = new ValueLessThanToken(_contract, value);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value)
        {
            State.CurrentToken = new ValueLessThanOrEqualToken(_contract, value);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> GreaterThan(TVal value)
        {
            State.CurrentToken = new ValueGreaterThanToken(_contract, value);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value)
        {
            State.CurrentToken = new ValueGreaterThanOrEqualToken(_contract, value);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> Matches(Regex regex)
        {
            State.CurrentToken = new ValueRegularExpressionToken(_contract, regex);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> Matches(string regexExpression)
        {
            State.CurrentToken = new ValueRegularExpressionToken(_contract, regexExpression);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> Matches(string regexExpression, RegexOptions options)
        {
            State.CurrentToken = new ValueRegularExpressionToken(_contract, regexExpression, options);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> Matches(Func<T, Regex> regexFunc)
        {
            State.CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexFunc);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc)
        {
            State.CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpressionFunc);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc, RegexOptions options)
        {
            State.CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpressionFunc, options);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func)
        {
            State.CurrentToken = new ValueFuncToken<TVal>(_contract, func);
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

        public IPredicateValueRuleBuilder<T, TVal> Must(Func<TVal, CustomVerifyResult> func)
        {
            State.CurrentToken = new ValueFuncToken<TVal>(_contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, func);
        }

        public IWaitForMessageValueRuleBuilder<T, TVal> Satisfies(Func<TVal, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T, TVal>(this, func);
        }

        public IPredicateValueRuleBuilder<T, TVal> Satisfies(Func<TVal, bool> func, string message)
        {
            return Satisfies(func).WithMessage(message);
        }

        public IPredicateValueRuleBuilder<T, TVal> In(ICollection<TVal> collection)
        {
            State.CurrentToken = new ValueInToken<TVal>(_contract, collection);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> In(params TVal[] objects)
        {
            State.CurrentToken = new ValueInToken<TVal>(_contract, objects);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection)
        {
            State.CurrentToken = new ValueNotInToken<TVal>(_contract, collection);
            return this;
        }

        public IPredicateValueRuleBuilder<T, TVal> NotIn(params TVal[] objects)
        {
            State.CurrentToken = new ValueInToken<TVal>(_contract, objects);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> InEnum(Type enumType)
        {
            State.CurrentToken = new ValueEnumToken(_contract, enumType);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> InEnum<TEnum>()
        {
            State.CurrentToken = new ValueEnumToken<TEnum>(_contract);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> IsEnumName(Type enumType, bool caseSensitive)
        {
            State.CurrentToken = new ValueStringEnumToken(_contract, enumType, caseSensitive);
            return this;
        }

        public new IPredicateValueRuleBuilder<T, TVal> IsEnumName<TEnum>(bool caseSensitive)
        {
            State.CurrentToken = new ValueStringEnumToken<TEnum>(_contract, caseSensitive);
            return this;
        }

        /// <summary>
        /// Limit the scale and precision of the value.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="ignoreTrailingZeros"></param>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            State.CurrentToken = new ValueScalePrecisionToken(_contract, scale, precision, ignoreTrailingZeros);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> RequiredType(Type type)
        {
            State.CurrentToken = new ValueRequiredTypeToken(_contract, type);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes(params Type[] types)
        {
            State.CurrentToken = new ValueRequiredTypesToken(_contract, types);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1>()
        {
            State.CurrentToken = new ValueRequiredTypeToken<T1>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2>(_contract);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(_contract);
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
        public new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(_contract);
            return this;
        }

        /// <summary>
        /// The constraint type must be of string type.
        /// </summary>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> RequiredString()
        {
            State.CurrentToken = new ValueRequiredStringToken(_contract);
            return this;
        }

        /// <summary>
        /// The constraint type must be of numeric type.
        /// </summary>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            State.CurrentToken = new ValueRequiredNumericToken(_contract, isOptions);
            return this;
        }

        /// <summary>
        /// The constraint type must be of boolean type.
        /// </summary>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> RequiredBoolean()
        {
            State.CurrentToken = new ValueRequiredBooleanToken(_contract);
            return this;
        }

        /// <summary>
        /// The constraint type must be of Guid type.
        /// </summary>
        /// <returns></returns>
        public new IPredicateValueRuleBuilder<T, TVal> RequiredGuid()
        {
            State.CurrentToken = new ValueRequiredGuidToken(_contract);
            return this;
        }

        #endregion
    }
}