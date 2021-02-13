namespace Cosmos.Validation.Internals.Tokens
{
    internal interface IToken
    {
        string TokenName { get; }

        TokenClass TokenClass { get; }
    }
}