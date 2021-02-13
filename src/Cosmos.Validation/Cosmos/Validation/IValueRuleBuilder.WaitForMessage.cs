namespace Cosmos.Validation
{
    public interface IWaitForMessageValueRuleBuilder
    {
        IValueRuleBuilder WithMessage(string message);
    }

    public interface IWaitForMessageValueRuleBuilder<T>
    {
        IValueRuleBuilder<T> WithMessage(string message);
    }

    public interface IWaitForMessageValueRuleBuilder<T, TVal>
    {
        IValueRuleBuilder<T, TVal> WithMessage(string message);
    }
}