using System;
using Cosmos.Validation;
using Cosmos.Validation.Objects;

#pragma warning disable 108,114

namespace CosmosValidationUT.Fakes
{
    public class FakeValueRuleBuilder<T, TVal> : FakeValueRuleBuilder<T>, IValueRuleBuilder<T, TVal>
    {
        public FakeValueRuleBuilder(VerifiableMemberContract contract) : base(contract) { }

        public IValueRuleBuilder<T, TVal> AppendRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> OverwriteRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> And()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Or()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Use(VerifyMemberRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder<T, TVal> Use(VerifyMemberRulePackage package)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> InEnum(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> InEnum<TEnum>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> IsEnumName(Type enumType, bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> IsEnumName<TEnum>(bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredType(Type type)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            throw new NotImplementedException();
        }
    }
}