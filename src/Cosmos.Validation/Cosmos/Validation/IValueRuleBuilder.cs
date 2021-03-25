using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Cosmos.Reflection;

namespace Cosmos.Validation
{
    /// <summary>
    /// Interface of ValueRuleBuilder
    /// </summary>
    public interface IValueRuleBuilder
    {
        IValueRuleBuilder AppendRule();

        IValueRuleBuilder OverwriteRule();

        IValueRuleBuilder And();

        IValueRuleBuilder Or();

        IPredicateValueRuleBuilder Empty();

        IPredicateValueRuleBuilder NotEmpty();

        IPredicateValueRuleBuilder Required();

        IPredicateValueRuleBuilder Null();

        IPredicateValueRuleBuilder NotNull();

        IPredicateValueRuleBuilder Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval);

        IPredicateValueRuleBuilder RangeWithOpenInterval(object from, object to);

        IPredicateValueRuleBuilder RangeWithCloseInterval(object from, object to);

        IPredicateValueRuleBuilder Length(int min, int max);

        IPredicateValueRuleBuilder MinLength(int min);

        IPredicateValueRuleBuilder MaxLength(int max);

        IPredicateValueRuleBuilder AtLeast(int count);

        IPredicateValueRuleBuilder Equal(object value);

        IPredicateValueRuleBuilder Equal(object value, IEqualityComparer comparer);

        IPredicateValueRuleBuilder NotEqual(object value);

        IPredicateValueRuleBuilder NotEqual(object value, IEqualityComparer comparer);

        IPredicateValueRuleBuilder LessThan(object value);

        IPredicateValueRuleBuilder LessThanOrEqual(object value);

        IPredicateValueRuleBuilder GreaterThan(object value);

        IPredicateValueRuleBuilder GreaterThanOrEqual(object value);

        IPredicateValueRuleBuilder Matches(Regex regex);

        IPredicateValueRuleBuilder Matches(string regexExpression);

        IPredicateValueRuleBuilder Matches(string regexExpression, RegexOptions options);

        IPredicateValueRuleBuilder Matches(Func<object, Regex> regexFunc);

        IPredicateValueRuleBuilder Matches(Func<object, string> regexExpressionFunc);

        IPredicateValueRuleBuilder Matches(Func<object, string> regexExpressionFunc, RegexOptions options);

        IPredicateValueRuleBuilder Func(Func<object, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder Func(Func<object, bool> func);

        IWaitForMessageValueRuleBuilder Predicate(Predicate<object> predicate);

        IPredicateValueRuleBuilder Must(Func<object, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder Must(Func<object, bool> func);

        IWaitForMessageValueRuleBuilder Satisfies(Func<object, bool> func);

        IPredicateValueRuleBuilder Satisfies(Func<object, bool> func, string message);

        IPredicateValueRuleBuilder Any(Func<object, bool> func);

        IPredicateValueRuleBuilder All(Func<object, bool> func);

        IPredicateValueRuleBuilder NotAny(Func<object, bool> func);

        IPredicateValueRuleBuilder NotAll(Func<object, bool> func);
        IPredicateValueRuleBuilder None(Func<object, bool> func);

        IPredicateValueRuleBuilder In(ICollection<object> collection);

        IPredicateValueRuleBuilder In(params object[] objects);

        IPredicateValueRuleBuilder NotIn(ICollection<object> collection);

        IPredicateValueRuleBuilder NotIn(params object[] objects);

        IPredicateValueRuleBuilder InEnum(Type enumType);

        IPredicateValueRuleBuilder InEnum<TEnum>();

        IPredicateValueRuleBuilder IsEnumName(Type enumType, bool caseSensitive);

        IPredicateValueRuleBuilder IsEnumName<TEnum>(bool caseSensitive);

        /// <summary>
        /// Limit the scale and precision of the value.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="ignoreTrailingZeros"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false);

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredType(Type type);

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes(params Type[] types);

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <typeparam name="T15"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <typeparam name="T15"></typeparam>
        /// <typeparam name="T16"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();

        /// <summary>
        /// The constraint type must be of string type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredString();

        /// <summary>
        /// The constraint type must be of numeric type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default);

        /// <summary>
        /// The constraint type must be of boolean type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredBoolean();

        /// <summary>
        /// The constraint type must be of Guid type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredGuid();

        /// <summary>
        /// The constraint type must be of DateTime type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredDateTime(DateTimeStyles style = DateTimeStyles.None);

        /// <summary>
        /// The constraint type must be of DateInfo type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder RequiredDateInfo(DateTimeStyles style = DateTimeStyles.None);
    }

    /// <summary>
    /// Interface of ValueRuleBuilder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValueRuleBuilder<T>
    {
        IValueRuleBuilder<T> AppendRule();

        IValueRuleBuilder<T> OverwriteRule();

        IValueRuleBuilder<T> And();

        IValueRuleBuilder<T> Or();

        IPredicateValueRuleBuilder<T> Empty();

        IPredicateValueRuleBuilder<T> NotEmpty();

        IPredicateValueRuleBuilder<T> Required();

        IPredicateValueRuleBuilder<T> Null();

        IPredicateValueRuleBuilder<T> NotNull();

        IPredicateValueRuleBuilder<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval);

        IPredicateValueRuleBuilder<T> RangeWithOpenInterval(object from, object to);

        IPredicateValueRuleBuilder<T> RangeWithCloseInterval(object from, object to);

        IPredicateValueRuleBuilder<T> Length(int min, int max);

        IPredicateValueRuleBuilder<T> MinLength(int min);

        IPredicateValueRuleBuilder<T> MaxLength(int max);

        IPredicateValueRuleBuilder<T> AtLeast(int count);

        IPredicateValueRuleBuilder<T> Equal(object value);

        IPredicateValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer);

