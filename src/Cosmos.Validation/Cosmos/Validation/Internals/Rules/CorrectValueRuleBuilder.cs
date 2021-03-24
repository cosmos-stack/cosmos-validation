using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Conditions;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRuleBuilder : IValueRuleBuilder
    {
        internal readonly VerifiableMemberContract _contract;

        public CorrectValueRuleState State { get; private set; }

        public CorrectValueRuleBuilder(VerifiableMemberContract contract)
        {
            _contract = contract;
            State = new CorrectValueRuleState(contract);
        }

        public CorrectValueRuleBuilder(VerifiableMemberContract contract, ValueRuleMode mode)
        {
            _contract = contract;
            State = new CorrectValueRuleState(contract);
            Mode = mode.X();
        }

        public string MemberName => _contract.MemberName;

        public CorrectValueRuleMode Mode { get; set; } = CorrectValueRuleMode.Append;

        #region Update Mode of ValueRule

        public IValueRuleBuilder AppendRule()
        {
            Mode = CorrectValueRuleMode.Append;
            return this;
        }

        public IValueRuleBuilder OverwriteRule()
        {
            Mode = CorrectValueRuleMode.Overwrite;
            return this;
        }

        #endregion

        #region Condition

        public IValueRuleBuilder And()
        {
            State.MakeAndOps();
            return this;
        }

        public IValueRuleBuilder Or()
        {
            State.MakeOrOps();
            return this;
        }

        #endregion

        #region Rules

        public IValueRuleBuilder Empty()
        {
            State.CurrentToken = new ValueEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder NotEmpty()
        {
            State.CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder Required()
        {
            State.CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder Null()
        {
            State.CurrentToken = new ValueNullToken(_contract);
            return this;
        }

        public IValueRuleBuilder NotNull()
        {
            State.CurrentToken = new ValueNotNullToken(_contract);
            return this;
        }

        public IValueRuleBuilder Length(int min, int max)
        {
            State.CurrentToken = new ValueLengthLimitedToken(_contract, min, max);
            return this;
        }

        public IValueRuleBuilder Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            State.CurrentToken = new ValueRangeToken(_contract, from, to, options);
            return this;
        }

        public IValueRuleBuilder RangeWithOpenInterval(object from, object to)
        {
            State.CurrentToken = new ValueRangeToken(_contract, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public IValueRuleBuilder RangeWithCloseInterval(object from, object to)
        {
            State.CurrentToken = new ValueRangeToken(_contract, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public IValueRuleBuilder MinLength(int min)
        {
            State.CurrentToken = new ValueMinLengthLimitedToken(_contract, min);
            return this;
        }

        public IValueRuleBuilder MaxLength(int max)
        {
            State.CurrentToken = new ValueMaxLengthLimitedToken(_contract, max);
            return this;
        }

        public IValueRuleBuilder AtLeast(int count)
        {
            State.CurrentToken = new ValueMinLengthLimitedToken(_contract, count);
            return this;
        }

        public IValueRuleBuilder Equal(object value)
        {
            State.CurrentToken = new ValueEqualToken(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder Equal(object value, IEqualityComparer comparer)
        {
            State.CurrentToken = new ValueEqualToken(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder NotEqual(object value)
        {
            State.CurrentToken = new ValueNotEqualToken(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder NotEqual(object value, IEqualityComparer comparer)
        {
            State.CurrentToken = new ValueNotEqualToken(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder LessThan(object value)
        {
            State.CurrentToken = new ValueLessThanToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder LessThanOrEqual(object value)
        {
            State.CurrentToken = new ValueLessThanOrEqualToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder GreaterThan(object value)
        {
            State.CurrentToken = new ValueGreaterThanToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder GreaterThanOrEqual(object value)
        {
            State.CurrentToken = new ValueGreaterThanOrEqualToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder Matches(Regex regex)
        {
            State.CurrentToken = new ValueRegularExpressionToken(_contract, regex);
            return this;
        }

        public IValueRuleBuilder Matches(string regexExpression)
        {
            State.CurrentToken = new ValueRegularExpressionToken(_contract, regexExpression);
            return this;
        }

        public IValueRuleBuilder Matches(string regexExpression, RegexOptions options)
        {
            State.CurrentToken = new ValueRegularExpressionToken(_contract, regexExpression, options);
            return this;
        }

        public IValueRuleBuilder Matches(Func<object, Regex> regexFunc)
        {
            State.CurrentToken = new ValueRegularExpressionToken(_contract, regexFunc);
            return this;
        }

        public IValueRuleBuilder Matches(Func<object, string> regexExpressionFunc)
        {
            State.CurrentToken = new ValueRegularExpressionToken(_contract, regexExpressionFunc);
            return this;
        }

        public IValueRuleBuilder Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            State.CurrentToken = new ValueRegularExpressionToken(_contract, regexExpressionFunc, options);
            return this;
        }

        public IValueRuleBuilder Func(Func<object, CustomVerifyResult> func)
        {
            State.CurrentToken = new ValueFuncToken(_contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder Func(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder(this, func);
        }

        public IWaitForMessageValueRuleBuilder Predicate(Predicate<object> predicate)
        {
            return new CorrectWaitForMessageValueRuleBuilder(this, predicate);
        }

        public IValueRuleBuilder Must(Func<object, CustomVerifyResult> func)
        {
            State.CurrentToken = new ValueFuncToken(_contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder Must(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder(this, func);
        }

        public IWaitForMessageValueRuleBuilder Satisfies(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder(this, func);
        }

        public IValueRuleBuilder Satisfies(Func<object, bool> func, string message)
        {
            return Satisfies(func).WithMessage(message);
        }

        public IValueRuleBuilder Any(Func<object, bool> func)
        {
            State.CurrentToken = new ValueAnyToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder All(Func<object, bool> func)
        {
            State.CurrentToken = new ValueAllToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder NotAny(Func<object, bool> func)
        {
            State.CurrentToken = new ValueAllToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder NotAll(Func<object, bool> func)
        {
            State.CurrentToken = new ValueAnyToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder None(Func<object, bool> func)
        {
            State.CurrentToken = new ValueNoneToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder In(ICollection<object> collection)
        {
            State.CurrentToken = new ValueInToken(_contract, collection);
            return this;
        }

        public IValueRuleBuilder In(params object[] objects)
        {
            State.CurrentToken = new ValueInToken(_contract, objects);
            return this;
        }

        public IValueRuleBuilder NotIn(ICollection<object> collection)
        {
            State.CurrentToken = new ValueNotInToken(_contract, collection);
            return this;
        }

        public IValueRuleBuilder NotIn(params object[] objects)
        {
            State.CurrentToken = new ValueNotInToken(_contract, objects);
            return this;
        }

        public IValueRuleBuilder InEnum(Type enumType)
        {
            State.CurrentToken = new ValueEnumToken(_contract, enumType);
            return this;
        }

        public IValueRuleBuilder InEnum<TEnum>()
        {
            State.CurrentToken = new ValueEnumToken<TEnum>(_contract);
            return this;
        }

        public IValueRuleBuilder IsEnumName(Type enumType, bool caseSensitive)
        {
            State.CurrentToken = new ValueStringEnumToken(_contract, enumType, caseSensitive);
            return this;
        }

        public IValueRuleBuilder IsEnumName<TEnum>(bool caseSensitive)
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
        public IValueRuleBuilder ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            State.CurrentToken = new ValueScalePrecisionToken(_contract, scale, precision, ignoreTrailingZeros);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IValueRuleBuilder RequiredType(Type type)
        {
            State.CurrentToken = new ValueRequiredTypeToken(_contract, type);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IValueRuleBuilder RequiredTypes(params Type[] types)
        {
            State.CurrentToken = new ValueRequiredTypesToken(_contract, types);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public IValueRuleBuilder RequiredTypes<T1>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
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
        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(_contract);
            return this;
        }

        /// <summary>
        /// The constraint type must be of string type.
        /// </summary>
        /// <returns></returns>
        public IValueRuleBuilder RequiredString()
        {
            State.CurrentToken = new ValueRequiredStringToken(_contract);
            return this;
        }

        /// <summary>
        /// The constraint type must be of numeric type.
        /// </summary>
        /// <returns></returns>
        public IValueRuleBuilder RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            State.CurrentToken = new ValueRequiredNumericToken(_contract, isOptions);
            return this;
        }

        /// <summary>
        /// The constraint type must be of boolean type.
        /// </summary>
        /// <returns></returns>
        public IValueRuleBuilder RequiredBoolean()
        {
            State.CurrentToken = new ValueRequiredBooleanToken(_contract);
            return this;
        }

        /// <summary>
        /// The constraint type must be of Guid type.
        /// </summary>
        /// <returns></returns>
        public IValueRuleBuilder RequiredGuid()
        {
            State.CurrentToken = new ValueRequiredGuidToken(_contract);
            return this;
        }

        #endregion

        public CorrectValueRule Build()
        {
            var tokens = State.ExposeValueTokens(out var topOps);

            CorrectValueRule result;

            if (topOps == ConditionOps.Break)
            {
                result = new CorrectValueRule()
                {
                    MemberName = MemberName,
                    Contract = _contract,
                    Mode = Mode,
                    Tokens = tokens
                };
            }
            else
            {
                result = new LogicCorrectValueRule()
                {
                    MemberName = MemberName,
                    Contract = _contract,
                    Mode = Mode,
                    Tokens = tokens,
                    InternalLogic = topOps != ConditionOps.Or
                };
            }

            return result;
        }
    }
}