namespace Cosmos.Validation.Internals.Tokens
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
        void Verify(VerifiableOpsContext context);

        /// <summary>
        /// Custom message.
        /// </summary>
        string CustomMessage { get; set; }

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
    internal interface IValueToken<in TVal> : IValueToken { }
}