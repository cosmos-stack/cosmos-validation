using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal interface ICorrectProvider
    {
        string Name { get; set; }
        IValidationProjectManager ExposeProjectManager();

        IValidationObjectResolver ExposeObjectResolver();

        CustomValidatorManager ExposeCustomValidatorManager();

        ValidationOptions ExposeValidationOptions();

        void RegisterValidator<TValidator>() where TValidator : CustomValidator, new();

        void RegisterValidator<TValidator, T>() where TValidator : CustomValidator<T>, new();

        void RegisterValidator(CustomValidator validator);

        void RegisterValidator<T>(CustomValidator<T> validator);
    }
}