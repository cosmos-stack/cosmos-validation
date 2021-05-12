using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayRegisterForMember
    {
        IValueFluentValidationRegistrar ForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar ForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar ForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
    }

    public interface IMayRegisterForMember<T>
    {
        IValueFluentValidationRegistrar<T> ForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar<T> ForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar<T> ForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar<T, TVal> ForMember<TVal>(Expression<Func<T, TVal>> expression, VerifyRuleMode mode = VerifyRuleMode.Append);
    }

    public interface IMayContinueRegisterForMember
    {
        IValueFluentValidationRegistrar AndForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar AndForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar AndForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
    }

    public interface IMayContinueRegisterForMember<T>
    {
        IValueFluentValidationRegistrar<T> AndForMember(string memberName, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar<T> AndForMember(PropertyInfo propertyInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar<T> AndForMember(FieldInfo fieldInfo, VerifyRuleMode mode = VerifyRuleMode.Append);
        IValueFluentValidationRegistrar<T, TVal> AndForMember<TVal>(Expression<Func<T, TVal>> expression, VerifyRuleMode mode = VerifyRuleMode.Append);
    }
}