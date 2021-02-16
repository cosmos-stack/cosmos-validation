namespace Cosmos.Validation.Registrars
{
    public interface IWaitForMessageValidationRegistrar
    {
        IValueFluentValidationRegistrar WithMessage(string message);
    }

    public interface IWaitForMessageValidationRegistrar<T>
    {
        IValueFluentValidationRegistrar<T> WithMessage(string message);
    }

    public interface IWaitForMessageValidationRegistrar<T, TVal>
    {
        IValueFluentValidationRegistrar<T, TVal> WithMessage(string message);
    }
}