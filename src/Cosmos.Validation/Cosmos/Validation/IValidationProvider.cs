using System;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation
{
    public interface IValidationProvider
    {
        IValidator Resolve(Type type);
        IValidator Resolve(Type type, string name);
        IValidator Resolve<T>();
        IValidator Resolve<T>(string name);

        void UpdateOptions(ValidationOptions options);
        void UpdateOptions(Action<ValidationOptions> optionAct);
    }
}