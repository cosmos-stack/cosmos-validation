using System;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayRegisterForType
    {
        IFluentValidationRegistrar ForType(Type type);
        IFluentValidationRegistrar ForType(Type type, string name);
        IFluentValidationRegistrar<T> ForType<T>();
        IFluentValidationRegistrar<T> ForType<T>(string name);
    }

    public interface IMayContinueRegisterForType
    {
        IFluentValidationRegistrar AndForType(Type type);
        IFluentValidationRegistrar AndForType(Type type, string name);
        IFluentValidationRegistrar<T> AndForType<T>();
        IFluentValidationRegistrar<T> AndForType<T>(string name);
    }

    public interface IMayContinueRegisterForType<T>
    {
        
    }
}