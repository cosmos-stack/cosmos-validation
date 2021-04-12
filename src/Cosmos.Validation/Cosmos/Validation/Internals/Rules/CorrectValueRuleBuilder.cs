using System;
using Cosmos.Validation.Internals.Conditions;
using Cosmos.Validation.Internals.Tokens.ValueTokens;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Rules
{
    internal class CorrectValueRuleBuilder :
        IValueRuleBuilder,
        IPredicateValueRuleBuilder
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

        #region Activation Conditions

        public IValueRuleBuilder When(Func<object, bool> condition)
        {
            if (condition is not null)
            {
                State.CurrentToken.ActivationConditions2 = condition;
                State.CurrentToken.WithActivationConditions = true;
            }

            return this;
        }

        public IValueRuleBuilder When(Func<object, object, bool> condition)
        {
            if (condition is not null)
            {
                State.CurrentToken.ActivationConditions3 = condition;
                State.CurrentToken.WithActivationConditions = true;
            }

            return this;
        }

        public IValueRuleBuilder Unless(Func<object, bool> condition)
        {
            if (condition is not null)
            {
                State.CurrentToken.ActivationConditions2 = condition;
                State.CurrentToken.WithActivationConditions = true;
            }

            return this;
        }

        public IValueRuleBuilder Unless(Func<object, object, bool> condition)
        {
            if (condition is not null)
            {
                State.CurrentToken.ActivationConditions3 = condition;
                State.CurrentToken.WithActivationConditions = true;
            }

            return this;
        }

        #endregion

        #region Rules
        
        public IPredicateValueRuleBuilder InEnum(Type enumType)
        {
            State.CurrentToken = new ValueEnumToken(_contract, enumType);
            return this;
        }

        public IPredicateValueRuleBuilder InEnum<TEnum>()
        {
            State.CurrentToken = new ValueEnumToken<TEnum>(_contract);
            return this;
        }

        public IPredicateValueRuleBuilder IsEnumName(Type enumType, bool caseSensitive)
        {
            State.CurrentToken = new ValueStringEnumToken(_contract, enumType, caseSensitive);
            return this;
        }

        public IPredicateValueRuleBuilder IsEnumName<TEnum>(bool caseSensitive)
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
        public IPredicateValueRuleBuilder ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            State.CurrentToken = new ValueScalePrecisionToken(_contract, scale, precision, ignoreTrailingZeros);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IPredicateValueRuleBuilder RequiredType(Type type)
        {
            State.CurrentToken = new ValueRequiredTypeToken(_contract, type);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        public IPredicateValueRuleBuilder RequiredTypes(params Type[] types)
        {
            State.CurrentToken = new ValueRequiredTypesToken(_contract, types);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public IPredicateValueRuleBuilder RequiredTypes<T1>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
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
        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            State.CurrentToken = new ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(_contract);
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