namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayTakeEffect<out TCallbackEntry>
    {
        TCallbackEntry TakeEffect();
    }
}