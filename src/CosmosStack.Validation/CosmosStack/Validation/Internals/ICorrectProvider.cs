using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Projects;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Internals
{
    internal interface ICorrectProvider
    {
        string Name { get; set; }
        IValidationProjectManager ExposeProjectManager();

        IVerifiableObjectResolver ExposeObjectResolver();

        ICustomValidatorManager ExposeCustomValidatorManager();

        ValidationOptions ExposeValidationOptions();
    }
}