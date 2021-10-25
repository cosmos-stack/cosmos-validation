namespace CosmosStack.Validation
{
    public interface IValidationHandlerFactory
    {
        ValidationHandlerBuilder CreateBuilder();
    }
}