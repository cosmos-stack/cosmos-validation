﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cosmos.Validation.Registrars.Interfaces;

namespace Cosmos.Validation.Registrars
{
    public interface IValueFluentValidationRegistrar :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect
    {
        Type DeclaringType { get; }
        Type MemberType { get; }
        IValueFluentValidationRegistrar WithConfig(Func<IValueRuleBuilder, IValueRuleBuilder> func);
        IValueFluentValidationRegistrar Empty();
        IValueFluentValidationRegistrar NotEmpty();
        IValueFluentValidationRegistrar Required();
        IValueFluentValidationRegistrar Null();
        IValueFluentValidationRegistrar NotNull();
        IValueFluentValidationRegistrar Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval);
        IValueFluentValidationRegistrar RangeWithOpenInterval(object from, object to);
        IValueFluentValidationRegistrar RangeWithCloseInterval(object from, object to);
        IValueFluentValidationRegistrar Length(int min, int max);
        IValueFluentValidationRegistrar MinLength(int min);
        IValueFluentValidationRegistrar MaxLength(int max);
        IValueFluentValidationRegistrar AtLeast(int count);
        IValueFluentValidationRegistrar Equal(object value);
        IValueFluentValidationRegistrar Equal(object value, IEqualityComparer comparer);
        IValueFluentValidationRegistrar NotEqual(object value);
        IValueFluentValidationRegistrar NotEqual(object value, IEqualityComparer comparer);
        IValueFluentValidationRegistrar LessThan(object value);
        IValueFluentValidationRegistrar LessThanOrEqual(object value);
        IValueFluentValidationRegistrar GreaterThan(object value);
        IValueFluentValidationRegistrar GreaterThanOrEqual(object value);
        IValueFluentValidationRegistrar Func(Func<object, CustomVerifyResult> func);
        IWaitForMessageValidationRegistrar Func(Func<object, bool> func);
        IWaitForMessageValidationRegistrar Predicate(Predicate<object> predicate);
        IValueFluentValidationRegistrar Must(Func<object, CustomVerifyResult> func);
        IWaitForMessageValidationRegistrar Must(Func<object, bool> func);
        IValueFluentValidationRegistrar InEnum(Type enumType);
        IValueFluentValidationRegistrar InEnum<TEnum>();
        IValueFluentValidationRegistrar IsEnumName(Type enumType, bool caseSensitive);
        IValueFluentValidationRegistrar IsEnumName<TEnum>(bool caseSensitive);
        IValueFluentValidationRegistrar ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false);
        IValueFluentValidationRegistrar RequiredType(Type type);
        IValueFluentValidationRegistrar RequiredTypes(params Type[] types);
        IValueFluentValidationRegistrar RequiredTypes<T1>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
        IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
    }

    public interface IValueFluentValidationRegistrar<T> :
        IMayContinueRegisterForStrategy,
        IMayContinueRegisterForCustomValidator,
        IMayContinueRegisterForType,
        IMayContinueRegisterForMember<T>,
        IMayBuild,
        IMayTempBuild,
        IMayTakeEffect
    {
        Type DeclaringType { get; }
        Type MemberType { get; }
        IValueFluentValidationRegistrar<T> WithConfig(Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func);
        IValueFluentValidationRegistrar<T> Empty();

        IValueFluentValidationRegistrar<T> NotEmpty();

        IValueFluentValidationRegistrar<T> Required();

        IValueFluentValidationRegistrar<T> Null();

        IValueFluentValidationRegistrar<T> NotNull();

        IValueFluentValidationRegistrar<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval);

        IValueFluentValidationRegistrar<T> RangeWithOpenInterval(object from, object to);

        IValueFluentValidationRegistrar<T> RangeWithCloseInterval(object from, object to);

        IValueFluentValidationRegistrar<T> Length(int min, int max);

        IValueFluentValidationRegistrar<T> MinLength(int min);

        IValueFluentValidationRegistrar<T> MaxLength(int max);

        IValueFluentValidationRegistrar<T> AtLeast(int count);

        IValueFluentValidationRegistrar<T> Equal(object value);

        IValueFluentValidationRegistrar<T> Equal(object value, IEqualityComparer comparer);

        IValueFluentValidationRegistrar<T> NotEqual(object value);

        IValueFluentValidationRegistrar<T> NotEqual(object value, IEqualityComparer comparer);

        IValueFluentValidationRegistrar<T> LessThan(object value);

        IValueFluentValidationRegistrar<T> LessThanOrEqual(object value);

        IValueFluentValidationRegistrar<T> GreaterThan(object value);

        IValueFluentValidationRegistrar<T> GreaterThanOrEqual(object value);

        IValueFluentValidationRegistrar<T> Func(Func<object, CustomVerifyResult> func);

        IWaitForMessageValidationRegistrar<T> Func(Func<object, bool> func);

        IWaitForMessageValidationRegistrar<T> Predicate(Predicate<object> predicate);

        IValueFluentValidationRegistrar<T> Must(Func<object, CustomVerifyResult> func);

        IWaitForMessageValidationRegistrar<T> Must(Func<object, bool> func);

        IValueFluentValidationRegistrar<T> InEnum(Type enumType);

        IValueFluentValidationRegistrar<T> InEnum<TEnum>();

        IValueFluentValidationRegistrar<T> IsEnumName(Type enumType, bool caseSensitive);

        IValueFluentValidationRegistrar<T> IsEnumName<TEnum>(bool caseSensitive);

        IValueFluentValidationRegistrar<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false);

        IValueFluentValidationRegistrar<T> RequiredType(Type type);

        IValueFluentValidationRegistrar<T> RequiredTypes(params Type[] types);

        IValueFluentValidationRegistrar<T> RequiredTypes<T1>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
    }

    public interface IValueFluentValidationRegistrar<T, TVal> : IValueFluentValidationRegistrar<T>
    {
        IValueFluentValidationRegistrar<T, TVal> WithConfig(Func<IValueRuleBuilder<T, TVal>, IValueRuleBuilder<T, TVal>> func);
        new IValueFluentValidationRegistrar<T, TVal> Empty();

        new IValueFluentValidationRegistrar<T, TVal> NotEmpty();

        new IValueFluentValidationRegistrar<T, TVal> Required();

        new IValueFluentValidationRegistrar<T, TVal> Null();

        new IValueFluentValidationRegistrar<T, TVal> NotNull();

        IValueFluentValidationRegistrar<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval);

        IValueFluentValidationRegistrar<T, TVal> RangeWithOpenInterval(TVal from, TVal to);

        IValueFluentValidationRegistrar<T, TVal> RangeWithCloseInterval(TVal from, TVal to);

        new IValueFluentValidationRegistrar<T, TVal> Length(int min, int max);

        new IValueFluentValidationRegistrar<T, TVal> MinLength(int min);

        new IValueFluentValidationRegistrar<T, TVal> MaxLength(int max);

        new IValueFluentValidationRegistrar<T, TVal> AtLeast(int count);

        IValueFluentValidationRegistrar<T, TVal> Equal(TVal value);

        IValueFluentValidationRegistrar<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer);

        IValueFluentValidationRegistrar<T, TVal> NotEqual(TVal value);

        IValueFluentValidationRegistrar<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer);

        IValueFluentValidationRegistrar<T, TVal> LessThan(TVal value);

        IValueFluentValidationRegistrar<T, TVal> LessThanOrEqual(TVal value);

        IValueFluentValidationRegistrar<T, TVal> GreaterThan(TVal value);

        IValueFluentValidationRegistrar<T, TVal> GreaterThanOrEqual(TVal value);

        IValueFluentValidationRegistrar<T, TVal> Func(Func<TVal, CustomVerifyResult> func);

        IWaitForMessageValidationRegistrar<T, TVal> Func(Func<TVal, bool> func);

        IWaitForMessageValidationRegistrar<T, TVal> Predicate(Predicate<TVal> predicate);

        IValueFluentValidationRegistrar<T, TVal> Must(Func<TVal, CustomVerifyResult> func);

        IWaitForMessageValidationRegistrar<T, TVal> Must(Func<TVal, bool> func);

        new IValueFluentValidationRegistrar<T, TVal> InEnum(Type enumType);

        new IValueFluentValidationRegistrar<T, TVal> InEnum<TEnum>();

        new IValueFluentValidationRegistrar<T, TVal> IsEnumName(Type enumType, bool caseSensitive);

        new IValueFluentValidationRegistrar<T, TVal> IsEnumName<TEnum>(bool caseSensitive);

        new IValueFluentValidationRegistrar<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false);

        new IValueFluentValidationRegistrar<T, TVal> RequiredType(Type type);

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes(params Type[] types);

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();

        new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
    }
}