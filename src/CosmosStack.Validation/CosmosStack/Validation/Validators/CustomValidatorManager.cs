using System;
using System.Collections.Generic;
using System.Linq;
using CosmosStack.Collections;

namespace CosmosStack.Validation.Validators
{
    /// <summary>
    /// Custom Validator Manager <br />
    /// 自定义验证器管理器
    /// </summary>
    public class CustomValidatorManager : ICustomValidatorManager
    {
        //For all normal CustomValidator/CustomValidator<T>
        private readonly Dictionary<string, CustomValidator> _validators = new();

        #region Register

        /// <inheritdoc />
        public void Register<TValidator>() where TValidator : CustomValidator, new()
        {
            Register(new TValidator());
        }

        /// <inheritdoc />
        public void Register<TValidator, T>() where TValidator : CustomValidator<T>, new()
        {
            Register(new TValidator());
        }

        /// <inheritdoc />
        public void Register(CustomValidator validator)
        {
            if (validator is null)
                throw new ArgumentNullException(nameof(validator));
            RegisterImpl(validator);
        }

        /// <inheritdoc />
        public void Register<T>(CustomValidator<T> validator)
        {
            if (validator is null)
                throw new ArgumentNullException(nameof(validator));
            RegisterImpl(validator);
        }

        #endregion

        #region RegisterImpl

        private void RegisterImpl(CustomValidator validator)
        {
            if (_validators.ContainsKey(validator.Name))
                return;
            _validators.AddValueIfNotExist(validator.Name, validator);
        }

        #endregion

        #region Resolve

        /// <inheritdoc />
        public CustomValidator Resolve(string name)
        {
            if (_validators.TryGetValue(name, out var validator))
                return validator;
            return SealedValidator.Instance;
        }

        /// <inheritdoc />
        public CustomValidator<T> Resolve<T>(string name)
        {
            if (_validators.TryGetValue(name, out var v) && v is CustomValidator<T> validator)
                return validator;
            return SealedValidator<T>.Instance;
        }

        /// <inheritdoc />
        public IEnumerable<CustomValidator> ResolveAll() => _validators.Values;

        /// <inheritdoc />
        public IEnumerable<CustomValidator> ResolveEmpty() => Enumerable.Empty<CustomValidator>();

        /// <inheritdoc />
        public IEnumerable<CustomValidator> ResolveBy(Func<CustomValidator, bool> filter) => _validators.Values.Where(filter);

        #endregion
    }
}