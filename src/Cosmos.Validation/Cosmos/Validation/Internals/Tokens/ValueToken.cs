using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens
{
    internal abstract class ValueToken : IValueToken
    {
        public static int[] NoMutuallyExclusiveFlags = { };

        protected ValueToken(VerifiableMemberContract contract)
        {
            VerifiableMember = contract ?? throw new ArgumentNullException(nameof(contract));
        }

        public abstract CorrectValueOps Ops { get; }

        public abstract string TokenName { get; }

        public virtual TokenClass TokenClass => TokenClass.ValueToken;

        public abstract bool MutuallyExclusive { get; }

        public abstract int[] MutuallyExclusiveFlags { get; }

        public abstract CorrectVerifyVal Valid(VerifiableObjectContext context);

        public abstract CorrectVerifyVal Valid(VerifiableMemberContext context);

        protected VerifiableMemberContract VerifiableMember { get; }

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

        protected object GetValueFrom(VerifiableObjectContext context)
        {
            var memberContext = context?.GetValue(VerifiableMember.MemberName);
            return memberContext?.GetValue();
        }

        protected object GetValueFrom(VerifiableMemberContext context)
        {
            return context?.GetValue();
        }

        protected bool ContainsMember(VerifiableObjectContext context)
        {
            if (context is null) return false;
            return context.ContainsMember(VerifiableMember.MemberName);
        }
    }

    internal abstract class ValueToken<TVal> : ValueToken, IValueToken<TVal>
    {
        protected ValueToken(VerifiableMemberContract contract) : base(contract) { }

        protected new TVal GetValueFrom(VerifiableObjectContext context)
        {
            var memberContext = context?.GetValue(VerifiableMember.MemberName);
            if (memberContext is null)
                return default;
            return memberContext.GetValue<TVal>();
        }

        protected new TVal GetValueFrom(VerifiableMemberContext context)
        {
            if (context is null)
                return default;
            return context.GetValue<TVal>();
        }
    }
}