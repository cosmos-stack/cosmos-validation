namespace Cosmos.Validation.Validators
{
    internal interface ICorrectValidator
    {
        bool IsTypeBinding { get; }

        bool IsFluentValidator { get; set; }
    }

    internal interface ICorrectValidator<T> : ICorrectValidator { }
}