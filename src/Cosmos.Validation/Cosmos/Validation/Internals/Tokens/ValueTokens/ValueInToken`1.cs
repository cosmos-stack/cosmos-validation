using System;
using System.Collections.Generic;
using Cosmos.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueInToken<TVal> : ValueToken<TVal>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueInToken";

        private readonly ICollection<TVal> _objects;

        public ValueInToken(VerifiableMemberContract contract, ICollection<TVal> objects) : base(contract)
        {
            _objects = objects ?? Arrays.Empty<TVal>();
        }

        public override CorrectValueOps Ops => CorrectValueOps.In_T1;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};
           
            var value = GetValueFrom(context);

            if (!_objects.Contains(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = new CorrectVerifyVal {NameOfExecutedRule = NAME};
           
            var value = GetValueFrom(context);

            if (!_objects.Contains(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The value is not contained in the given value array or collection.");
        }

        public override string ToString() => NAME;
    }
}