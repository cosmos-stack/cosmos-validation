using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens
{
    internal abstract class ValueToken : IValueToken
    {
        public static int[] NoMutuallyExclusiveFlags = { };

        // ReSharper disable once InconsistentNaming
        private readonly VerifiableMemberContract _contract;

        protected ValueToken(VerifiableMemberContract contract)
        {
            _contract = contract ?? throw new ArgumentNullException(nameof(contract));
        }

        public abstract CorrectValueOps Ops { get; }

        public abstract string TokenName { get; }

        public virtual TokenClass TokenClass => TokenClass.ValueToken;

        public abstract bool MutuallyExclusive { get; }

        public abstract int[] MutuallyExclusiveFlags { get; }

        protected abstract CorrectVerifyVal ValidValueImpl(object value);

        public virtual CorrectVerifyVal ValidValue(VerifiableMemberContext context)
        {
            return ValidValueImpl(context.Value);
        }

        protected VerifiableMemberContract VerifiableMember => _contract;

        public string CustomMessage { get; set; }

        public bool WithMessageMode { get; set; }

        /// <summary>
        /// If WithMessage is true, this AppendOrOverwrite takes effect. <br />
        /// true - Append <br />
        /// false - Overwrite
        /// </summary>
        public bool AppendOrOverwrite { get; set; }

        protected string MergeMessage(string messageSinceToken)
        {
            if (WithMessageMode)
            {
                return AppendOrOverwrite
                    ? $"{messageSinceToken} {CustomMessage}"
                    : CustomMessage;
            }

            return messageSinceToken;
        }
    }

    internal abstract class ValueToken<TVal> : ValueToken, IValueToken<TVal>
    {
        protected ValueToken(VerifiableMemberContract contract) : base(contract) { }

        protected abstract CorrectVerifyVal ValidValueImpl(TVal value);

        protected override CorrectVerifyVal ValidValueImpl(object value)
        {
            if (value is TVal t)
            {
                return ValidValueImpl(t);
            }

            return CorrectVerifyVal.Success;
        }

        public override CorrectVerifyVal ValidValue(VerifiableMemberContext context)
        {
            if (context.Value is TVal t)
            {
                return ValidValueImpl(t);
            }

            return CorrectVerifyVal.Success;
        }
    }
}