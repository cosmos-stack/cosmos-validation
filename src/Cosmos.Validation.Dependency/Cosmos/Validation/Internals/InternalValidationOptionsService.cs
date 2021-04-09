using System;
using Cosmos.Reflection;

namespace Cosmos.Validation.Internals
{
    internal class InternalValidationOptionsService : IValidationOptionsService
    {
        public ValidationOptions GetDefaultOptions() => ((ICorrectProvider) ValidationMe.ExposeDefaultProvider()).ExposeValidationOptions().DeepCopy();
        
        public void ReplaceDefaultOptions(ValidationOptions options) => ValidationMe.UpdateDefaultOptions(options.DeepCopy());

        public void UpdateDefaultOptions(Action<ValidationOptions> optionAct) => ValidationMe.UpdateDefaultOptions(optionAct);
        
        public ValidationOptions GetMainOptions() => ((ICorrectProvider) ValidationMe.ExposeValidationProvider()).ExposeValidationOptions().DeepCopy();

        public void ReplaceMainOptions(ValidationOptions options) => ValidationMe.UpdateMainOptions(options.DeepCopy());

        public void UpdateMainOptions(Action<ValidationOptions> optionAct) => ValidationMe.UpdateMainOptions(optionAct);
        
        public ValidationOptions GetOptions() => ((ICorrectProvider) ValidationMe.ExposeValidationProvider()).ExposeValidationOptions().DeepCopy();

        public void ReplaceOptions(ValidationOptions options) => ValidationMe.UpdateMainOptions(options.DeepCopy());

        public void UpdateOptions(Action<ValidationOptions> optionAct) => ValidationMe.UpdateMainOptions(optionAct);
        
        public ValidationOptions GetOptions(string providerName) => ((ICorrectProvider) ValidationMe.ExposeValidationProvider(providerName)).ExposeValidationOptions().DeepCopy();

        public void ReplaceOptions(ValidationOptions options, string providerName) => ValidationMe.UpdateOptions(providerName, options.DeepCopy());

        public void UpdateOptions(Action<ValidationOptions> optionAct, string providerName) => ValidationMe.UpdateOptions(providerName, optionAct);
    }
}