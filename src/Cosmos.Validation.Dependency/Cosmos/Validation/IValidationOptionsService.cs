using System;

namespace Cosmos.Validation
{
    public interface IValidationOptionsService
    {
        ValidationOptions GetDefaultOptions();
        
        void ReplaceDefaultOptions(ValidationOptions options);

        void UpdateDefaultOptions(Action<ValidationOptions> optionAct);

        ValidationOptions GetMainOptions();

        void ReplaceMainOptions(ValidationOptions options);

        void UpdateMainOptions(Action<ValidationOptions> optionAct);

        ValidationOptions GetOptions();

        void ReplaceOptions(ValidationOptions options);

        void UpdateOptions(Action<ValidationOptions> optionAct);

        ValidationOptions GetOptions(string providerName);

        void ReplaceOptions(ValidationOptions options, string providerName);

        void UpdateOptions(Action<ValidationOptions> optionAct, string providerName);
    }
}