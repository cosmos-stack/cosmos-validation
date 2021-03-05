using Cosmos.Validation.Registrars.Interfaces;

namespace Cosmos.Validation.Registrars
{
    public interface IWaitForMessageValidationRegistrar :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect<IValueFluentValidationRegistrar>
    {
        IValueFluentValidationRegistrar WithMessage(string message);
    }

    public interface IWaitForMessageValidationRegistrar<T> :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember<T>,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect<IValueFluentValidationRegistrar<T>>
    {
        IValueFluentValidationRegistrar<T> WithMessage(string message);
    }

    public interface IWaitForMessageValidationRegistrar<T, TVal> :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember<T>,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect<IValueFluentValidationRegistrar<T, TVal>>
    {
        IValueFluentValidationRegistrar<T, TVal> WithMessage(string message);
    }
}