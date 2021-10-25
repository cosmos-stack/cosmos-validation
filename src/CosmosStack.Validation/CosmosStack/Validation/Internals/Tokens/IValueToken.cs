using System;

namespace CosmosStack.Validation.Internals.Tokens
{
    /// <summary>
    /// Interface of value token
    /// </summary>
    internal interface IValueToken : IToken
    {
        /// <summary>
        /// To mark this Verifiable token as a mutually exclusive token.
        /// </summary>
        bool MutuallyExclusive { get; }

        /// <summary>
        /// If this verifiable token is mutually exclusive, then mark which tokens are mutually exclusive.
        /// </summary>
        int[] MutuallyExclusiveFlags { get; }

        /// <summary>
        /// Verification.
        /// </summary>
        /// <param name="context"></param>
        bool Verify(VerifiableOpsContext context);

        /// <summary>
        /// Custom message.
        /// </summary>
        string CustomMessage { get; set; }

        /// <summary>
        /// Activation condition <br />
        /// 1st param: Instance <br />
        /// 2nd param: result of activation condition
        /// </summary>
        Func<object, bool> ActivationConditions2 { get; set; }

        /// <summary>
        /// Activation condition <br />
        /// 1st param: Instance <br />
        /// 2nd param: Member's Value <br />
        /// 3rd param: result of activation condition
        /// </summary>
        Func<object, object, bool> ActivationConditions3 { get; set; }

        /// <summary>
        /// Mark whether to use activation conditions.
        /// </summary>
        bool WithActivationConditions { get; set; }

        /// <summary>
        /// Mark whether to use custom message.
        /// </summary>
        bool WithMessageMode { get; set; }

        /// <summary>
        /// If WithMessage is true, this AppendOrOverwrite takes effect. <br />
        /// true - Append <br />
        /// false - Overwrite
        /// </summary>
        bool AppendOrOverwrite { get; set; }
    }

    /// <summary>
    /// Interface of value token, a generic version.
    /// </summary>
    /// <typeparam name="TVal"></typeparam>
    internal interface IValueToken<TVal> : IValueToken { }
}