using System;
using System.Linq.Expressions;
using System.Reflection;
using Cosmos.Validation.Strategies;
using Cosmos.Validation.Validators;

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
        IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IFluentValidationRegistrar AndForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IFluentValidationRegistrar AndForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForValidator<TValidator>() where TValidator : CustomValidator, new();
        IFluentValidationRegistrar AndForValidator<TValidator, T>() where TValidator : CustomValidator<T>, new();
        IFluentValidationRegistrar AndForValidator(CustomValidator validator);
        IFluentValidationRegistrar AndForValidator<T>(CustomValidator<T> validator);
        void Build();
        ValidationHandler TempBuild();
    }

    public interface IFluentValidationRegistrar<T> : IFluentValidationRegistrar
    {
        new IValueFluentValidationRegistrar<T> ForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        new IValueFluentValidationRegistrar<T> ForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        new IValueFluentValidationRegistrar<T> ForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T, TVal> ForMember<TVal>(Expression<Func<T, TVal>> expression, ValueRuleMode mode = ValueRuleMode.Append);
    }
}