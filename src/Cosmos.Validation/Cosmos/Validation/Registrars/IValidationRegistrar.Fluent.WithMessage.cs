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
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect
    {
        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar WithMessage(string message);
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
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect
    {
        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> WithMessage(string message);
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
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect
    {
        /// <summary>
        /// Fill in the message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> WithMessage(string message);
    }
}