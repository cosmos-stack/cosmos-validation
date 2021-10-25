using System;
using CosmosStack.Validation;
using CosmosStack.Validation.Objects;

namespace CosmosValidationUT.Fakes
{
    public class FakeValueRuleBuilder : IValueRuleBuilder
    {
        internal readonly VerifiableMemberContract Contract;

        public FakeValueRuleBuilder(VerifiableMemberContract contract)
        {
            Contract = contract;
        }

        public IValueRuleBuilder AppendRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder OverwriteRule()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder And()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Or()
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Use(VerifyMemberRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            throw new NotImplementedException();
        }

        public IValueRuleBuilder Use(VerifyMemberRulePackage package)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder InEnum(Type enumType)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder InEnum<TEnum>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder IsEnumName(Type enumType, bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder IsEnumName<TEnum>(bool caseSensitive)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredType(Type type)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes(params Type[] types)
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            throw new NotImplementedException();
        }

        public IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            throw new NotImplementedException();
        }
    }
}