using System;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Function Registrar Extensions <br />
    /// 函数注册扩展
    /// </summary>
    public static class FuncRegistrarExtensions
    {
        /// <summary>
        /// Func <br />
        /// 注册使用函数
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Func(this IValueFluentValidationRegistrar registrar, Func<object, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Func(func);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Func <br />
        /// 注册使用函数
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar Func(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage(impl, impl.ExposeRoot(), func);
        }

        /// <summary>
        /// Func <br />
        /// 注册使用函数
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Func<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Func(func);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Func <br />
        /// 注册使用函数
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar<T> Func<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T>(impl, impl.ExposeRoot(), func);
        }

        /// <summary>
        /// Func <br />
        /// 注册使用函数
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Func<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Func(func);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Func <br />
        /// 注册使用函数
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar<T, TVal> Func<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T, TVal>(impl, impl.ExposeRoot(), func);
        }

        /// <summary>
        /// Predicate <br />
        /// 注册使用条件
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar Predicate(this IValueFluentValidationRegistrar registrar, Predicate<object> predicate)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage(impl, impl.ExposeRoot(), predicate);
        }

        /// <summary>
        /// Predicate <br />
        /// 注册使用条件
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar<T> Predicate<T>(this IValueFluentValidationRegistrar<T> registrar, Predicate<object> predicate)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T>(impl, impl.ExposeRoot(), predicate);
        }

        /// <summary>
        /// Predicate <br />
        /// 注册使用条件
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar<T, TVal> Predicate<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Predicate<TVal> predicate)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T, TVal>(impl, impl.ExposeRoot(), predicate);
        }

        /// <summary>
        /// Must <br />
        /// 表示必须满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Must(this IValueFluentValidationRegistrar registrar, Func<object, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Must(func);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Must <br />
        /// 表示必须满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar Must(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage(impl, impl.ExposeRoot(), func);
        }

        /// <summary>
        /// Must <br />
        /// 表示必须满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Must<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder().Must(func);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Must <br />
        /// 表示必须满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar<T> Must<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T>(impl, impl.ExposeRoot(), func);
        }

        /// <summary>
        /// Must <br />
        /// 表示必须满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Must<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, CustomVerifyResult> func)
        {
            registrar._impl().ExposeValueRuleBuilder2().Must(func);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Must <br />
        /// 表示必须满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar<T, TVal> Must<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T, TVal>(impl, impl.ExposeRoot(), func);
        }

        /// <summary>
        /// Satisfies <br />
        /// 表示需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar Satisfies(this IValueFluentValidationRegistrar registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage(impl, impl.ExposeRoot(), func);
        }

        /// <summary>
        /// Satisfies <br />
        /// 表示需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Satisfies(this IValueFluentValidationRegistrar registrar, Func<object, bool> func, string message)
        {
            return Satisfies(registrar, func).WithMessage(message);
        }

        /// <summary>
        /// Satisfies <br />
        /// 表示需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar<T> Satisfies<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T>(impl, impl.ExposeRoot(), func);
        }

        /// <summary>
        /// Satisfies <br />
        /// 表示需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Satisfies<T>(this IValueFluentValidationRegistrar<T> registrar, Func<object, bool> func, string message)
        {
            return Satisfies(registrar, func).WithMessage(message);
        }

        /// <summary>
        /// Satisfies <br />
        /// 表示需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IWaitForMessageValidationRegistrar<T, TVal> Satisfies<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, bool> func)
        {
            var impl = registrar._impl();
            return new ValidationRegistrarWithMessage<T, TVal>(impl, impl.ExposeRoot(), func);
        }

        /// <summary>
        /// Satisfies <br />
        /// 表示需要满足
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="func"></param>
        /// <param name="message"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Satisfies<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, Func<TVal, bool> func, string message)
        {
            return Satisfies(registrar, func).WithMessage(message);
        }
    }
}