using Cosmos.Validation.Registrars.Interfaces;

namespace Cosmos.Validation.Registrars
{
    public interface IValidationRegistrar :
        IMayRegisterForStrategy,
        IMayRegisterForCustomValidator,
        IMayRegisterForType,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect,
        IMayRegisterRulePackage,
        IMayExposeRulePackage,
        IMayExposeUnregisteredRulePackage { }
}