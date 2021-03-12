using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cosmos.Validation.Internals.Tokens;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRuleBuilder : IValueRuleBuilder
    {
        internal readonly VerifiableMemberContract _contract;

        private readonly List<IValueToken> _valueTokens;

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

        public IValueRuleBuilder Empty()
        {
            CurrentToken = new ValueEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder NotEmpty()
        {
            CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder Required()
        {
            CurrentToken = new ValueNotEmptyToken(_contract);
            return this;
        }

        public IValueRuleBuilder Null()
        {
            CurrentToken = new ValueNullToken(_contract);
            return this;
        }

        public IValueRuleBuilder NotNull()
        {
            CurrentToken = new ValueNotNullToken(_contract);
            return this;
        }

        public IValueRuleBuilder Length(int min, int max)
        {
            CurrentToken = new ValueLengthLimitedToken(_contract, min, max);
            return this;
        }

        public IValueRuleBuilder Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            CurrentToken = new ValueRangeToken(_contract, from, to, options);
            return this;
        }

        public IValueRuleBuilder RangeWithOpenInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(_contract, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public IValueRuleBuilder RangeWithCloseInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(_contract, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public IValueRuleBuilder MinLength(int min)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_contract, min);
            return this;
        }

        public IValueRuleBuilder MaxLength(int max)
        {
            CurrentToken = new ValueMaxLengthLimitedToken(_contract, max);
            return this;
        }

        public IValueRuleBuilder AtLeast(int count)
        {
            CurrentToken = new ValueMinLengthLimitedToken(_contract, count);
            return this;
        }

        public IValueRuleBuilder Equal(object value)
        {
            CurrentToken = new ValueEqualToken(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder Equal(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueEqualToken(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder NotEqual(object value)
        {
            CurrentToken = new ValueNotEqualToken(_contract, value, null);
            return this;
        }

        public IValueRuleBuilder NotEqual(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueNotEqualToken(_contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder LessThan(object value)
        {
            CurrentToken = new ValueLessThanToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder LessThanOrEqual(object value)
        {
            CurrentToken = new ValueLessThanOrEqualToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder GreaterThan(object value)
        {
            CurrentToken = new ValueGreaterThanToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder GreaterThanOrEqual(object value)
        {
            CurrentToken = new ValueGreaterThanOrEqualToken(_contract, value);
            return this;
        }

        public IValueRuleBuilder Matches(Regex regex)
        {
            CurrentToken = new ValueRegularExpressionToken(_contract, regex);
            return this;
        }

        public IValueRuleBuilder Matches(string regexExpression)
        {
            CurrentToken = new ValueRegularExpressionToken(_contract, regexExpression);
            return this;
        }

        public IValueRuleBuilder Matches(string regexExpression, RegexOptions options)
        {
            CurrentToken = new ValueRegularExpressionToken(_contract, regexExpression, options);
            return this;
        }

        public IValueRuleBuilder Matches(Func<object, Regex> regexFunc)
        {
            CurrentToken = new ValueRegularExpressionToken(_contract, regexFunc);
            return this;
        }

        public IValueRuleBuilder Matches(Func<object, string> regexExpressionFunc)
        {
            CurrentToken = new ValueRegularExpressionToken(_contract, regexExpressionFunc);
            return this;
        }

        public IValueRuleBuilder Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            CurrentToken = new ValueRegularExpressionToken(_contract, regexExpressionFunc, options);
            return this;
        }

        public IValueRuleBuilder Func(Func<object, CustomVerifyResult> func)
        {
            CurrentToken = new ValueFuncToken(_contract, func);
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
            CurrentToken = new ValueFuncToken(_contract, func);
            return this;
        }

        public IWaitForMessageValueRuleBuilder Must(Func<object, bool> func)
        {
            return new CorrectWaitForMessageValueRuleBuilder(this, func);
        }

        public IValueRuleBuilder Any(Func<object, bool> func)
        {
            CurrentToken = new ValueAnyToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder All(Func<object, bool> func)
        {
            CurrentToken = new ValueAllToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder NotAny(Func<object, bool> func)
        {
            CurrentToken = new ValueAllToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder NotAll(Func<object, bool> func)
        {
            CurrentToken = new ValueAnyToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder None(Func<object, bool> func)
        {
            CurrentToken = new ValueNoneToken(_contract, func);
            return this;
        }

        public IValueRuleBuilder In(ICollection<object> collection)
        {
            CurrentToken = new ValueInToken(_contract, collection);
            return this;
        }

        public IValueRuleBuilder In(params object[] objects)
        {
            CurrentToken = new ValueInToken(_contract, objects);
            return this;
        }

        public IValueRuleBuilder NotIn(ICollection<object> collection)
        {
            CurrentToken = new ValueNotInToken(_contract, collection);
            return this;
        }

        public IValueRuleBuilder NotIn(params object[] objects)
        {
            CurrentToken = new ValueNotInToken(_contract, objects);
            return this;
        }

        public IValueRuleBuilder InEnum(Type enumType)
        {
            CurrentToken = new ValueEnumToken(_contract, enumType);
            return this;
        }

        public IValueRuleBuilder InEnum<TEnum>()
        {
            CurrentToken = new ValueEnumToken<TEnum>(_contract);
            return this;
        }

        public IValueRuleBuilder IsEnumName(Type enumType, bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken(_contract, enumType, caseSensitive);
            return this;
        }

        public IValueRuleBuilder IsEnumName<TEnum>(bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken<TEnum>(_contract, caseSensitive);
            return this;
        }

        public IValueRuleBuilder ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            CurrentToken = new ValueScalePrecisionToken(_contract, scale, precision, ignoreTrailingZeros);
            return this;
        }

        public IValueRuleBuilder RequiredType(Type type)
        {
            CurrentToken = new ValueRequiredTypeToken(_contract, type);
            return this;
        }

        public IValueRuleBuilder RequiredTypes(params Type[] types)
        {
            CurrentToken = new ValueRequiredTypesToken(_contract, types);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1>()
        {
            CurrentToken = new ValueRequiredTypeToken<T1>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(_contract);
            return this;
        }

        public IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
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