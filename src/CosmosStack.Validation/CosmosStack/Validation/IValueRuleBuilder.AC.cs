using System;

// ReSharper disable InconsistentNaming

namespace CosmosStack.Validation
{
    /// <summary>
    /// An interface for ValueRueBuilder to wait for activation conditions. <br />
    /// 值验证规则构造器接口，此接口可用于期待一个可执行条件
    /// </summary>
    public interface IPredicateValueRuleBuilder : IValueRuleBuilder
    {
        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder When(Func<object, bool> condition);
        
        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder When(Func<object, object, bool> condition);
        
        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder Unless(Func<object, bool> condition);
        
        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder Unless(Func<object, object, bool> condition);
    }

    /// <summary>
    /// An interface for ValueRueBuilder to wait for activation conditions. <br />
    /// 值验证规则构造器接口，此接口可用于期待一个可执行条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPredicateValueRuleBuilder<T> : IValueRuleBuilder<T>
    {
        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder<T> When(Func<object, bool> condition);
        
        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder<T> When(Func<T, object, bool> condition);
        
        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder<T> Unless(Func<object, bool> condition);
        
        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder<T> Unless(Func<T, object, bool> condition);
    }

    /// <summary>
    /// An interface for ValueRueBuilder to wait for activation conditions. <br />
    /// 值验证规则构造器接口，此接口可用于期待一个可执行条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IPredicateValueRuleBuilder<T, TVal> : IValueRuleBuilder<T, TVal>
    {
        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder<T, TVal> When(Func<TVal, bool> condition);
        
        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder<T, TVal> When(Func<T, TVal, bool> condition);
        
        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder<T, TVal> Unless(Func<TVal, bool> condition);
        
        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueRuleBuilder<T, TVal> Unless(Func<T, TVal, bool> condition);
    }
}