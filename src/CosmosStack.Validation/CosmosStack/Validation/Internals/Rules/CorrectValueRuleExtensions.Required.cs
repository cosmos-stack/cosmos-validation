using System.Globalization;
using CosmosStack.Reflection;
using CosmosStack.Validation.Internals.Tokens.ValueTokens;

// ReSharper disable once CheckNamespace
namespace CosmosStack.Validation
{
    public static partial class CorrectValueRuleExtensions
    {
        #region Empty/NotEmpty/Required

        //`0

        public static IPredicateValueRuleBuilder Empty(this IValueRuleBuilder builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEmptyToken(current._contract);
            return current;
        }

        public static IPredicateValueRuleBuilder NotEmpty(this IValueRuleBuilder builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEmptyToken(current._contract);
            return current;
        }

        public static IPredicateValueRuleBuilder Required(this IValueRuleBuilder builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEmptyToken(current._contract);
            return current;
        }

        //`1

        public static IPredicateValueRuleBuilder<T> Empty<T>(this IValueRuleBuilder<T> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEmptyToken(current._contract);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> NotEmpty<T>(this IValueRuleBuilder<T> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEmptyToken(current._contract);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> Required<T>(this IValueRuleBuilder<T> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEmptyToken(current._contract);
            return current;
        }

        //`2

        public static IPredicateValueRuleBuilder<T, TVal> Empty<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueEmptyToken(current._contract);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> NotEmpty<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEmptyToken(current._contract);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> Required<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotEmptyToken(current._contract);
            return current;
        }

        #endregion

        #region Null/NotNull

        //`0

        public static IPredicateValueRuleBuilder Null(this IValueRuleBuilder builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNullToken(current._contract);
            return current;
        }

        public static IPredicateValueRuleBuilder NotNull(this IValueRuleBuilder builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotNullToken(current._contract);
            return current;
        }

        //`1

        public static IPredicateValueRuleBuilder<T> Null<T>(this IValueRuleBuilder<T> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNullToken(current._contract);
            return current;
        }

        public static IPredicateValueRuleBuilder<T> NotNull<T>(this IValueRuleBuilder<T> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotNullToken(current._contract);
            return current;
        }

        //`2

        public static IPredicateValueRuleBuilder<T, TVal> Null<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNullToken(current._contract);
            return current;
        }

        public static IPredicateValueRuleBuilder<T, TVal> NotNull<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueNotNullToken(current._contract);
            return current;
        }

        #endregion

        #region Reflection

        //`0

        /// <summary>
        /// The constraint type must be of string type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder RequiredString(this IValueRuleBuilder builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredStringToken(current._contract);
            return current;
        }

        /// <summary>
        /// The constraint type must be of numeric type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder RequiredNumeric(this IValueRuleBuilder builder, TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredNumericToken(current._contract, isOptions);
            return current;
        }

        /// <summary>
        /// The constraint type must be of boolean type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder RequiredBoolean(this IValueRuleBuilder builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredBooleanToken(current._contract);
            return current;
        }

        /// <summary>
        /// The constraint type must be of Guid type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder RequiredGuid(this IValueRuleBuilder builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredGuidToken(current._contract);
            return current;
        }

        /// <summary>
        /// The constraint type must be of DateTime type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder RequiredDateTime(this IValueRuleBuilder builder, DateTimeStyles style = DateTimeStyles.None)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredDateTimeToken(current._contract, style);
            return current;
        }

        /// <summary>
        /// The constraint type must be of DateInfo type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder RequiredDateInfo(this IValueRuleBuilder builder, DateTimeStyles style = DateTimeStyles.None)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredDateInfoToken(current._contract, style);
            return current;
        }

        //`1

        /// <summary>
        /// The constraint type must be of string type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> RequiredString<T>(this IValueRuleBuilder<T> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredStringToken(current._contract);
            return current;
        }

        /// <summary>
        /// The constraint type must be of numeric type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> RequiredNumeric<T>(this IValueRuleBuilder<T> builder, TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredNumericToken(current._contract, isOptions);
            return current;
        }

        /// <summary>
        /// The constraint type must be of boolean type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> RequiredBoolean<T>(this IValueRuleBuilder<T> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredBooleanToken(current._contract);
            return current;
        }

        /// <summary>
        /// The constraint type must be of Guid type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> RequiredGuid<T>(this IValueRuleBuilder<T> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredGuidToken(current._contract);
            return current;
        }

        /// <summary>
        /// The constraint type must be of DateTime type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> RequiredDateTime<T>(this IValueRuleBuilder<T> builder, DateTimeStyles style = DateTimeStyles.None)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredDateTimeToken(current._contract, style);
            return current;
        }

        /// <summary>
        /// The constraint type must be of DateInfo type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T> RequiredDateInfo<T>(this IValueRuleBuilder<T> builder, DateTimeStyles style = DateTimeStyles.None)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredDateInfoToken(current._contract, style);
            return current;
        }

        //`2

        /// <summary>
        /// The constraint type must be of string type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> RequiredString<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredStringToken(current._contract);
            return current;
        }

        /// <summary>
        /// The constraint type must be of numeric type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> RequiredNumeric<T, TVal>(this IValueRuleBuilder<T, TVal> builder, TypeIsOptions isOptions = TypeIsOptions.Default)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredNumericToken(current._contract, isOptions);
            return current;
        }

        /// <summary>
        /// The constraint type must be of boolean type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> RequiredBoolean<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredBooleanToken(current._contract);
            return current;
        }

        /// <summary>
        /// The constraint type must be of Guid type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> RequiredGuid<T, TVal>(this IValueRuleBuilder<T, TVal> builder)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredGuidToken(current._contract);
            return current;
        }

        /// <summary>
        /// The constraint type must be of DateTime type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> RequiredDateTime<T, TVal>(this IValueRuleBuilder<T, TVal> builder, DateTimeStyles style = DateTimeStyles.None)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredDateTimeToken(current._contract, style);
            return current;
        }

        /// <summary>
        /// The constraint type must be of DateInfo type.
        /// </summary>
        /// <returns></returns>
        public static IPredicateValueRuleBuilder<T, TVal> RequiredDateInfo<T, TVal>(this IValueRuleBuilder<T, TVal> builder, DateTimeStyles style = DateTimeStyles.None)
        {
            var current = builder._impl();
            current.State.CurrentToken = new ValueRequiredDateInfoToken(current._contract, style);
            return current;
        }

        #endregion
    }
}