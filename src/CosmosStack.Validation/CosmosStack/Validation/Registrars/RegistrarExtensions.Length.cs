namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Length Registrar Extensions <br />
    /// 长度注册扩展
    /// </summary>
    public static class LengthRegistrarExtensions
    {
        /// <summary>
        /// Length <br />
        /// 应长
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar Length(this IValueFluentValidationRegistrar registrar, int min, int max)
        {
            registrar._impl().ExposeValueRuleBuilder().Length(min, max);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Min length <br />
        /// 至少长
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="min"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar MinLength(this IValueFluentValidationRegistrar registrar, int min)
        {
            registrar._impl().ExposeValueRuleBuilder().MinLength(min);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Max length <br />
        /// 至多长
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar MaxLength(this IValueFluentValidationRegistrar registrar, int max)
        {
            registrar._impl().ExposeValueRuleBuilder().MaxLength(max);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// As least <br />
        /// 至少存在
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar AtLeast(this IValueFluentValidationRegistrar registrar, int count)
        {
            registrar._impl().ExposeValueRuleBuilder().AtLeast(count);
            return (IPredicateValidationRegistrar)registrar;
        }

        /// <summary>
        /// Length <br />
        /// 应长
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> Length<T>(this IValueFluentValidationRegistrar<T> registrar, int min, int max)
        {
            registrar._impl().ExposeValueRuleBuilder().Length(min, max);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Min length <br />
        /// 至少长
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="min"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> MinLength<T>(this IValueFluentValidationRegistrar<T> registrar, int min)
        {
            registrar._impl().ExposeValueRuleBuilder().MinLength(min);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Max length <br />
        /// 至多长
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="max"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> MaxLength<T>(this IValueFluentValidationRegistrar<T> registrar, int max)
        {
            registrar._impl().ExposeValueRuleBuilder().MaxLength(max);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// As least <br />
        /// 至少存在
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> AtLeast<T>(this IValueFluentValidationRegistrar<T> registrar, int count)
        {
            registrar._impl().ExposeValueRuleBuilder().AtLeast(count);
            return (IPredicateValidationRegistrar<T>)registrar;
        }

        /// <summary>
        /// Length <br />
        /// 应长
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> Length<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, int min, int max)
        {
            registrar._impl().ExposeValueRuleBuilder2().Length(min, max);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Min length <br />
        /// 至少长
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="min"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> MinLength<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, int min)
        {
            registrar._impl().ExposeValueRuleBuilder2().MinLength(min);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// Max length <br />
        /// 至多长
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="max"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> MaxLength<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, int max)
        {
            registrar._impl().ExposeValueRuleBuilder2().MaxLength(max);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }

        /// <summary>
        /// As least <br />
        /// 至少存在
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="count"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> AtLeast<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, int count)
        {
            registrar._impl().ExposeValueRuleBuilder2().AtLeast(count);
            return (IPredicateValidationRegistrar<T, TVal>)registrar;
        }
    }
}