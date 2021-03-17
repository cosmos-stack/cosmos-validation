namespace Cosmos.Validation.Internals.Tokens
{
    internal interface IValueToken : IToken
    {
        bool MutuallyExclusive { get; }

        int[] MutuallyExclusiveFlags { get; }

        void Verify(VerifiableOpsContext context);

        string CustomMessage { get; set; }

        bool WithMessageMode { get; set; }

        /// <summary>
        /// If WithMessage is true, this AppendOrOverwrite takes effect. <br />
        /// true - Append <br />
        /// false - Overwrite
        /// </summary>
        bool AppendOrOverwrite { get; set; }
    }

    internal interface IValueToken<in TVal> : IValueToken { }
}