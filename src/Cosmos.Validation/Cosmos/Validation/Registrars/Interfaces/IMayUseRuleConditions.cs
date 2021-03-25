namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayUseRuleConditions
    {
        IValueFluentValidationRegistrar And();

        IValueFluentValidationRegistrar Or();
    }

    public interface IMayUseRuleConditions<T>
    {
        IValueFluentValidationRegistrar<T> And();

        IValueFluentValidationRegistrar<T> Or();
    }

    public interface IMayUseRuleConditions<T, TVal>
    {
        IValueFluentValidationRegistrar<T, TVal> And();

        IValueFluentValidationRegistrar<T, TVal> Or();
    }
}