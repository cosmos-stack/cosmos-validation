using System;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation
{
    public interface IValidationProvider
    {
        IValidator Resolve(Type type);
        IValidator Resolve(Type type, string name);
        IValidator<T> Resolve<T>();
        IValidator<T> Resolve<T>(string name);

        void UpdateOptions(ValidationOptions options);
        void UpdateOptions(Action<ValidationOptions> optionAct);
    }
}