using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueRequiredTypeToken<T> : ValueRequiredTypeToken
    {
        public ValueRequiredTypeToken(ObjectValueContract contract) : base(contract, typeof(T)) { }
    }
}