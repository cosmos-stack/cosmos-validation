using Cosmos.Validation.Registrars.Interfaces;

namespace Cosmos.Validation.Registrars
{
    /// <summary>
    /// The lower-level interface of ValidationRegistrar. Used to wait for verification information.
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
        IMayTakeEffect
    {
        /// <summary>
        /// Fill in the operation name.
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IWaitForMessageValidationRegistrar WithName(string operationName);
        
        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValidationRegistrar WithMessage(string message);
    }

    /// <summary>
    /// The lower-level interface of ValidationRegistrar. Used to wait for verification information.
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
        IMayTakeEffect
    {
        /// <summary>
        /// Fill in the operation name.
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IWaitForMessageValidationRegistrar<T> WithName(string operationName);

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValidationRegistrar<T> WithMessage(string message);
    }

    /// <summary>
    /// The lower-level interface of ValidationRegistrar. Used to wait for verification information.
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
        IMayTakeEffect
    {
        /// <summary>
        /// Fill in the operation name.
        /// </summary>
        /// <param name="operationName"></param>
        /// <returns></returns>
        IWaitForMessageValidationRegistrar<T, TVal> WithName(string operationName);

        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IPredicateValidationRegistrar<T, TVal> WithMessage(string message);
    }
}