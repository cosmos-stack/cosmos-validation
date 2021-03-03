using Cosmos.Validation.Registrars.Interfaces;

namespace Cosmos.Validation.Registrars
{
    public interface IWaitForMessageValidationRegistrar :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember,
        IMayBuild,
        IMayTempBuild
    {
        IValueFluentValidationRegistrar WithMessage(string message);
    }

    public interface IWaitForMessageValidationRegistrar<T> :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember<T>,
        IMayBuild,
        IMayTempBuild
    {
        IValueFluentValidationRegistrar<T> WithMessage(string message);
    }

    public interface IWaitForMessageValidationRegistrar<T, TVal> :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember<T>,
        IMayBuild,
        IMayTempBuild
    {
        IValueFluentValidationRegistrar<T, TVal> WithMessage(string message);
    }
}