using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Cosmos.Validation.Registrars
{
    public interface IFluentValidationRegistrar
    {
        string Name { get; }
        bool IsAnonymous { get; }
        Type SourceType { get; }
        IValueFluentValidationRegistrar ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar ForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar ForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IFluentValidationRegistrar AndForType(Type type);
        IFluentValidationRegistrar AndForType(Type type, string name);
        IFluentValidationRegistrar<T> AndForType<T>();
        IFluentValidationRegistrar<T> AndForType<T>(string name);
        void Build();
    }

    public interface IFluentValidationRegistrar<T> : IFluentValidationRegistrar
    {
        new IValueFluentValidationRegistrar<T> ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        new IValueFluentValidationRegistrar<T> ForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        new IValueFluentValidationRegistrar<T> ForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T,TVal> ForMember<TVal>(Expression<Func<T, TVal>> expression, ValueRuleMode mode = ValueRuleMode.Append);
    }
}