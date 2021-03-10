using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Cosmos.Validation
{
    public interface IValueRuleBuilder
    {
        IValueRuleBuilder AppendRule();

        IValueRuleBuilder OverwriteRule();

        IValueRuleBuilder Empty();

        IValueRuleBuilder NotEmpty();

        IValueRuleBuilder Required();

        IValueRuleBuilder Null();

        IValueRuleBuilder NotNull();

        IValueRuleBuilder Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval);

        IValueRuleBuilder RangeWithOpenInterval(object from, object to);

        IValueRuleBuilder RangeWithCloseInterval(object from, object to);

        IValueRuleBuilder Length(int min, int max);

        IValueRuleBuilder MinLength(int min);

        IValueRuleBuilder MaxLength(int max);

        IValueRuleBuilder AtLeast(int count);

        IValueRuleBuilder Equal(object value);

        IValueRuleBuilder Equal(object value, IEqualityComparer comparer);

        IValueRuleBuilder NotEqual(object value);

        IValueRuleBuilder NotEqual(object value, IEqualityComparer comparer);

        IValueRuleBuilder LessThan(object value);

        IValueRuleBuilder LessThanOrEqual(object value);

        IValueRuleBuilder GreaterThan(object value);

        IValueRuleBuilder GreaterThanOrEqual(object value);

        IValueRuleBuilder Matches(Regex regex);

        IValueRuleBuilder Matches(string regexExpression);

        IValueRuleBuilder Matches(string regexExpression, RegexOptions options);

        IValueRuleBuilder Matches(Func<object, Regex> regexFunc);
        
        IValueRuleBuilder Matches(Func<object, string> regexExpressionFunc);
        
        IValueRuleBuilder Matches(Func<object, string> regexExpressionFunc, RegexOptions options);
        
        IValueRuleBuilder Matches(Expression<Func<object, Regex>> expression);
        
        IValueRuleBuilder Matches(Expression<Func<object, string>> expression);
        
        IValueRuleBuilder Matches(Expression<Func<object, string>> expression, RegexOptions options);

        IValueRuleBuilder Func(Func<object, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder Func(Func<object, bool> func);

        IWaitForMessageValueRuleBuilder Predicate(Predicate<object> predicate);

        IValueRuleBuilder Must(Func<object, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder Must(Func<object, bool> func);

        IValueRuleBuilder Any(Func<object, bool> func);

        IValueRuleBuilder All(Func<object, bool> func);

        IValueRuleBuilder NotAny(Func<object, bool> func);

        IValueRuleBuilder NotAll(Func<object, bool> func);
        IValueRuleBuilder None(Func<object, bool> func);

        IValueRuleBuilder In(ICollection<object> collection);

        IValueRuleBuilder In(params object[] objects);

        IValueRuleBuilder NotIn(ICollection<object> collection);

        IValueRuleBuilder NotIn(params object[] objects);

        IValueRuleBuilder InEnum(Type enumType);

        IValueRuleBuilder InEnum<TEnum>();

        IValueRuleBuilder IsEnumName(Type enumType, bool caseSensitive);

        IValueRuleBuilder IsEnumName<TEnum>(bool caseSensitive);

        IValueRuleBuilder ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false);

        IValueRuleBuilder RequiredType(Type type);

        IValueRuleBuilder RequiredTypes(params Type[] types);

        IValueRuleBuilder RequiredTypes<T1>();

        IValueRuleBuilder RequiredTypes<T1, T2>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        IValueRuleBuilder RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
    }

    public interface IValueRuleBuilder<T>
    {
        IValueRuleBuilder<T> AppendRule();

        IValueRuleBuilder<T> OverwriteRule();

        IValueRuleBuilder<T> Empty();

        IValueRuleBuilder<T> NotEmpty();

        IValueRuleBuilder<T> Required();

        IValueRuleBuilder<T> Null();

        IValueRuleBuilder<T> NotNull();

        IValueRuleBuilder<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval);

        IValueRuleBuilder<T> RangeWithOpenInterval(object from, object to);

        IValueRuleBuilder<T> RangeWithCloseInterval(object from, object to);

        IValueRuleBuilder<T> Length(int min, int max);

        IValueRuleBuilder<T> MinLength(int min);

        IValueRuleBuilder<T> MaxLength(int max);

        IValueRuleBuilder<T> AtLeast(int count);

        IValueRuleBuilder<T> Equal(object value);

        IValueRuleBuilder<T> Equal(object value, IEqualityComparer comparer);

        IValueRuleBuilder<T> NotEqual(object value);

        IValueRuleBuilder<T> NotEqual(object value, IEqualityComparer comparer);

        IValueRuleBuilder<T> LessThan(object value);

        IValueRuleBuilder<T> LessThanOrEqual(object value);

        IValueRuleBuilder<T> GreaterThan(object value);

        IValueRuleBuilder<T> GreaterThanOrEqual(object value);

        IValueRuleBuilder<T> Matches(Regex regex);

        IValueRuleBuilder<T> Matches(string regexExpression);

        IValueRuleBuilder<T> Matches(string regexExpression, RegexOptions options);

        IValueRuleBuilder<T> Matches(Func<T, Regex> regexFunc);
        
        IValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc);
        
        IValueRuleBuilder<T> Matches(Func<T, string> regexExpressionFunc, RegexOptions options);
        
        IValueRuleBuilder<T> Matches(Expression<Func<T, Regex>> expression);
        
        IValueRuleBuilder<T> Matches(Expression<Func<T, string>> expression);
        
        IValueRuleBuilder<T> Matches(Expression<Func<T, string>> expression, RegexOptions options);

        IValueRuleBuilder<T> Func(Func<object, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder<T> Func(Func<object, bool> func);

        IWaitForMessageValueRuleBuilder<T> Predicate(Predicate<object> predicate);

        IValueRuleBuilder<T> Must(Func<object, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder<T> Must(Func<object, bool> func);

        // IValueRuleBuilder<T> Any(Func<object, bool> func);
        //
        // IValueRuleBuilder<T> All(Func<object, bool> func);
        //
        // IValueRuleBuilder<T> NotAny(Func<object, bool> func);
        //
        // IValueRuleBuilder<T> NotAll(Func<object, bool> func);

        IValueRuleBuilder<T> In(ICollection<object> collection);

        IValueRuleBuilder<T> In(params object[] objects);

        IValueRuleBuilder<T> NotIn(ICollection<object> collection);

        IValueRuleBuilder<T> NotIn(params object[] objects);

        IValueRuleBuilder<T> InEnum(Type enumType);

        IValueRuleBuilder<T> InEnum<TEnum>();

        IValueRuleBuilder<T> IsEnumName(Type enumType, bool caseSensitive);

        IValueRuleBuilder<T> IsEnumName<TEnum>(bool caseSensitive);

        IValueRuleBuilder<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false);

        IValueRuleBuilder<T> RequiredType(Type type);

        IValueRuleBuilder<T> RequiredTypes(params Type[] types);

        IValueRuleBuilder<T> RequiredTypes<T1>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        IValueRuleBuilder<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
    }

    public interface IValueRuleBuilder<T, TVal> : IValueRuleBuilder<T>
    {
        new IValueRuleBuilder<T, TVal> AppendRule();

        new IValueRuleBuilder<T, TVal> OverwriteRule();

        new IValueRuleBuilder<T, TVal> Empty();

        new IValueRuleBuilder<T, TVal> NotEmpty();

        new IValueRuleBuilder<T, TVal> Required();

        new IValueRuleBuilder<T, TVal> Null();

        new IValueRuleBuilder<T, TVal> NotNull();

        IValueRuleBuilder<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval);

        IValueRuleBuilder<T, TVal> RangeWithOpenInterval(TVal from, TVal to);

        IValueRuleBuilder<T, TVal> RangeWithCloseInterval(TVal from, TVal to);

        new IValueRuleBuilder<T, TVal> Length(int min, int max);

        new IValueRuleBuilder<T, TVal> MinLength(int min);

        new IValueRuleBuilder<T, TVal> MaxLength(int max);

        new IValueRuleBuilder<T, TVal> AtLeast(int count);

        IValueRuleBuilder<T, TVal> Equal(TVal value);

        IValueRuleBuilder<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer);

        IValueRuleBuilder<T, TVal> NotEqual(TVal value);

        IValueRuleBuilder<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer);

        IValueRuleBuilder<T, TVal> LessThan(TVal value);

        IValueRuleBuilder<T, TVal> LessThanOrEqual(TVal value);

        IValueRuleBuilder<T, TVal> GreaterThan(TVal value);

        IValueRuleBuilder<T, TVal> GreaterThanOrEqual(TVal value);

        new IValueRuleBuilder<T, TVal> Matches(Regex regex);

        new IValueRuleBuilder<T, TVal> Matches(string regexExpression);

        new IValueRuleBuilder<T, TVal> Matches(string regexExpression, RegexOptions options);

        new IValueRuleBuilder<T, TVal> Matches(Func<T, Regex> regexFunc);
        
        new IValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc);
        
        new IValueRuleBuilder<T, TVal> Matches(Func<T, string> regexExpressionFunc, RegexOptions options);
        
        new IValueRuleBuilder<T, TVal> Matches(Expression<Func<T, Regex>> expression);
        
        new IValueRuleBuilder<T, TVal> Matches(Expression<Func<T, string>> expression);
        
        new IValueRuleBuilder<T, TVal> Matches(Expression<Func<T, string>> expression, RegexOptions options);

        IValueRuleBuilder<T, TVal> Func(Func<TVal, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder<T, TVal> Func(Func<TVal, bool> func);

        IWaitForMessageValueRuleBuilder<T, TVal> Predicate(Predicate<TVal> predicate);

        IValueRuleBuilder<T, TVal> Must(Func<TVal, CustomVerifyResult> func);

        IWaitForMessageValueRuleBuilder<T, TVal> Must(Func<TVal, bool> func);

        IValueRuleBuilder<T, TVal> In(ICollection<TVal> collection);

        IValueRuleBuilder<T, TVal> In(params TVal[] objects);

        IValueRuleBuilder<T, TVal> NotIn(ICollection<TVal> collection);

        IValueRuleBuilder<T, TVal> NotIn(params TVal[] objects);

        new IValueRuleBuilder<T, TVal> InEnum(Type enumType);

        new IValueRuleBuilder<T, TVal> InEnum<TEnum>();

        new IValueRuleBuilder<T, TVal> IsEnumName(Type enumType, bool caseSensitive);

        new IValueRuleBuilder<T, TVal> IsEnumName<TEnum>(bool caseSensitive);

        new IValueRuleBuilder<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false);

        new IValueRuleBuilder<T, TVal> RequiredType(Type type);

        new IValueRuleBuilder<T, TVal> RequiredTypes(params Type[] types);

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        new IValueRuleBuilder<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
    }
}