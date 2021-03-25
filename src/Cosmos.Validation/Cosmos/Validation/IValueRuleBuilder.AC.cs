using System;

// ReSharper disable InconsistentNaming

namespace Cosmos.Validation
{
    /// <summary>
    /// An interface for ValueRueBuilder to wait for activation conditions.
    /// </summary>
    public interface IPredicateValueRuleBuilder : IValueRuleBuilder
    {
        IValueRuleBuilder When(Func<object, bool> condition);
        IValueRuleBuilder Unless(Func<object, bool> condition);
    }

    /// <summary>
    /// An interface for ValueRueBuilder to wait for activation conditions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPredicateValueRuleBuilder<T> : IValueRuleBuilder<T>
    {
        IValueRuleBuilder<T> When(Func<object, bool> condition);
        IValueRuleBuilder<T> Unless(Func<object, bool> condition);
    }

    /// <summary>
    /// An interface for ValueRueBuilder to wait for activation conditions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IPredicateValueRuleBuilder<T, TVal> : IValueRuleBuilder<T, TVal>
    {
        IValueRuleBuilder<T, TVal> When(Func<TVal, bool> condition);
        IValueRuleBuilder<T, TVal> Unless(Func<TVal, bool> condition);
    }
}