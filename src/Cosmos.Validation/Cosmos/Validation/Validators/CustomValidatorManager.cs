﻿using System;
using System.Collections.Generic;
using Cosmos.Collections;

namespace Cosmos.Validation.Validators
{
    internal class CustomValidatorManager
    {
        private readonly Dictionary<string, CustomValidator> _validators = new();

        public void Register<TValidator>() where TValidator : CustomValidator, new()
        {
            Register(new TValidator());
        }

        public void Register<TValidator, T>() where TValidator : CustomValidator<T>, new()
        {
            Register(new TValidator());
        }

        public void Register(CustomValidator validator)
        {
            if (validator is null)
                throw new ArgumentNullException(nameof(validator));

            if (_validators.ContainsKey(validator.Name))
                return;

            _validators.AddValueIfNotExist(validator.Name, validator);
        }

        public void Register<T>(CustomValidator<T> validator)
        {
            if (validator is null)
                throw new ArgumentNullException(nameof(validator));

            if (_validators.ContainsKey(validator.Name))
                return;

            _validators.AddValueIfNotExist(validator.Name, validator);
        }

        public CustomValidator Resolve(string name)
        {
            if (_validators.TryGetValue(name, out var validator))
                return validator;
            return SealedValidator.Instance;
        }

        public CustomValidator<T> Resolve<T>(string name)
        {
            if (_validators.TryGetValue(name, out var v) && v is CustomValidator<T> validator)
                return validator;
            return SealedValidator<T>.Instance;
        }

        public IEnumerable<CustomValidator> ResolveAll() => _validators.Values;
    }
}