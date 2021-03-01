using System;

namespace Cosmos.Validation.Validators
{
    internal interface ICorrectValidator
    {
        bool IsTypeBinding { get; }

        Type SourceType { get; }
    }

    internal interface ICorrectValidator<T> : ICorrectValidator { }
}