using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Cosmos.Validation.Strategies;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    public interface IValueFluentValidationRegistrar
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
        IValueFluentValidationRegistrar Matches(Regex regex);
        IValueFluentValidationRegistrar Matches(string regexExpression);
        IValueFluentValidationRegistrar Matches(string regexExpression, RegexOptions options);
        IValueFluentValidationRegistrar Matches(Func<object, Regex> regexFunc);
        IValueFluentValidationRegistrar Matches(Func<object, string> regexExpressionFunc);
        IValueFluentValidationRegistrar Matches(Func<object, string> regexExpressionFunc, RegexOptions options);
        IValueFluentValidationRegistrar Func(Func<object, CustomVerifyResult> func);
        IWaitForMessageValidationRegistrar Func(Func<object, bool> func);
        IWaitForMessageValidationRegistrar Predicate(Predicate<object> predicate);
        IValueFluentValidationRegistrar Must(Func<object, CustomVerifyResult> func);
        IWaitForMessageValidationRegistrar Must(Func<object, bool> func);
        IValueFluentValidationRegistrar Any(Func<object, bool> func);
        IValueFluentValidationRegistrar All(Func<object, bool> func);
        IValueFluentValidationRegistrar NotAny(Func<object, bool> func);
        IValueFluentValidationRegistrar NotAll(Func<object, bool> func);
        IValueFluentValidationRegistrar In(ICollection<object> collection);
        IValueFluentValidationRegistrar In(params object[] objects);
        IValueFluentValidationRegistrar NotIn(ICollection<object> collection);
        IValueFluentValidationRegistrar NotIn(params object[] objects);
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
        IValueFluentValidationRegistrar AndForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar AndForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar AndForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IFluentValidationRegistrar AndForType(Type type);
        IFluentValidationRegistrar AndForType(Type type, string name);
        IFluentValidationRegistrar<T> AndForType<T>();
        IFluentValidationRegistrar<T> AndForType<T>(string name);
        IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IFluentValidationRegistrar AndForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IFluentValidationRegistrar AndForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForValidator<TValidator>() where TValidator : CustomValidator, new();
        IFluentValidationRegistrar AndForValidator<TValidator, T>() where TValidator : CustomValidator<T>, new();
        IFluentValidationRegistrar AndForValidator(CustomValidator validator);
        IFluentValidationRegistrar AndForValidator<T>(CustomValidator<T> validator);
        void Build();
        ValidationHandler TempBuild();
        ValidationHandler TempBuild(ValidationOptions options);
        ValidationHandler TempBuild(Action<ValidationOptions> optionsAct);
    }

    public interface IValueFluentValidationRegistrar<T>
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

        IValueFluentValidationRegistrar<T> Matches(Regex regex);

        IValueFluentValidationRegistrar<T> Matches(string regexExpression);

        IValueFluentValidationRegistrar<T> Matches(string regexExpression, RegexOptions options);

        IValueFluentValidationRegistrar<T> Matches(Func<object, Regex> regexFunc);

        IValueFluentValidationRegistrar<T> Matches(Func<object, string> regexExpressionFunc);

        IValueFluentValidationRegistrar<T> Matches(Func<object, string> regexExpressionFunc, RegexOptions options);

        IValueFluentValidationRegistrar<T> Func(Func<object, CustomVerifyResult> func);

        IWaitForMessageValidationRegistrar<T> Func(Func<object, bool> func);

        IWaitForMessageValidationRegistrar<T> Predicate(Predicate<object> predicate);

        IValueFluentValidationRegistrar<T> Must(Func<object, CustomVerifyResult> func);

        IWaitForMessageValidationRegistrar<T> Must(Func<object, bool> func);

        IValueFluentValidationRegistrar<T> Any(Func<object, bool> func);

        IValueFluentValidationRegistrar<T> All(Func<object, bool> func);

        IValueFluentValidationRegistrar<T> NotAny(Func<object, bool> func);

        IValueFluentValidationRegistrar<T> NotAll(Func<object, bool> func);

        IValueFluentValidationRegistrar<T> In(ICollection<object> collection);

        IValueFluentValidationRegistrar<T> In(params object[] objects);

        IValueFluentValidationRegistrar<T> NotIn(ICollection<object> collection);

        IValueFluentValidationRegistrar<T> NotIn(params object[] objects);

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
        IValueFluentValidationRegistrar<T> AndForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T> AndForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T> AndForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append);
        IValueFluentValidationRegistrar<T, TVal> AndForMember<TVal>(Expression<Func<T, TVal>> expression, ValueRuleMode mode = ValueRuleMode.Append);
        IFluentValidationRegistrar AndForType(Type type);
        IFluentValidationRegistrar AndForType(Type type, string name);
        IFluentValidationRegistrar<TType> AndForType<TType>();
        IFluentValidationRegistrar<TType> AndForType<TType>(string name);
        void Build();
        ValidationHandler TempBuild();
        ValidationHandler TempBuild(ValidationOptions options);
        ValidationHandler TempBuild(Action<ValidationOptions> optionsAct);
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

        new IValueFluentValidationRegistrar<T, TVal> Matches(Regex regex);

        new IValueFluentValidationRegistrar<T, TVal> Matches(string regexExpression);

        new IValueFluentValidationRegistrar<T, TVal> Matches(string regexExpression, RegexOptions options);

        new IValueFluentValidationRegistrar<T, TVal> Matches(Func<object, Regex> regexFunc);

        new IValueFluentValidationRegistrar<T, TVal> Matches(Func<object, string> regexExpressionFunc);

        new IValueFluentValidationRegistrar<T, TVal> Matches(Func<object, string> regexExpressionFunc, RegexOptions options);

        IValueFluentValidationRegistrar<T, TVal> Func(Func<TVal, CustomVerifyResult> func);

        IWaitForMessageValidationRegistrar<T, TVal> Func(Func<TVal, bool> func);

        IWaitForMessageValidationRegistrar<T, TVal> Predicate(Predicate<TVal> predicate);

        IValueFluentValidationRegistrar<T, TVal> Must(Func<TVal, CustomVerifyResult> func);

        IWaitForMessageValidationRegistrar<T, TVal> Must(Func<TVal, bool> func);

        IValueFluentValidationRegistrar<T, TVal> In(ICollection<TVal> collection);

        IValueFluentValidationRegistrar<T, TVal> In(params TVal[] objects);

        IValueFluentValidationRegistrar<T, TVal> NotIn(ICollection<TVal> collection);

        IValueFluentValidationRegistrar<T, TVal> NotIn(params TVal[] objects);

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