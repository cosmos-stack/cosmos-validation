// ReSharper disable InconsistentNaming
namespace Cosmos.Validation.Registrars
{
    public interface IPredicateValidationRegistrar : IValueFluentValidationRegistrar { }

    public interface IPredicateValidationRegistrar<T> : IValueFluentValidationRegistrar<T> { }

    public interface IPredicateValidationRegistrar<T, TVal> : IValueFluentValidationRegistrar<T, TVal> { }
}