namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Scale registrar extensions
    /// </summary>
    public static class ScaleRegistrarExtensions
    {
        /// <summary>
        /// Limit the scale and precision of the value.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="ignoreTrailingZeros"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar ScalePrecision(this IValueFluentValidationRegistrar registrar, int scale, int precision, bool ignoreTrailingZeros = false)
        {
            registrar._impl().ExposeValueRuleBuilder().ScalePrecision(scale, precision, ignoreTrailingZeros);
            return (IPredicateValidationRegistrar) registrar;
        }

        /// <summary>
        /// Limit the scale and precision of the value.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="ignoreTrailingZeros"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T> ScalePrecision<T>(this IValueFluentValidationRegistrar<T> registrar, int scale, int precision, bool ignoreTrailingZeros = false)
        {
            registrar._impl().ExposeValueRuleBuilder().ScalePrecision(scale, precision, ignoreTrailingZeros);
            return (IPredicateValidationRegistrar<T>) registrar;
        }

        /// <summary>
        /// Limit the scale and precision of the value.
        /// </summary>
        /// <param name="registrar"></param>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="ignoreTrailingZeros"></param>
        /// <returns></returns>
        public static IPredicateValidationRegistrar<T, TVal> ScalePrecision<T, TVal>(this IValueFluentValidationRegistrar<T, TVal> registrar, int scale, int precision, bool ignoreTrailingZeros = false)
        {
            registrar._impl().ExposeValueRuleBuilder2().ScalePrecision(scale, precision, ignoreTrailingZeros);
            return (IPredicateValidationRegistrar<T, TVal>) registrar;
        }
    }
}