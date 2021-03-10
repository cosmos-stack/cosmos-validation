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
        internal readonly VerifiableMemberContract Contract;

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
            Contract = contract;
            _valueTokens = new List<IValueToken>();
        }

        public CorrectValueRuleBuilder(VerifiableMemberContract contract, ValueRuleMode mode)
        {
            Contract = contract;
            _valueTokens = new List<IValueToken>();
            Mode = mode.X();
        }

        public string MemberName => Contract.MemberName;

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
            CurrentToken = new ValueEmptyToken(Contract);
            return this;
        }

        public IValueRuleBuilder<T> NotEmpty()
        {
            CurrentToken = new ValueNotEmptyToken(Contract);
            return this;
        }

        public IValueRuleBuilder<T> Required()
        {
            CurrentToken = new ValueNotEmptyToken(Contract);
            return this;
        }

        public IValueRuleBuilder<T> Null()
        {
            CurrentToken = new ValueNullToken(Contract);
            return this;
        }

        public IValueRuleBuilder<T> NotNull()
        {
            CurrentToken = new ValueNotNullToken(Contract);
            return this;
        }

        public IValueRuleBuilder<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            CurrentToken = new ValueRangeToken(Contract, from, to, options);
            return this;
        }

        public IValueRuleBuilder<T> RangeWithOpenInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(Contract, from, to, RangeOptions.OpenInterval);
            return this;
        }

        public IValueRuleBuilder<T> RangeWithCloseInterval(object from, object to)
        {
            CurrentToken = new ValueRangeToken(Contract, from, to, RangeOptions.CloseInterval);
            return this;
        }

        public IValueRuleBuilder<T> Length(int min, int max)
        {
            CurrentToken = new ValueLengthLimitedToken(Contract, min, max);
            return this;
        }

        public IValueRuleBuilder<T> MinLength(int min)
        {
            CurrentToken = new ValueMinLengthLimitedToken(Contract, min);
            return this;
        }

        public IValueRuleBuilder<T> MaxLength(int max)
        {
            CurrentToken = new ValueMaxLengthLimitedToken(Contract, max);
            return this;
        }

        public IValueRuleBuilder<T> AtLeast(int count)
        {
            CurrentToken = new ValueMinLengthLimitedToken(Contract, count);
            return this;
        }

        public IValueRuleBuilder<T> Equal(object value)
        {
            CurrentToken = new ValueEqualToken(Contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueEqualToken(Contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T> NotEqual(object value)
        {
            CurrentToken = new ValueNotEqualToken(Contract, value, null);
            return this;
        }

        public IValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer)
        {
            CurrentToken = new ValueNotEqualToken(Contract, value, comparer);
            return this;
        }

        public IValueRuleBuilder<T> LessThan(object value)
        {
            CurrentToken = new ValueLessThanToken(Contract, value);
            return this;
        }

        public IValueRuleBuilder<T> LessThanOrEqual(object value)
        {
            CurrentToken = new ValueLessThanOrEqualToken(Contract, value);
            return this;
        }

        public IValueRuleBuilder<T> GreaterThan(object value)
        {
            CurrentToken = new ValueGreaterThanToken(Contract, value);
            return this;
        }

        public IValueRuleBuilder<T> GreaterThanOrEqual(object value)
        {
            CurrentToken = new ValueGreaterThanOrEqualToken(Contract, value);
            return this;
        }

        public IValueRuleBuilder<T> Matches(Regex regex)
        {
            CurrentToken = new ValueRegularExpressionToken(Contract, regex);
            return this;
        }

        public IValueRuleBuilder<T> Matches(string regexExpression)
        {
            CurrentToken = new ValueRegularExpressionToken(Contract, regexExpression);
            return this;
        }

        public IValueRuleBuilder<T> Matches(string regexExpression, RegexOptions options)
        {
            CurrentToken = new ValueRegularExpressionToken(Contract, regexExpression, options);
            return this;
        }

        // public IValueRuleBuilder<T> Matches(Func<object, Regex> regexFunc)
        // {
        //     CurrentToken = new ValueRegularExpressionToken(Contract, regexFunc);
        //     return this;
        // }
        //
        // public IValueRuleBuilder<T> Matches(Func<object, string> regexExpressionFunc)
        // {
        //     CurrentToken = new ValueRegularExpressionToken(Contract, regexExpressionFunc);
        //     return this;
        // }
        //
        // public IValueRuleBuilder<T> Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        // {
        //     CurrentToken = new ValueRegularExpressionToken(Contract, regexExpressionFunc, options);
        //     return this;
        // }

        public IValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func)
        {
            CurrentToken = new ValueFuncToken(Contract, func);
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
            CurrentToken = new ValueFuncToken(Contract, func);
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
            CurrentToken = new ValueInToken(Contract, collection);
            return this;
        }

        public IValueRuleBuilder<T> In(params object[] objects)
        {
            CurrentToken = new ValueInToken(Contract, objects);
            return this;
        }

        public IValueRuleBuilder<T> NotIn(ICollection<object> collection)
        {
            CurrentToken = new ValueNotInToken(Contract, collection);
            return this;
        }

        public IValueRuleBuilder<T> NotIn(params object[] objects)
        {
            CurrentToken = new ValueNotInToken(Contract, objects);
            return this;
        }

        public IValueRuleBuilder<T> InEnum(Type enumType)
        {
            CurrentToken = new ValueEnumToken(Contract, enumType);
            return this;
        }

        public IValueRuleBuilder<T> InEnum<TEnum>()
        {
            CurrentToken = new ValueEnumToken<TEnum>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> IsEnumName(Type enumType, bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken(Contract, enumType, caseSensitive);
            return this;
        }

        public IValueRuleBuilder<T> IsEnumName<TEnum>(bool caseSensitive)
        {
            CurrentToken = new ValueStringEnumToken<TEnum>(Contract, caseSensitive);
            return this;
        }

        public IValueRuleBuilder<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            CurrentToken = new ValueScalePrecisionToken(Contract, scale, precision, ignoreTrailingZeros);
            return this;
        }

        public IValueRuleBuilder<T> RequiredType(Type type)
        {
            CurrentToken = new ValueRequiredTypeToken(Contract, type);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes(params Type[] types)
        {
            CurrentToken = new ValueRequiredTypesToken(Contract, types);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1>()
        {
            CurrentToken = new ValueRequiredTypeToken<T1>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(Contract);
            return this;
        }

        public IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(Contract);
            return this;
        }

        public CorrectValueRule Build()
        {
            ClearCurrentToken();

            return new CorrectValueRule
            {
                MemberName = MemberName,
                Contract = Contract,
                Mode = Mode,
                Tokens = _valueTokens,
            };
        }
    }
}