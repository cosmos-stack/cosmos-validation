using System;
using Cosmos.Validation;
using Cosmos.Validation.Objects;

namespace CosmosValidationUT.Fakes
{
    public class FakeValueRuleBuilder<T> : IValueRuleBuilder<T>
    {
        internal readonly VerifiableMemberContract Contract;

        public FakeValueRuleBuilder(VerifiableMemberContract contract)
        {
            Contract = contract;
        }

        public IValueRuleBuilder<T> AppendRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> OverwriteRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> And()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T> Or()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> InEnum(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> InEnum<TEnum>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> IsEnumName(Type enumType, bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> IsEnumName<TEnum>(bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredType(Type type)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            throw new NotImplementedException();
        }
    }
}