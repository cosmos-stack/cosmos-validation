using System;
using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation
{
    public abstract class AbstractValidationProvider : IValidationProvider, ICorrectProvider
    {
        private readonly IValidationProjectManager _projectManager;
        private readonly IValidationObjectResolver _objectResolver;
        private readonly CustomValidatorManager _customValidatorManager;

        private ValidationOptions _options;

        protected AbstractValidationProvider(
            IValidationProjectManager projectManager,
            IValidationObjectResolver objectResolver,
            ValidationOptions options)
        {
            _projectManager = projectManager ?? throw new ArgumentNullException(nameof(projectManager));
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _customValidatorManager = new CustomValidatorManager();
            _options = options ?? new ValidationOptions();
        }

        string ICorrectProvider.Name { get; set; }

        public virtual IValidator Resolve(Type type)
        {
            var d = typeof(AggregationValidator<>);
            var v = d.MakeGenericType(type);

#if NETFRAMEWORK
            return TypeVisit.CreateInstance<IValidator>(v, _projectManager, _objectResolver, _options);
#else
            var arguments = new List<ArgumentDescriptor>
            {
                new("projectManager", _projectManager, typeof(IValidationProjectManager)),
                new("objectResolver", _objectResolver, typeof(IValidationObjectResolver)),
                new("options", _options, typeof(ValidationOptions))
            };

            return TypeVisit.CreateInstance<IValidator>(v, arguments);
#endif
        }

        public virtual IValidator Resolve(Type type, string name)
        {
            var d = typeof(AggregationValidator<>);
            var v = d.MakeGenericType(type);
#if NETFRAMEWORK
            return TypeVisit.CreateInstance<IValidator>(v, name, _projectManager, _objectResolver, _options);
#else
            var arguments = new List<ArgumentDescriptor>
            {
                new("name", name, TypeClass.StringClazz),
                new("projectManager", _projectManager, typeof(IValidationProjectManager)),
                new("objectResolver", _objectResolver, typeof(IValidationObjectResolver)),
                new("options", _options, typeof(ValidationOptions))
            };

            return TypeVisit.CreateInstance<IValidator>(v, arguments);
#endif
        }

        public virtual IValidator<T> Resolve<T>() => (IValidator<T>) Resolve(typeof(T));

        public virtual IValidator<T> Resolve<T>(string name) => (IValidator<T>) Resolve(typeof(T), name);

        IValidationProjectManager ICorrectProvider.ExposeProjectManager()
        {
            return _projectManager;
        }

        IValidationObjectResolver ICorrectProvider.ExposeObjectResolver()
        {
            return _objectResolver;
        }

        CustomValidatorManager ICorrectProvider.ExposeCustomValidatorManager()
        {
            return _customValidatorManager;
        }

        ValidationOptions ICorrectProvider.ExposeValidationOptions()
        {
            return _options;
        }

        void ICorrectProvider.RegisterValidator<TValidator>()
        {
            _customValidatorManager.Register<TValidator>();
        }

        void ICorrectProvider.RegisterValidator<TValidator, T>()
        {
            _customValidatorManager.Register<TValidator, T>();
        }

        void ICorrectProvider.RegisterValidator(CustomValidator validator)
        {
            _customValidatorManager.Register(validator);
        }

        void ICorrectProvider.RegisterValidator<T>(CustomValidator<T> validator)
        {
            _customValidatorManager.Register(validator);
        }

        public virtual void UpdateOptions(ValidationOptions options)
        {
            if (options is not null)
                _options = options;
        }

        public virtual void UpdateOptions(Action<ValidationOptions> optionAct)
        {
            optionAct?.Invoke(_options);
        }
    }
}