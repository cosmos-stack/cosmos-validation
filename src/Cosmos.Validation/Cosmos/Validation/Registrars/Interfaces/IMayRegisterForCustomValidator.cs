using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayRegisterForCustomValidator
    {
        IValidationRegistrar ForCustomValidator<TValidator>() where TValidator : CustomValidator, new();
        IValidationRegistrar ForCustomValidator<TValidator, T>() where TValidator : CustomValidator<T>, new();
        IValidationRegistrar ForCustomValidator(CustomValidator validator);
        IValidationRegistrar ForCustomValidator<T>(CustomValidator<T> validator);
    }

    public interface IMayContinueRegisterForCustomValidator
    {
        IFluentValidationRegistrar AndForCustomValidator<TValidator>() where TValidator : CustomValidator, new();
        IFluentValidationRegistrar AndForCustomValidator<TValidator, T>() where TValidator : CustomValidator<T>, new();
        IFluentValidationRegistrar AndForCustomValidator(CustomValidator validator);
        IFluentValidationRegistrar AndForCustomValidator<T>(CustomValidator<T> validator);
    }
}