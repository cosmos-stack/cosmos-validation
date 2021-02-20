namespace Cosmos.Validation.Validators
{
    internal interface ICorrectValidator
    {
        bool IsTypeBinding { get; }
    }

    internal interface ICorrectValidator<T> : ICorrectValidator { }
}