namespace Cosmos.Validation.Internals.Tokens
{
    /// <summary>
    /// Interface for verifiable token
    /// </summary>
    internal interface IToken
    {
        /// <summary>
        /// Name of verifiable token
        /// </summary>
        string TokenName { get; }

        /// <summary>
        /// Class of verifiable token
        /// </summary>
        TokenClass TokenClass { get; }
    }
}