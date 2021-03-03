using System;
using System.Reflection;
using Cosmos.Validation.Registrars.Interfaces;

namespace Cosmos.Validation.Registrars
{
    public interface IFluentValidationRegistrar :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayRegisterForMember,
        IMayBuild,
        IMayTempBuild
    {
        string Name { get; }
        bool IsAnonymous { get; }
        Type SourceType { get; }
    }

    public interface IFluentValidationRegistrar<T> : IFluentValidationRegistrar, 
        IMayRegisterForMember<T>
    {
        new IValueFluentValidationRegistrar<T> ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        new IValueFluentValidationRegistrar<T> ForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        new IValueFluentValidationRegistrar<T> ForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
    }
}