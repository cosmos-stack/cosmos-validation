using System;

namespace Cosmos.Validation.Strategies
{
    public interface IValidationStrategy
    {
        Type SourceType { get; }
    }

    public interface IValidationStrategy<T>
    {
        Type SourceType { get; }
    }
}