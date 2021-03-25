// ReSharper disable InconsistentNaming

using Cosmos.Validation.Registrars.Interfaces;

namespace Cosmos.Validation.Registrars
{
    public interface IPredicateValidationRegistrar : IValueFluentValidationRegistrar, IMayUseActivationConditions { }

    public interface IPredicateValidationRegistrar<T> : IValueFluentValidationRegistrar<T>, IMayUseActivationConditions<T> { }

    public interface IPredicateValidationRegistrar<T, TVal> : IValueFluentValidationRegistrar<T, TVal>, IMayUseActivationConditions<T, TVal> { }
}