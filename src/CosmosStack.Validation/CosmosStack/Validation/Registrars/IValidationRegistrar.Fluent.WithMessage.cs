using CosmosStack.Validation.Registrars.Interfaces;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// The lower-level interface of ValidationRegistrar. Used to wait for verification information. <br />
    /// 带信息的验证注册器接口
    /// </summary>
    public interface IWaitForMessageValidationRegistrar :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember,
        IMayUseRuleConditions,
        IMayUseActivationConditions,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect,
        IMayExposeRulePackageForType,
        IMayExposeUnregisteredRulePackageForType
    {
        /// <summary>
        /// Fill in the operation name. <br />
        /// 填入操作名称
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IWaitForMessageValidationRegistrar WithName(string operationName);

        /// <summary>
        /// Fill in the message. <br />
        /// 填入信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValidationRegistrar WithMessage(string message);
    }

    /// <summary>
    /// The lower-level interface of ValidationRegistrar. Used to wait for verification information. <br />
    /// 带信息的验证注册器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IWaitForMessageValidationRegistrar<T> :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember<T>,
        IMayUseRuleConditions<T>,
        IMayUseActivationConditions<T>,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect,
        IMayExposeRulePackageForType,
        IMayExposeUnregisteredRulePackageForType
    {
        /// <summary>
        /// Fill in the operation name. <br />
        /// 填入操作名称
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IWaitForMessageValidationRegistrar<T> WithName(string operationName);

        /// <summary>
        /// Fill in the message. <br />
        /// 填入信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValidationRegistrar<T> WithMessage(string message);
    }

    /// <summary>
    /// The lower-level interface of ValidationRegistrar. Used to wait for verification information. <br />
    /// 带信息的验证注册器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IWaitForMessageValidationRegistrar<T, TVal> :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember<T>,
        IMayUseRuleConditions<T, TVal>,
        IMayUseActivationConditions<T, TVal>,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect,
        IMayExposeRulePackageForType,
        IMayExposeUnregisteredRulePackageForType
    {
        /// <summary>
        /// Fill in the operation name. <br />
        /// 填入操作名称
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IWaitForMessageValidationRegistrar<T, TVal> WithName(string operationName);

        /// <summary>
        /// Fill in the message. <br />
        /// 填入信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValidationRegistrar<T, TVal> WithMessage(string message);
    }
}