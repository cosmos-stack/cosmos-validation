using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cosmos.Validation.Internals.Tokens;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRuleBuilder<T> : IValueRuleBuilder<T>
    {
        internal readonly VerifiableMemberContract _contract;

        protected readonly List<IValueToken> _valueTokens;

        private IValueToken _currentTokenPtr;

        internal IValueToken CurrentToken
        {
            get => _currentTokenPtr;
            set
            {
                if (value is not null)
                {
                    _currentTokenPtr = value;
                    _valueTokens.Add(value);
                }
            }
        }

        internal void ClearCurrentToken()
        {
            _currentTokenPtr = null;
        }

        public CorrectValueRuleBuilder(VerifiableMemberContract contract)
        {
            _contract = contract;
            _valueTokens = new List<IValueToken>();
        }

        public CorrectValueRuleBuilder(VerifiableMemberContract contract, ValueRuleMode mode)
        {
            _contract = contract;
            _valueTokens = new List<IValueToken>();
            Mode = mode.X();
        }

        public string MemberName => _contract.MemberName;

        public CorrectValueRuleMode Mode { get; set; } = CorrectValueRuleMode.Append;

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

        public IValueRuleBuilder<T> Empty()
        {
            CurrentToken = new ValueEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> NotEmpty()
        {
            CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> Required()
        {
            CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> Null()
        {
            CurrentToken = new ValueNullToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> NotNull()
        {
            CurrentToken = new ValueNotNullToken(_contract);
            return this;
        }

        public IValueRuleBuilder<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            CurrentToken = new ValueRangeToken(_contract, from, to, options);
            return this;
        }

        public IValueRuleBuilder<T> RangeWithOpenInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(_contract, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public IValueRuleBuilder<T> RangeWithCloseInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(_contract, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public IValueRuleBuilder<T> Length(int min, int max)
        {
            CurrentToken = new ValueLengthLimitedToken(_contract, min, max);
            return this;
        }

        public IValueRuleBuilder<T> MinLength(int min)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_contract, min);
            return this;
        }

        public IValueRuleBuilder<T> MaxLength(int max)
        {
            CurrentToken = new ValueMaxLengthLimitedToken(_contract, max);
            return this;
        }

        public IValueRuleBuilder<T> AtLeast(int count)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_contract, count);
            return this;
        }

        public IValueRuleBuilder<T> Equal(object value)
        {
            CurrentToken = new ValueEqualToken(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueEqualToken(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T> NotEqual(object value)
        {
            CurrentToken = new ValueNotEqualToken(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueNotEqualToken(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T> LessThan(object value)
        {
            CurrentToken = new ValueLessThanToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T> LessThanOrEqual(object value)
        {
            CurrentToken = new ValueLessThanOrEqualToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T> GreaterThan(object value)
        {
            CurrentToken = new ValueGreaterThanToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T> GreaterThanOrEqual(object value)
        {
            CurrentToken = new ValueGreaterThanOrEqualToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder<T> Matches(Regex regex)
        {
            CurrentToken = new ValueRegularExpressionToken<T>(_contract, regex);
            return this;
        }

        public IValueRuleBuilder<T> Matches(string regexExpression)
        {
            CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpression);
            return this;
        }

        public IValueRuleBuilder<T> Matches(string regexExpression, RegexOptions options)
        {
            CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpression, options);
            return this;
        }

        public IValueRuleBuilder<T> Matches(Func<T, Regex> regexFunc)
        {
            CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexFunc);
            return this;
        }

        public IValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc)
        {
            CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpressionFunc);
            return this;
        }

        public IValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc, RegexOptions options)
        {
            CurrentToken = new ValueRegularExpressionToken<T>(_contract, regexExpressionFunc, options);
            return this;
        }

        public IValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func)
        {
            CurrentToken = new ValueFuncToken(_contract, func);
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
            CurrentToken = new ValueFuncToken(_contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder<T>(this, func);
        }

        // public IValueRuleBuilder<T> Any(Func<object, bool> func)
        // {
        //     CurrentToken = new ValueAnyToken(Contract, func);
        //     return this;
        // }
        //
        // public IValueRuleBuilder<T> All(Func<object, bool> func)
        // {
        //     CurrentToken = new ValueAllToken(Contract, func);
        //     return this;
        // }
        //
        // public IValueRuleBuilder<T> NotAny(Func<object, bool> func)
        // {
        //     CurrentToken = new ValueAllToken(Contract, func);
        //     return this;
        // }
        //
        // public IValueRuleBuilder<T> NotAll(Func<object, bool> func)
        // {
        //     CurrentToken = new ValueAnyToken(Contract, func);
        //     return this;
        // }

        public IValueRuleBuilder<T> In(ICollection<object> collection)
        {
            CurrentToken = new ValueInToken(_contract, collection);
            return this;
        }

        public IValueRuleBuilder<T> In(params object[] objects)
        {
            CurrentToken = new ValueInToken(_contract, objects);
            return this;
        }

        public IValueRuleBuilder<T> NotIn(ICollection<object> collection)
        {
            CurrentToken = new ValueNotInToken(_contract, collection);
            return this;
        }

        public IValueRuleBuilder<T> NotIn(params object[] objects)
        {
            CurrentToken = new ValueNotInToken(_contract, objects);
            return this;
        }

        public IValueRuleBuilder<T> InEnum(Type enumType)
        {
            CurrentToken = new ValueEnumToken(_contract, enumType);
            return this;
        }

        public IValueRuleBuilder<T> InEnum<TEnum>()
        {
            CurrentToken = new ValueEnumToken<TEnum>(_contract);
            return this;
        }

        public IValueRuleBuilder<T> IsEnumName(Type enumType, bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken(_contract, enumType, caseSensitive);
            return this;
        }

        public IValueRuleBuilder<T> IsEnumName<TEnum>(bool caseSensitive)
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
        public IValueRuleBuilder<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            CurrentToken = new ValueScalePrecisionToken(_contract, scale, precision, ignoreTrailingZeros);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredType(Type type)
        {
            CurrentToken = new ValueRequiredTypeToken(_contract, type);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredTypes(params Type[] types)
        {
            CurrentToken = new ValueRequiredTypesToken(_contract, types);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public IValueRuleBuilder<T> RequiredTypes<T1>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
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
        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(_contract);
            return this;
        }

        public CorrectValueRule Build()
        {
            ClearCurrentToken();

            return new CorrectValueRule
            {
                MemberName = MemberName,
                Contract = _contract,
                Mode = Mode,
                Tokens = _valueTokens,
            };
        }
    }
}