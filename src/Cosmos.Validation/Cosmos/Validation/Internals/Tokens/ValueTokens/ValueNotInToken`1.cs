using System.Collections.Generic;
using Cosmos.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueNotInToken<TVal> : ValueToken<TVal>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueInToken";

        private readonly ICollection<TVal> _objects;

        public ValueNotInToken(VerifiableMemberContract contract, ICollection<TVal> objects) : base(contract)
        {
            _objects = objects ?? Arrays.Empty<TVal>();
        }

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();
           
            var value = GetValueFrom(context);

            if (_objects.Contains(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();
           
            var value = GetValueFrom(context);

            if (_objects.Contains(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        private void UpdateVal(CorrectVerifyVal val, object obj)
        {
            val.IsSuccess = false;
            val.VerifiedValue = obj;
            val.ErrorMessage = MergeMessage("The value is contained in the given value array or collection.");
        }

        public override string ToString() => NAME;
    }
}