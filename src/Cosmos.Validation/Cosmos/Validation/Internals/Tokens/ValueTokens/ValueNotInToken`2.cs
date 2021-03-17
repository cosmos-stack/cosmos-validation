using System.Collections.Generic;
using System.Linq;
using Cosmos.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueNotInToken<TVal, TItem> : ValueToken<TVal>
        where TVal : IEnumerable<TItem>
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "GenericValueInToken2";

        private readonly ICollection<TItem> _objects;

        public ValueNotInToken(VerifiableMemberContract contract, ICollection<TItem> objects) : base(contract)
        {
            _objects = objects ?? Arrays.Empty<TItem>();
        }

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        public override CorrectVerifyVal Valid(VerifiableObjectContext context)
        {
            var verifyVal = CreateVerifyVal();
           
            var value = GetValueFrom(context);

            if(!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }

        public override CorrectVerifyVal Valid(VerifiableMemberContext context)
        {
            var verifyVal = CreateVerifyVal();
           
            var value = GetValueFrom(context);

            if(!IsValidImpl(value))
            {
                UpdateVal(verifyVal, value);
            }

            return verifyVal;
        }
        
        private bool IsValidImpl(IEnumerable<TItem> items)
        {
            return items.All(item => !_objects.Contains(item));
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