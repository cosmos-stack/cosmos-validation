namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Range registrar extensions <br />
    /// 区间注册扩展
    /// </summary>
    public static class RangeRegistrarExtensions
    {
        /// <summary>
        /// Range... from .. to .. <br />
        /// 位于区间
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Range(this IValueFluentValidationRegistrar registrar, object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            registrar._impl().ExposeValueRuleBuilder().Range(from, to, options);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Range... (from .. to ..) <br />
        /// 位于开区间
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RangeWithOpenInterval(this IValueFluentValidationRegistrar registrar, object from, object to)
        {
            registrar._impl().ExposeValueRuleBuilder().RangeWithOpenInterval(from, to);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Range... [from .. to ..] <br />
        /// 位于闭区间
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar RangeWithCloseInterval(this IValueFluentValidationRegistrar registrar, object from, object to)
        {
            registrar._impl().ExposeValueRuleBuilder().RangeWithCloseInterval(from, to);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Range... from .. to .. <br />
        /// 位于区间
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="options"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Range<T>(this IValueFluentValidationRegistrar<T> registrar, object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            registrar._impl().ExposeValueRuleBuilder().Range(from, to, options);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Range... (from .. to ..) <br />
        /// 位于开区间
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RangeWithOpenInterval<T>(this IValueFluentValidationRegistrar<T> registrar, object from, object to)
        {
            registrar._impl().ExposeValueRuleBuilder().RangeWithOpenInterval(from, to);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Range... [from .. to ..] <br />
        /// 位于闭区间
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> RangeWithCloseInterval<T>(this IValueFluentValidationRegistrar<T> registrar, object from, object to)
        {
            registrar._impl().ExposeValueRuleBuilder().RangeWithCloseInterval(from, to);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Range... from .. to .. <br />
        /// 位于区间
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="options"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Range<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            registrar._impl().ExposeValueRuleBuilder2().Range(from, to, options);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Range... (from .. to ..) <br />
        /// 位于开区间
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RangeWithOpenInterval<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal from, TVal to)
        {
            registrar._impl().ExposeValueRuleBuilder2().RangeWithOpenInterval(from, to);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Range... [from .. to ..] <br />
        /// 位于闭区间
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> RangeWithCloseInterval<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, TVal from, TVal to)
        {
            registrar._impl().ExposeValueRuleBuilder2().RangeWithCloseInterval(from, to);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }
    }
}