using System;
using System.Collections.Generic;

namespace Cosmos.Validation.Validators
{
    public interface ICustomValidatorManager
    {
        void Register<TValidator>() where TValidator : CustomValidator, new();

        void Register<TValidator, T>() where TValidator : CustomValidator<T>, new();

        void Register(CustomValidator validator);

        void Register<T>(CustomValidator<T> validator);

        CustomValidator Resolve(string name);

        CustomValidator<T> Resolve<T>(string name);

        IEnumerable<CustomValidator> ResolveAll();

        IEnumerable<CustomValidator> ResolveEmpty();

        IEnumerable<CustomValidator> ResolveBy(Func<CustomValidator, bool> filter);
    }
}