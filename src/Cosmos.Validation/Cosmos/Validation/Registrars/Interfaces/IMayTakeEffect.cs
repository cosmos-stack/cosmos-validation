namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayTakeEffect
    {
        void TakeEffect();
        IValidationRegistrar TakeEffectAndBack();
    }
}