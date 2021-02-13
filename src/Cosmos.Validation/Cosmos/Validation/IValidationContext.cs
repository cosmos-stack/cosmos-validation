using System;
using System.Linq.Expressions;
using System.Reflection;
using Cosmos.Validation.Strategies;

namespace Cosmos.Validation
{
    public interface IValidationContext
    {
        IValidationContext SetStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new();

        IValidationContext SetStrategy<TStrategy>(TStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new();

        IValidationContext ForMember(string name, Func<IValueRuleBuilder, IValueRuleBuilder> func);

        IValidationContext ForMember(PropertyInfo propertyInfo, Func<IValueRuleBuilder, IValueRuleBuilder> func);

        IValidationContext ForMember(FieldInfo fieldInfo, Func<IValueRuleBuilder, IValueRuleBuilder> func);
    }

    public interface IValidationContext<T> : IValidationContext
    {
        new IValidationContext<T> SetStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<T>, new();

        new IValidationContext<T> SetStrategy<TStrategy>(TStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<T>, new();

        IValidationContext<T> ForMember(string name, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func);

        IValidationContext<T> ForMember(PropertyInfo propertyInfo, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func);

        IValidationContext<T> ForMember(FieldInfo fieldInfo, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func);

        IValidationContext<T> ForMember<TVal>(Expression<Func<T, TVal>> expression, Func<IValueRuleBuilder<T, TVal>, IValueRuleBuilder<T, TVal>> func);

    }
}