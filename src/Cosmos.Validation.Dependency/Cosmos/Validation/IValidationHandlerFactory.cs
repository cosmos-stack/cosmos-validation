namespace Cosmos.Validation
{
    public interface IValidationHandlerFactory
    {
        ValidationHandlerBuilder CreateBuilder();
    }
}