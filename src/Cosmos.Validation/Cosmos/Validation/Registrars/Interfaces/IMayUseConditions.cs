namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayUseConditions
    {
        IValueFluentValidationRegistrar And();

        IValueFluentValidationRegistrar Or();
    }

    public interface IMayUseConditions<T>
    {
        IValueFluentValidationRegistrar<T> And();

        IValueFluentValidationRegistrar<T> Or();
    }

    public interface IMayUseConditions<T, TVal>
    {
        IValueFluentValidationRegistrar<T, TVal> And();

        IValueFluentValidationRegistrar<T, TVal> Or();
    }
}