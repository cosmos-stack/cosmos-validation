using System;
using System.Linq;
using Cosmos.Reflection;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    /// <summary>
    /// Required types token
    /// </summary>
    internal class ValueRequiredTypesToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueRequiredTypesToken";
        private readonly Type[] _types;

        /// <inheritdoc />
        public ValueRequiredTypesToken(VerifiableMemberContract contract, params Type[] types) : base(contract)
        {
            _types = types;
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public override string TokenName => NAME;

        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        public override bool MutuallyExclusive => false;

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        /// <summary>
        /// Verification for VerifiableObjectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        /// <summary>
        /// Verification for VerifiableMemberContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();

            var value = GetValueFrom(context);

            if (!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        private bool IsValidImpl(object value)
        {
            if (!_types.Any())
                return true;

            var realType = _getType();

            foreach (var type in _types)
            {
                if (VerifiableMember.MemberType == type || VerifiableMember.MemberType.IsDerivedFrom(type))
                    return true;

                if (realType is not null && (realType == type || realType.IsDerivedFrom(type)))
                    return true;
            }

            return false;

            Type _getType()
            {
                if (value is Type t)
                    return t;
                return value?.GetType();
            }
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage($"The given type is not a derived class or implementation of any one of the types in the list. The current type is {VerifiableMember.MemberType.GetFriendlyName()}.");
        }
    }

    internal class ValueRequiredTypesToken<T1, T2> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15)) { }
    }

    internal class ValueRequiredTypesToken<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16> : ValueRequiredTypesToken
    {
        public ValueRequiredTypesToken(VerifiableMemberContract contract)
            : base(contract, typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10), typeof(T11), typeof(T12), typeof(T13), typeof(T14), typeof(T15), typeof(T16)) { }
    }
}