        IPredicateValueRuleBuilder<T> NotEqual(object value);

        IPredicateValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer);

        IPredicateValueRuleBuilder<T> LessThan(object value);

        IPredicateValueRuleBuilder<T> LessThanOrEqual(object value);

        IPredicateValueRuleBuilder<T> GreaterThan(object value);

        IPredicateValueRuleBuilder<T> GreaterThanOrEqual(object value);

        IPredicateValueRuleBuilder<T> Matches(Regex regex);

        IPredicateValueRuleBuilder<T> Matches(string regexExpression);

        IPredicateValueRuleBuilder<T> Matches(string regexExpression, RegexOptions options);

        IPredicateValueRuleBuilder<T> Matches(Func<T, Regex> regexFunc);

        IPredicateValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc);

        IPredicateValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc, RegexOptions options);

        IPredicateValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder<T> Func(Func<object, bool> func);

        IWaitForMessageValueRuleBuilder<T> Predicate(Predicate<object> predicate);

        IPredicateValueRuleBuilder<T> Must(Func<object, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func);

        IWaitForMessageValueRuleBuilder<T> Satisfies(Func<object, bool> func);

        IPredicateValueRuleBuilder<T> Satisfies(Func<object, bool> func, string message);

        // IWaitForActivationConditionsValueRuleBuilder<T> Any(Func<object, bool> func);
        //
        // IWaitForActivationConditionsValueRuleBuilder<T> All(Func<object, bool> func);
        //
        // IWaitForActivationConditionsValueRuleBuilder<T> NotAny(Func<object, bool> func);
        //
        // IWaitForActivationConditionsValueRuleBuilder<T> NotAll(Func<object, bool> func);

        IPredicateValueRuleBuilder<T> In(ICollection<object> collection);

        IPredicateValueRuleBuilder<T> In(params object[] objects);

        IPredicateValueRuleBuilder<T> NotIn(ICollection<object> collection);

        IPredicateValueRuleBuilder<T> NotIn(params object[] objects);

        IPredicateValueRuleBuilder<T> InEnum(Type enumType);

        IPredicateValueRuleBuilder<T> InEnum<TEnum>();

        IPredicateValueRuleBuilder<T> IsEnumName(Type enumType, bool caseSensitive);

        IPredicateValueRuleBuilder<T> IsEnumName<TEnum>(bool caseSensitive);

        /// <summary>
        /// Limit the scale and precision of the value.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="ignoreTrailingZeros"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false);

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredType(Type type);

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes(params Type[] types);

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <typeparam name="T15"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <typeparam name="T15"></typeparam>
        /// <typeparam name="T16"></typeparam>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();

        /// <summary>
        /// The constraint type must be of string type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredString();

        /// <summary>
        /// The constraint type must be of numeric type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default);

        /// <summary>
        /// The constraint type must be of boolean type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredBoolean();

        /// <summary>
        /// The constraint type must be of Guid type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredGuid();

        /// <summary>
        /// The constraint type must be of DateTime type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredDateTime(DateTimeStyles style = DateTimeStyles.None);

        /// <summary>
        /// The constraint type must be of DateInfo type.
        /// </summary>
        /// <returns></returns>
        IPredicateValueRuleBuilder<T> RequiredDateInfo(DateTimeStyles style = DateTimeStyles.None);
    }

    /// <summary>
    /// Interface of ValueRuleBuilder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IValueRuleBuilder<T, TVal> : IValueRuleBuilder<T>
    {
        new IValueRuleBuilder<T, TVal> AppendRule();

        new IValueRuleBuilder<T, TVal> OverwriteRule();

        new IValueRuleBuilder<T, TVal> And();

        new IValueRuleBuilder<T, TVal> Or();

        new IPredicateValueRuleBuilder<T, TVal> Empty();

        new IPredicateValueRuleBuilder<T, TVal> NotEmpty();

        new IPredicateValueRuleBuilder<T, TVal> Required();

        new IPredicateValueRuleBuilder<T, TVal> Null();

        new IPredicateValueRuleBuilder<T, TVal> NotNull();

        IPredicateValueRuleBuilder<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval);

        IPredicateValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal from, TVal to);

        IPredicateValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal from, TVal to);

        new IPredicateValueRuleBuilder<T, TVal> Length(int min, int max);

        new IPredicateValueRuleBuilder<T, TVal> MinLength(int min);

        new IPredicateValueRuleBuilder<T, TVal> MaxLength(int max);

        new IPredicateValueRuleBuilder<T, TVal> AtLeast(int count);

        IPredicateValueRuleBuilder<T, TVal> Equal(TVal value);

        IPredicateValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer);

        IPredicateValueRuleBuilder<T, TVal> NotEqual(TVal value);

        IPredicateValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer);

        IPredicateValueRuleBuilder<T, TVal> LessThan(TVal value);

        IPredicateValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value);

        IPredicateValueRuleBuilder<T, TVal> GreaterThan(TVal value);

        IPredicateValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value);

        new IPredicateValueRuleBuilder<T, TVal> Matches(Regex regex);

        new IPredicateValueRuleBuilder<T, TVal> Matches(string regexExpression);

        new IPredicateValueRuleBuilder<T, TVal> Matches(string regexExpression, RegexOptions options);

        new IPredicateValueRuleBuilder<T, TVal> Matches(Func<T, Regex> regexFunc);

        new IPredicateValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc);

        new IPredicateValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc, RegexOptions options);

        IPredicateValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder<T, TVal> Func(Func<TVal, bool> func);

        IWaitForMessageValueRuleBuilder<T, TVal> Predicate(Predicate<TVal> predicate);

        IPredicateValueRuleBuilder<T, TVal> Must(Func<TVal, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func);

        IWaitForMessageValueRuleBuilder<T, TVal> Satisfies(Func<TVal, bool> func);

        IPredicateValueRuleBuilder<T, TVal> Satisfies(Func<TVal, bool> func, string message);

        IPredicateValueRuleBuilder<T, TVal> In(ICollection<TVal> collection);

        IPredicateValueRuleBuilder<T, TVal> In(params TVal[] objects);

        IPredicateValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection);

        IPredicateValueRuleBuilder<T, TVal> NotIn(params TVal[] objects);

        new IPredicateValueRuleBuilder<T, TVal> InEnum(Type enumType);

        new IPredicateValueRuleBuilder<T, TVal> InEnum<TEnum>();

        new IPredicateValueRuleBuilder<T, TVal> IsEnumName(Type enumType, bool caseSensitive);

        new IPredicateValueRuleBuilder<T, TVal> IsEnumName<TEnum>(bool caseSensitive);

        /// <summary>
        /// Limit the scale and precision of the value.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="ignoreTrailingZeros"></param>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false);

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredType(Type type);

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <param name="types"></param>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes(params Type[] types);

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <typeparam name="T15"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="T8"></typeparam>
        /// <typeparam name="T9"></typeparam>
        /// <typeparam name="T10"></typeparam>
        /// <typeparam name="T11"></typeparam>
        /// <typeparam name="T12"></typeparam>
        /// <typeparam name="T13"></typeparam>
        /// <typeparam name="T14"></typeparam>
        /// <typeparam name="T15"></typeparam>
        /// <typeparam name="T16"></typeparam>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();

        /// <summary>
        /// The constraint type must be of string type.
        /// </summary>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredString();

        /// <summary>
        /// The constraint type must be of numeric type.
        /// </summary>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredNumeric(TypeIsOptions isOptions = TypeIsOptions.Default);

        /// <summary>
        /// The constraint type must be of boolean type.
        /// </summary>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredBoolean();

        /// <summary>
        /// The constraint type must be of Guid type.
        /// </summary>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredGuid();

        /// <summary>
        /// The constraint type must be of DateTime type.
        /// </summary>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredDateTime(DateTimeStyles style = DateTimeStyles.None);

        /// <summary>
        /// The constraint type must be of DateInfo type.
        /// </summary>
        /// <returns></returns>
        new IPredicateValueRuleBuilder<T, TVal> RequiredDateInfo(DateTimeStyles style = DateTimeStyles.None);
    }
}