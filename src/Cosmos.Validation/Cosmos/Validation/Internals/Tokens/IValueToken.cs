using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens
{
    internal interface IValueToken : IToken
    {
        CorrectValueOps Ops { get; }

        bool MutuallyExclusive { get; }

        int[] MutuallyExclusiveFlags { get; }

        CorrectVerifyVal ValidValue(VerifiableMemberContext context);

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