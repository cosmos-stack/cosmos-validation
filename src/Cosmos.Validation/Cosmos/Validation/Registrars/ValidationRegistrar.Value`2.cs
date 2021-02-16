using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Registrars
{
    internal class ValueValidationRegistrar<T, TVal> : ValueValidationRegistrar<T>, IValueFluentValidationRegistrar<T, TVal>
    {
        public ValueValidationRegistrar(
            ObjectValueContract valueContract,
            List<CorrectValueRule> rules,
            ValueRuleMode mode,
            IFluentValidationRegistrar<T> parentRegistrar)
            : base(valueContract, rules, mode, parentRegistrar)
        {
            ValueRuleBuilder = new CorrectValueRuleBuilder<T, TVal>(valueContract, mode);
        }

        private CorrectValueRuleBuilder<T, TVal> ValueRuleBuilderPtr => (CorrectValueRuleBuilder<T, TVal>) ValueRuleBuilder;

        #region ValueRules`2

        public new IValueFluentValidationRegistrar<T, TVal> Empty()
        {
            ValueRuleBuilder.Empty();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> NotEmpty()
        {
            ValueRuleBuilder.NotEmpty();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> Required()
        {
            ValueRuleBuilder.Required();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> Null()
        {
            ValueRuleBuilder.Null();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> NotNull()
        {
            ValueRuleBuilder.NotNull();
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> Range(TVal from, TVal to, RangeOptions options = RangeOptions.OpenInterval)
        {
            ValueRuleBuilder.Range(from, to, options);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> RangeWithOpenInterval(TVal from, TVal to)
        {
            ValueRuleBuilder.RangeWithOpenInterval(from, to);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> RangeWithCloseInterval(TVal from, TVal to)
        {
            ValueRuleBuilder.RangeWithCloseInterval(from, to);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> Length(int min, int max)
        {
            ValueRuleBuilder.Length(min, max);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> MinLength(int min)
        {
            ValueRuleBuilder.MinLength(min);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> MaxLength(int max)
        {
            ValueRuleBuilder.MaxLength(max);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> AtLeast(int count)
        {
            ValueRuleBuilder.AtLeast(count);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> Equal(TVal value)
        {
            ValueRuleBuilder.Equal(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> Equal(TVal value, IEqualityComparer<TVal> comparer)
        {
            ValueRuleBuilderPtr.Equal(value, comparer);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> NotEqual(TVal value)
        {
            ValueRuleBuilder.NotEqual(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> NotEqual(TVal value, IEqualityComparer<TVal> comparer)
        {
            ValueRuleBuilderPtr.NotEqual(value, comparer);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> LessThan(TVal value)
        {
            ValueRuleBuilder.LessThan(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> LessThanOrEqual(TVal value)
        {
            ValueRuleBuilder.LessThanOrEqual(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> GreaterThan(TVal value)
        {
            ValueRuleBuilder.GreaterThan(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> GreaterThanOrEqual(TVal value)
        {
            ValueRuleBuilder.GreaterThanOrEqual(value);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> Matches(Regex regex)
        {
            ValueRuleBuilder.Matches(regex);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> Matches(string regexExpression)
        {
            ValueRuleBuilder.Matches(regexExpression);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> Matches(string regexExpression, RegexOptions options)
        {
            ValueRuleBuilder.Matches(regexExpression, options);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> Matches(Func<object, Regex> regexFunc)
        {
            ValueRuleBuilder.Matches(regexFunc);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> Matches(Func<object, string> regexExpressionFunc)
        {
            ValueRuleBuilder.Matches(regexExpressionFunc);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            ValueRuleBuilder.Matches(regexExpressionFunc, options);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> Func(Func<TVal, CustomVerifyResult> func)
        {
            ValueRuleBuilderPtr.Func(func);
            return this;
        }

        public IWaitForMessageValidationRegistrar<T, TVal> Func(Func<TVal, bool> func)
        {
            return new ValidationRegistrarWithMessage<T, TVal>(this, func);
        }

        public IWaitForMessageValidationRegistrar<T, TVal> Predicate(Predicate<TVal> predicate)
        {
            return new ValidationRegistrarWithMessage<T, TVal>(this, predicate);
        }

        public IValueFluentValidationRegistrar<T, TVal> Must(Func<TVal, CustomVerifyResult> func)
        {
            ValueRuleBuilderPtr.Must(func);
            return this;
        }

        public IWaitForMessageValidationRegistrar<T, TVal> Must(Func<TVal, bool> func)
        {
            return new ValidationRegistrarWithMessage<T, TVal>(this, func);
        }

        public new IValueFluentValidationRegistrar<T, TVal> Any(Func<object, bool> func)
        {
            ValueRuleBuilder.Any(func);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> All(Func<object, bool> func)
        {
            ValueRuleBuilder.All(func);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> NotAny(Func<object, bool> func)
        {
            ValueRuleBuilder.NotAny(func);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> NotAll(Func<object, bool> func)
        {
            ValueRuleBuilder.NotAll(func);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> In(ICollection<TVal> collection)
        {
            ValueRuleBuilder.In(collection);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> In(params TVal[] objects)
        {
            ValueRuleBuilder.In(objects);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> NotIn(ICollection<TVal> collection)
        {
            ValueRuleBuilder.NotIn(collection);
            return this;
        }

        public IValueFluentValidationRegistrar<T, TVal> NotIn(params TVal[] objects)
        {
            ValueRuleBuilder.NotIn(objects);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> InEnum(Type enumType)
        {
            ValueRuleBuilder.InEnum(enumType);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> InEnum<TEnum>()
        {
            ValueRuleBuilder.InEnum<TEnum>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> IsEnumName(Type enumType, bool caseSensitive)
        {
            ValueRuleBuilder.IsEnumName(enumType, caseSensitive);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> IsEnumName<TEnum>(bool caseSensitive)
        {
            ValueRuleBuilder.IsEnumName<TEnum>(caseSensitive);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            ValueRuleBuilder.ScalePrecision(scale, precision, ignoreTrailingZeros);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredType(Type type)
        {
            ValueRuleBuilder.RequiredType(type);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes(params Type[] types)
        {
            ValueRuleBuilder.RequiredTypes(types);
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1>()
        {
            ValueRuleBuilder.RequiredTypes<T1>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
            return this;
        }

        public new IValueFluentValidationRegistrar<T, TVal> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
            return this;
        }

        #endregion
    }
}