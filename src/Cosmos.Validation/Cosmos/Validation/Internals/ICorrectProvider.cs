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

        ICustomValidatorManager ExposeCustomValidatorManager();

        ValidationOptions ExposeValidationOptions();
    }
}