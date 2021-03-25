using System;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayUseActivationConditions
    {
        IValueFluentValidationRegistrar When(Func<object, bool> condition);

        IValueFluentValidationRegistrar Unless(Func<object, bool> condition);
    }

    public interface IMayUseActivationConditions<T>
    {
        IValueFluentValidationRegistrar<T> When(Func<object, bool> condition);

        IValueFluentValidationRegistrar<T> Unless(Func<object, bool> condition);
    }

    public interface IMayUseActivationConditions<T, TVal>
    {
        IValueFluentValidationRegistrar<T, TVal> When(Func<TVal, bool> condition);

        IValueFluentValidationRegistrar<T, TVal> Unless(Func<TVal, bool> condition);
    }
}