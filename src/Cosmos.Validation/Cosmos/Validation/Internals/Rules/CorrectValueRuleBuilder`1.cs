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
    internal class CorrectValueRuleBuilder<T> : IValueRuleBuilder<T>
    {
        internal readonly VerifiableMemberContract _contract;

        public CorrectValueRuleState State { get; protected set; }

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

        public IValueRuleBuilder<T> AppendRule()
        {
            Mode = CorrectValueRuleMode.Append;
            return this;
        }

        public IValueRuleBuilder<T> OverwriteRule()
        {
            Mode = CorrectValueRuleMode.Overwrite;
            return this;
        }

        #endregion

        #region Condition

        public IValueRuleBuilder<T> And()
        {
            State.MakeAndOps();
            return this;
        }

        public IValueRuleBuilder<T> Or()
        {
            State.MakeOrOps();
            return this;
        }

        #endregion

        #region Rules

        public IValueRuleBuilder<T> Empty()
        {
            State.CurrentToken = new ValueEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> NotEmpty()
        {
            State.CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> Required()
        {
            State.CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> Null()
        {
            State.CurrentToken = new ValueNullToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> NotNull()
        {
            State.CurrentToken = new ValueNotNullToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            State.CurrentToken = new ValueRangeToken(_contract, from, to, options);
            return this;
        }

        public IValueRuleBuilder<T> RangeWithOpenInterval(object from, object to)
        {
            State.CurrentToken = new ValueRangeToken(_contract, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public IValueRuleBuilder<T> RangeWithCloseInterval(object from, object to)
        {
            State.CurrentToken = new ValueRangeToken(_contract, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public IValueRuleBuilder<T> Length(int min, int max)
        {
            State.CurrentToken = new ValueLengthLimitedToken(_contract, min, max);
            return this;
        }

        public IValueRuleBuilder<T> MinLength(int min)
        {
            State.CurrentToken = new ValueMinLengthLimitedToken(_contract, min);
            return this;
        }

        public IValueRuleBuilder<T> MaxLength(int max)
        {
            State.CurrentToken = new ValueMaxLengthLimitedToken(_contract, max);
            return this;
        }

        public IValueRuleBuilder<T> AtLeast(int count)
        {
            State.CurrentToken = new ValueMinLengthLimitedToken(_contract, count);
            return this;
        }

        public IValueRuleBuilder<T> Equal(object value)
        {
            State.CurrentToken = new ValueEqualToken(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer)
        {
            State.CurrentToken = new ValueEqualToken(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T> NotEqual(object value)
        {
            State.CurrentToken = new ValueNotEqualToken(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer)
        {
            State.CurrentToken = new ValueNotEqualToken(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T> LessThan(object value)
        {
            State.CurrentToken = new ValueLessThanToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T> LessThanOrEqual(object value)
        {
            State.CurrentToken = new ValueLessThanOrEqualToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T> GreaterThan(object value)
        {
            State.CurrentToken = new ValueGreaterThanToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T> GreaterThanOrEqual(object value)
        {
            State.CurrentToken = new ValueGreaterThanOrEqualToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T> Matches(Regex regex)
        {
            State.CurrentToken = new ValueRegularExpressionToken<T>(_contract, regex);
            return this;
        }

        public IValueRuleBuilder<T> Matches(string regexExpression)
        {
            State.CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpression);
            return this;
        }

        public IValueRuleBuilder<T> Matches(string regexExpression, RegexOptions options)
        {
            State.CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpression, options);
            return this;
        }

        public IValueRuleBuilder<T> Matches(Func<T, Regex> regexFunc)
        {
            State.CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexFunc);
            return this;
        }

        public IValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc)
        {
            State.CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpressionFunc);
            return this;
        }

        public IValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc, RegexOptions options)
        {
            State.CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpressionFunc, options);
            return this;
        }

        public IValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func)
        {
            State.CurrentToken = new ValueFuncToken(_contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder<T> Func(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T>(this, func);
        }

        public IWaitForMessageValueRuleBuilder<T> Predicate(Predicate<object> predicate)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T>(this, predicate);
        }

        public IValueRuleBuilder<T> Must(Func<object, CustomVerifyResult> func)
        {
            State.CurrentToken = new ValueFuncToken(_contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T>(this, func);
        }

        public IWaitForMessageValueRuleBuilder<T> Satisfies(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T>(this, func);
        }

        public IValueRuleBuilder<T> Satisfies(Func<object, bool> func, string message)
        {
            return Satisfies(func).WithMessage(message);
        }

        // public IValueRuleBuilder<T> Any(Func<object, bool> func)
        // {
        //     State.CurrentToken = new ValueAnyToken(Contract, func);
        //     return this;
        // }
        //
        // public IValueRuleBuilder<T> All(Func<object, bool> func)
        // {
        //     State.CurrentToken = new ValueAllToken(Contract, func);
        //     return this;
        // }
        //
        // public IValueRuleBuilder<T> NotAny(Func<object, bool> func)
        // {
        //     State.CurrentToken = new ValueAllToken(Contract, func);
        //     return this;
        // }
        //
        // public IValueRuleBuilder<T> NotAll(Func<object, bool> func)
        // {
        //     State.CurrentToken = new ValueAnyToken(Contract, func);
        //     return this;
        // }

        public IValueRuleBuilder<T> In(ICollection<object> collection)
        {
            State.CurrentToken = new ValueInToken(_contract, collection);
            return this;
        }

        public IValueRuleBuilder<T> In(params object[] objects)
        {
            State.CurrentToken = new ValueInToken(_contract, objects);
            return this;
        }

        public IValueRuleBuilder<T> NotIn(ICollection<object> collection)
        {
            State.CurrentToken = new ValueNotInToken(_contract, collection);
            return this;
        }

        public IValueRuleBuilder<T> NotIn(params object[] objects)
        {
            State.CurrentToken = new ValueNotInToken(_contract, objects);
            return this;
        }

        public IValueRuleBuilder<T> InEnum(Type enumType)
        {
            State.CurrentToken = new ValueEnumToken(_contract, enumType);
            return this;
        }

        public IValueRuleBuilder<T> InEnum<TEnum>()
        {
            State.CurrentToken = new ValueEnumToken<TEnum>(_contract);
            return this;
        }

        public IValueRuleBuilder<T> IsEnumName(Type enumType, bool caseSensitive)
        {
            State.CurrentToken = new ValueStringEnumToken(_contract, enumType, caseSensitive);
            return this;
        }

        public IValueRuleBuilder<T> IsEnumName<TEnum>(bool caseSensitive)
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
        public IValueRuleBuilder<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            State.CurrentToken = new ValueScalePrecisionToken(_contract, scale, precision, ignoreTrailingZeros);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredType(Type type)
        {
            State.CurrentToken = new ValueRequiredTypeToken(_contract, type);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredTypes(params Type[] types)
        {
            State.CurrentToken = new ValueRequiredTypesToken(_contract, types);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredTypes<T1>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(_contract);
            return this;
        }

        /// <summary>
        /// The constraint type must be of string type.
        /// </summary>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredString()
        {
            State.CurrentToken = new ValueRequiredStringToken(_contract);
            return this;
        }

        /// <summary>
        /// The constraint type must be of numeric type.
        /// </summary>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            State.CurrentToken = new ValueRequiredNumericToken(_contract, isOptions);
            return this;
        }

        /// <summary>
        /// The constraint type must be of boolean type.
        /// </summary>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredBoolean()
        {
            State.CurrentToken = new ValueRequiredBooleanToken(_contract);
            return this;
        }

        /// <summary>
        /// The constraint type must be of Guid type.
        /// </summary>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredGuid()
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