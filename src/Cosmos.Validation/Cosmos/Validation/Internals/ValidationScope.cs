using System;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Internals
{
    internal class ValidationScope : IValidationProvider, ICorrectProvider
    {
        private readonly IValidationProvider _validationProvider;

        private ICorrectProvider InnerPtr => (ICorrectProvider) _validationProvider;

        internal ValidationScope(IValidationProvider validationProvider, string name)
        {
            _validationProvider = validationProvider ?? throw new ArgumentNullException(nameof(validationProvider));
            ((ICorrectProvider) this).Name = name;
        }

        string ICorrectProvider.Name { get; set; }

        public string GetName() => ((ICorrectProvider) this).Name;

        internal IValidationProvider ExposeValidationProvider() => _validationProvider;

        IValidationProjectManager ICorrectProvider.ExposeProjectManager() => InnerPtr.ExposeProjectManager();

        IValidationObjectResolver ICorrectProvider.ExposeObjectResolver() => InnerPtr.ExposeObjectResolver();

        CustomValidatorManager ICorrectProvider.ExposeCustomValidatorManager() => InnerPtr.ExposeCustomValidatorManager();

        ValidationOptions ICorrectProvider.ExposeValidationOptions() => InnerPtr.ExposeValidationOptions();

        void ICorrectProvider.RegisterValidator<TValidator>() => InnerPtr.RegisterValidator<TValidator>();

        void ICorrectProvider.RegisterValidator<TValidator, T>() => InnerPtr.RegisterValidator<TValidator, T>();

        void ICorrectProvider.RegisterValidator(CustomValidator validator) => InnerPtr.RegisterValidator(validator);

        void ICorrectProvider.RegisterValidator<T>(CustomValidator<T> validator) => InnerPtr.RegisterValidator(validator);

        public IValidator Resolve(Type type) => _validationProvider.Resolve(type);

        public IValidator Resolve(Type type, string name) => _validationProvider.Resolve(type, name);

        public IValidator Resolve<T>() => _validationProvider.Resolve<T>();

        public IValidator Resolve<T>(string name) => _validationProvider.Resolve<T>(name);

        void IValidationProvider.UpdateOptions(ValidationOptions options) { }

        void IValidationProvider.UpdateOptions(Action<ValidationOptions> optionAct) { }
    }
}