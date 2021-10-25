using System;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Projects;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Internals
{
    /// <summary>
    /// A scoped validation provider
    /// </summary>
    internal class ValidationScope : IValidationProvider, ICorrectProvider
    {
        private readonly IValidationProvider _validationProvider;

        private ICorrectProvider InnerPtr => (ICorrectProvider) _validationProvider;

        internal ValidationScope(IValidationProvider validationProvider, string name)
        {
            _validationProvider = validationProvider ?? throw new ArgumentNullException(nameof(validationProvider));
            ((ICorrectProvider) this).Name = name;
        }

        /// <inheritdoc />
        string ICorrectProvider.Name { get; set; }

        /// <summary>
        /// Get name of validation provider
        /// </summary>
        /// <returns></returns>
        public string GetName() => ((ICorrectProvider) this).Name;

        internal IValidationProvider ExposeValidationProvider() => _validationProvider;

        /// <inheritdoc />
        IValidationProjectManager ICorrectProvider.ExposeProjectManager() => InnerPtr.ExposeProjectManager();

        /// <inheritdoc />
        IVerifiableObjectResolver ICorrectProvider.ExposeObjectResolver() => InnerPtr.ExposeObjectResolver();

        /// <inheritdoc />
        ICustomValidatorManager ICorrectProvider.ExposeCustomValidatorManager() => InnerPtr.ExposeCustomValidatorManager();

        /// <inheritdoc />
        ValidationOptions ICorrectProvider.ExposeValidationOptions() => InnerPtr.ExposeValidationOptions();

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IValidator Resolve(Type type) => _validationProvider.Resolve(type);

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IValidator Resolve(Type type, string name) => _validationProvider.Resolve(type, name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IValidator<T> Resolve<T>() => _validationProvider.Resolve<T>();

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IValidator<T> Resolve<T>(string name) => _validationProvider.Resolve<T>(name);

        /// <summary>
        /// Override the configuration of the validator.
        /// </summary>
        /// <param name="options"></param>
        void IValidationProvider.UpdateOptions(ValidationOptions options) { }

        /// <summary>
        /// Update the configuration of the validator.
        /// </summary>
        /// <param name="optionAct"></param>
        void IValidationProvider.UpdateOptions(Action<ValidationOptions> optionAct) { }
    }
}