using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayRegisterForMember
    {
        IValueFluentValidationRegistrar ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar ForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar ForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
    }

    public interface IMayRegisterForMember<T>
    {
        IValueFluentValidationRegistrar<T> ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T> ForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T> ForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T, TVal> ForMember<TVal>(Expression<Func<T, TVal>> expression, ValueRuleMode mode = ValueRuleMode.Append);
    }

    public interface IMayContinueRegisterForMember
    {
        IValueFluentValidationRegistrar AndForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar AndForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar AndForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
    }

    public interface IMayContinueRegisterForMember<T>
    {
        IValueFluentValidationRegistrar<T> AndForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T> AndForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T> AndForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T, TVal> AndForMember<TVal>(Expression<Func<T, TVal>> expression, ValueRuleMode mode = ValueRuleMode.Append);
    }
}