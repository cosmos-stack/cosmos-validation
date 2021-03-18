using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens
{
    /// <summary>
    /// Value token
    /// </summary>
    internal abstract class ValueToken : IValueToken
    {
        /// <summary>
        /// Empty Mutually Exclusive Flags.
        /// </summary>
        public static readonly int[] NoMutuallyExclusiveFlags = { };

        protected ValueToken(VerifiableMemberContract contract)
        {
            VerifiableMember = contract ?? throw new ArgumentNullException(nameof(contract));
        }

        /// <summary>
        /// Name of verifiable token
        /// </summary>
        public abstract string TokenName { get; }

        /// <summary>
        /// Class of verifiable token
        /// </summary>
        public virtual TokenClass TokenClass => TokenClass.ValueToken;

        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        public abstract bool MutuallyExclusive { get; }

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        public abstract int[] MutuallyExclusiveFlags { get; }

        /// <summary>
        /// Verification.
        /// </summary>
        /// <param name="context"></param>
        public void Verify(VerifiableOpsContext context)
        {
            var val = context.OpsMode switch
            {
                VerifiableOpsMode.Object => Valid(context.VerifiableObjectContext),
                VerifiableOpsMode.Member => Valid(context.VerifiableMemberContext),
                _ => null
            };

            if (val is not null && val.IsSuccess == false)
            {
                context.AppendVerifyVal(VerifiableMember.MemberName, val);
                context.AppendNameOfExecutedRule(val.NameOfExecutedRule);
            }
        }

        /// <summary>
        /// Verification for VerifiableObjectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal abstract CorrectVerifyVal Valid(VerifiableObjectContext context);

        /// <summary>
        /// Verification for VerifiableMemberContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal abstract CorrectVerifyVal Valid(VerifiableMemberContext context);

        /// <summary>
        /// Verifiable Member
        /// </summary>
        protected VerifiableMemberContract VerifiableMember { get; }

        /// <summary>
        /// Custom message.
        /// </summary>
        public string CustomMessage { get; set; }

        /// <summary>
        /// Mark whether to use custom message.
        /// </summary>
        public bool WithMessageMode { get; set; }

        /// <summary>
        /// If WithMessage is true, this AppendOrOverwrite takes effect. <br />
        /// true - Append <br />
        /// false - Overwrite
        /// </summary>
        public bool AppendOrOverwrite { get; set; }

        /// <summary>
        /// Merge message
        /// </summary>
        /// <param name="messageSinceToken"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get value from VerifiableObjectContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected object GetValueFrom(VerifiableObjectContext context)
        {
            var memberContext = context?.GetValue(VerifiableMember.MemberName);
            return memberContext?.GetValue();
        }

        /// <summary>
        /// Get value from VerifiableMemberContext
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected object GetValueFrom(VerifiableMemberContext context)
        {
            return context?.GetValue();
        }

        /// <summary>
        /// Determine whether this verifiable token can be verified against the given VerifiableObjectContext.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected bool ContainsMember(VerifiableObjectContext context)
        {
            if (context is null) return false;
            return context.ContainsMember(VerifiableMember.MemberName);
        }

        /// <summary>
        /// Create a new instance of VerifyVal
        /// </summary>
        /// <returns></returns>
        protected CorrectVerifyVal CreateVerifyVal()
        {
            return new CorrectVerifyVal {NameOfExecutedRule = TokenName};
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => TokenName;
    }

    /// <summary>
    /// Value token, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    internal abstract class ValueToken<TVal> : ValueToken, IValueToken<TVal>
    {
        /// <inheritdoc />
        protected ValueToken(VerifiableMemberContract contract) : base(contract) { }

        /// <summary>
        /// Verification for VerifiableObjectContext, a generic version.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected new TVal GetValueFrom(VerifiableObjectContext context)
        {
            var memberContext = context?.GetValue(VerifiableMember.MemberName);
            if (memberContext is null)
                return default;
            return memberContext.GetValue<TVal>();
        }

        /// <summary>
        /// Verification for VerifiableMemberContext, a generic version.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected new TVal GetValueFrom(VerifiableMemberContext context)
        {
            if (context is null)
                return default;
            return context.GetValue<TVal>();
        }
    }
}