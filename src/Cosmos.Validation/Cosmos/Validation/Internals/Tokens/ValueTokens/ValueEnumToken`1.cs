using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Internals.Tokens.ValueTokens
{
    internal class ValueEnumToken<TEnum> : ValueEnumToken
    {
        public ValueEnumToken(ObjectValueContract contract) : base(contract, typeof(TEnum)) { }
    }
}