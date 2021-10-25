using System;
using System.Collections.Generic;
using CosmosStack.Reflection;
using CosmosStack.Validation.Internals;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Projects;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation
{
    /// <summary>
    /// Validation Provider <br />
    /// 验证服务提供者程序
    /// </summary>
    public class ValidationProvider : IValidationProvider, ICorrectProvider
    {
        private readonly IValidationProjectManager _projectManager;
        private readonly IVerifiableObjectResolver _objectResolver;
        private readonly ICustomValidatorManager _customValidatorManager;

        private ValidationOptions _options;

        static ValidationProvider()
        {
#if !NETFRAMEWORK
            NatashaInitializer.InitializeAndPreheating();
#endif
        }

        /// <summary>
        /// Create an instance of ValidationProvider.
        /// </summary>
        /// <param name="projectManager"></param>
        /// <param name="objectResolver"></param>
        /// <param name="options"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public ValidationProvider(
            IValidationProjectManager projectManager,
            IVerifiableObjectResolver objectResolver,
            ValidationOptions options)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _customValidatorManager = new CustomValidatorManager();
            _options = options ?? new ValidationOptions();
        }

        internal const string DefaultName = "Default Validation Provider";
        internal const string MainName = "Main Validation Provider";

        internal static bool IsDefault(string name) => name == DefaultName;

        string ICorrectProvider.Name { get; set; } = DefaultName;

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IValidator Resolve(Type type)
        {
            var d = typeof(AggregationValidator<>);
            var v = d.MakeGenericType(type);
#if !NETFRAMEWORK
            var args = new List<ArgumentDescriptor>
            {
                new("projectManager", _projectManager, typeof(IValidationProjectManager)),
                new("objectResolver", _objectResolver, typeof(IVerifiableObjectResolver)),
                new("customValidatorManager", _customValidatorManager, typeof(ICustomValidatorManager)),
                new("options", _options, typeof(ValidationOptions))
            };
            
            return TypeVisit.CreateInstance<IValidator>(v, args);
#else
            return TypeVisit.CreateInstance<IValidator>(v, _projectManager, _objectResolver, _customValidatorManager, _options);
#endif
        }

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IValidator Resolve(Type type, string name)
        {
            var d = typeof(AggregationValidator<>);
            var v = d.MakeGenericType(type);
#if !NETFRAMEWORK
            var args = new List<ArgumentDescriptor>
            {
                new("name", name, typeof(string)),
                new("projectManager", _projectManager, typeof(IValidationProjectManager)),
                new("objectResolver", _objectResolver, typeof(IVerifiableObjectResolver)),
                new("customValidatorManager", _customValidatorManager, typeof(ICustomValidatorManager)),
                new("options", _options, typeof(ValidationOptions))
            };
            
            return TypeVisit.CreateInstance<IValidator>(v, args);
#else
            return TypeVisit.CreateInstance<IValidator>(v, name, _projectManager, _objectResolver, _customValidatorManager, _options);
#endif
        }

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IValidator<T> Resolve<T>() => (IValidator<T>) Resolve(typeof(T));

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IValidator<T> Resolve<T>(string name) => (IValidator<T>) Resolve(typeof(T), name);

        /// <inheritdoc />
        IValidationProjectManager ICorrectProvider.ExposeProjectManager() => _projectManager;

        /// <inheritdoc />
        IVerifiableObjectResolver ICorrectProvider.ExposeObjectResolver() => _objectResolver;

        /// <inheritdoc />
        ICustomValidatorManager ICorrectProvider.ExposeCustomValidatorManager() => _customValidatorManager;

        /// <inheritdoc />
        ValidationOptions ICorrectProvider.ExposeValidationOptions() => _options;

        /// <summary>
        /// Override the configuration of the validator.
        /// </summary>
        /// <param name="options"></param>
        public void UpdateOptions(ValidationOptions options)
        {
            if (options is not null)
                _options = options;
        }

        /// <summary>
        /// Update the configuration of the validator.
        /// </summary>
        /// <param name="optionAct"></param>
        public void UpdateOptions(Action<ValidationOptions> optionAct)
        {
            optionAct?.Invoke(_options);
        }
    }
}