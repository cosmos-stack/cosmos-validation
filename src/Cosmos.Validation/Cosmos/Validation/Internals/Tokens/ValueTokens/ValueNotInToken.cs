using System.Collections.Generic;
using Cosmos.Collections;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueNotInToken : ValueToken
    {
        // ReSharper disable once InconsistentNaming
        public const string NAME = "ValueNotInToken";

        private readonly ICollection<object> _objects;

        public ValueNotInToken(ObjectValueContract contract, ICollection<object> objects) : base(contract)
        {
            _objects = objects ?? Arrays.Empty<object>();
        }

        public override CorrectValueOps Ops => CorrectValueOps.NotIn;

        public override string TokenName => NAME;

        public override bool MutuallyExclusive => false;

        public override int[] MutuallyExclusiveFlags => NoMutuallyExclusiveFlags;

        protected override CorrectVerifyVal ValidValueImpl(object value)
        {
            var val = new CorrectVerifyVal {NameOfExecutedRule = NAME};

            if (_objects.Contains(value))
            {
                UpdateVal(val, value);
            }

            return val;
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