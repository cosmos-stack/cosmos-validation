using System;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayUseActivationConditions
    {
        IValueFluentValidationRegistrar When(Func<object, bool> condition);
        IValueFluentValidationRegistrar When(Func<object, object, bool> condition);
        IValueFluentValidationRegistrar Unless(Func<object, bool> condition);
        IValueFluentValidationRegistrar Unless(Func<object, object, bool> condition);
    }

    public interface IMayUseActivationConditions<T>
    {
        IValueFluentValidationRegistrar<T> When(Func<object, bool> condition);
        IValueFluentValidationRegistrar<T> When(Func<T, object, bool> condition);
        IValueFluentValidationRegistrar<T> Unless(Func<object, bool> condition);
        IValueFluentValidationRegistrar<T> Unless(Func<T, object, bool> condition);
    }

    public interface IMayUseActivationConditions<T, TVal>
    {
        IValueFluentValidationRegistrar<T, TVal> When(Func<TVal, bool> condition);
        IValueFluentValidationRegistrar<T, TVal> When(Func<T, TVal, bool> condition);
        IValueFluentValidationRegistrar<T, TVal> Unless(Func<TVal, bool> condition);
        IValueFluentValidationRegistrar<T, TVal> Unless(Func<T, TVal, bool> condition);
    }
}