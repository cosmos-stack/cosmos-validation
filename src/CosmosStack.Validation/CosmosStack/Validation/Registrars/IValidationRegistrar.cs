using CosmosStack.Validation.Registrars.Interfaces;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Validation register interface <br />
    /// 验证注册器接口
    /// </summary>
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