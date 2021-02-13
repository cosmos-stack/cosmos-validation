using Cosmos.Validation.Internals.Conditions;

namespace Cosmos.Validation.Internals.Tokens
{
    internal interface IConditionToken : IToken
    {
        ConditionOps Ops { get; }
    }
}