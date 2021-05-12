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
        IMayTempBuild,
        IMayTakeEffect,
        IMayContinueImposeRulePackage,
        IMayExposeRulePackageForType,
        IMayExposeUnregisteredRulePackageForType
    {
        string Name { get; }
        bool IsAnonymous { get; }
        Type SourceType { get; }
    }

    public interface IFluentValidationRegistrar<T> : IFluentValidationRegistrar,
        IMayRegisterForMember<T>
    {
        new IValueFluentValidationRegistrar<T> ForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);
        new IValueFluentValidationRegistrar<T> ForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
        new IValueFluentValidationRegistrar<T> ForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}