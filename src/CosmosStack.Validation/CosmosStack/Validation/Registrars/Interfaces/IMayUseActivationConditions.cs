using System;

namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May use activation conditions interface <br />
    /// 标记可使用条件
    /// </summary>
    public interface IMayUseActivationConditions
    {
        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar When(Func<object, bool> condition);

        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar When(Func<object, object, bool> condition);

        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar Unless(Func<object, bool> condition);

        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar Unless(Func<object, object, bool> condition);
    }

    /// <summary>
    /// May use activation conditions interface <br />
    /// 标记可使用条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMayUseActivationConditions<T>
    {
        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> When(Func<object, bool> condition);

        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> When(Func<T, object, bool> condition);

        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> Unless(Func<object, bool> condition);
        
        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T> Unless(Func<T, object, bool> condition);
    }

    /// <summary>
    /// May use activation conditions interface <br />
    /// 标记可使用条件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IMayUseActivationConditions<T, TVal>
    {
        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> When(Func<TVal, bool> condition);

        /// <summary>
        /// When <br />
        /// 当
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> When(Func<T, TVal, bool> condition);

        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> Unless(Func<TVal, bool> condition);
        
        /// <summary>
        /// Unless <br />
        /// 除非
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        IValueFluentValidationRegistrar<T, TVal> Unless(Func<T, TVal, bool> condition);
    }
}