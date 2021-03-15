using System;
using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Validators;

// ReSharper disable MemberCanBePrivate.Global

namespace Cosmos.Validation
{
    /// <summary>
    /// Abstract Validation Provider.
    /// </summary>
    public abstract class AbstractValidationProvider : IValidationProvider, ICorrectProvider
    {
        protected readonly IValidationProjectManager _projectManager;
        protected readonly IVerifiableObjectResolver _objectResolver;
        protected readonly ICustomValidatorManager _customValidatorManager;

        protected ValidationOptions _options;

        static AbstractValidationProvider()
        {
#if !NETFRAMEWORK
            NatashaInitializer.InitializeAndPreheating();
#endif
        }

        protected AbstractValidationProvider(
            IValidationProjectManager projectManager,
            IVerifiableObjectResolver objectResolver,
            ValidationOptions options)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _customValidatorManager = new CustomValidatorManager();
            _options = options ?? new ValidationOptions();
        }

        string ICorrectProvider.Name { get; set; }

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual IValidator Resolve(Type type)
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
            return TypeVisit.CreateInstance<IValidator>(v, _projectManager, _objectResolver, this, _options);
#endif
        }

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual IValidator Resolve(Type type, string name)
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
            return TypeVisit.CreateInstance<IValidator>(v, name, _projectManager, _objectResolver, this, _options);
#endif
        }

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IValidator<T> Resolve<T>() => (IValidator<T>) Resolve(typeof(T));

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IValidator<T> Resolve<T>(string name) => (IValidator<T>) Resolve(typeof(T), name);

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
        public virtual void UpdateOptions(ValidationOptions options)
        {
            if (options is not null)
                _options = options;
        }

        /// <summary>
        /// Update the configuration of the validator.
        /// </summary>
        /// <param name="optionAct"></param>
        public virtual void UpdateOptions(Action<ValidationOptions> optionAct)
        {
            optionAct?.Invoke(_options);
        }
    }
}