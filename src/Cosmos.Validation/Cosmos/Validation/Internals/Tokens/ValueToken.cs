using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens
{
    /// <summary>
    /// Value token
    /// </summary>
    internal abstract class ValueToken : IValueToken
    {
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
        /// If WithMessage is true, this AppendOrOverwrite takes effect. <br />
        /// true - Append <br />
        /// false - Overwrite
        /// </summary>
        public bool AppendOrOverwrite { get; set; }

        #region MutuallyExclusive

        /// <summary>
        /// Empty Mutually Exclusive Flags.
        /// </summary>
        public static readonly int[] NoMutuallyExclusiveFlags = { };

        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        public abstract bool MutuallyExclusive { get; }

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        public abstract int[] MutuallyExclusiveFlags { get; }

        #endregion

        #region Verify

        /// <summary>
        /// Verification.
        /// </summary>
        /// <param name="context"></param>
        public bool Verify(VerifiableOpsContext context)
        {
            var val = context.OpsMode switch
            {
                VerifiableOpsMode.Object => Valid(context.VerifiableObjectContext),
                VerifiableOpsMode.Member => Valid(context.VerifiableMemberContext),
                _ => null
            };

            if (val is not null)
            {
                // 无论成功或失败， 都将 CorrectVerifyVal 写入 context 内
                context.AppendVerifyVal(VerifiableMember.MemberName, val);

                // 过滤 CorrectVerifyVal 结果为 Success 的规则名
                (!val.IsIgnore && !val.IsSuccess).IfFalse(v => context.AppendNameOfExecutedRule(v.NameOfExecutedRule), val);
            }

            return val?.IsSuccess ?? true;
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

        #endregion

        #region Custom Message

        /// <summary>
        /// Custom message.
        /// </summary>
        public string CustomMessage { get; set; }

        /// <summary>
        /// Mark whether to use custom message.
        /// </summary>
        public bool WithMessageMode { get; set; }

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

        #endregion

        #region Activation Conditions

        /// <summary>
        /// Activation condition <br />
        /// 1st param: Instance <br />
        /// 2nd param: result of activation condition
        /// </summary>
        public Func<object, bool> ActivationConditions2 { get; set; }

        /// <summary>
        /// Activation condition <br />
        /// 1st param: Instance <br />
        /// 2nd param: Member's Value <br />
        /// 3rd param: result of activation condition
        /// </summary>
        public Func<object, object, bool> ActivationConditions3 { get; set; }

        /// <summary>
        /// Mark whether to use activation conditions.
        /// </summary>
        public bool WithActivationConditions { get; set; }

        /// <summary>
        /// Is activate, This method is only applicable to 'Verify'.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsActivate(VerifiableMemberContext context, object value)
        {
            if (WithActivationConditions && ActivationConditions3 is not null && context.HasParentContext())
                return ActivationConditions3.Invoke(context.GetParentInstance(), value);
            if (WithActivationConditions && ActivationConditions2 is not null)
                return ActivationConditions2.Invoke(value);
            return true;
        }

        /// <summary>
        /// Is activate, This method is only applicable to 'Verify'.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsActivate(VerifiableObjectContext context, object value)
        {
            if (WithActivationConditions && ActivationConditions3 is not null)
                return ActivationConditions3.Invoke(context.Instance, value);
            if (WithActivationConditions && ActivationConditions2 is not null)
                return ActivationConditions2.Invoke(value);
            return true;
        }

        #endregion

        #region GetValue

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

        #endregion

        #region Member

        /// <summary>
        /// Verifiable Member
        /// </summary>
        protected VerifiableMemberContract VerifiableMember { get; }

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

        #endregion

        #region CorrectVerifyVal

        /// <summary>
        /// Create a new instance of VerifyVal
        /// </summary>
        /// <returns></returns>
        protected CorrectVerifyVal CreateVerifyVal()
        {
            return new() {NameOfExecutedRule = TokenName};
        }

        #endregion

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

        #region Activation Conditions

        /// <summary>
        /// Is activate, This method is only applicable to 'Verify'.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsActivate(VerifiableMemberContext context, TVal value)
        {
            if (WithActivationConditions && ActivationConditions3 is not null && context.HasParentContext())
                return ActivationConditions3.Invoke(context.GetParentInstance(), value);
            if (WithActivationConditions && ActivationConditions2 is not null)
                return ActivationConditions2.Invoke(value);
            return true;
        }

        /// <summary>
        /// Is activate, This method is only applicable to 'Verify'.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool IsActivate(VerifiableObjectContext context, TVal value)
        {
            if (WithActivationConditions && ActivationConditions3 is not null)
                return ActivationConditions3.Invoke(context.Instance, value);
            if (WithActivationConditions && ActivationConditions2 is not null)
                return ActivationConditions2.Invoke(value);
            return true;
        }

        #endregion

        #region GetValue

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

        #endregion
    }
}