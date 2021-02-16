using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Registrars
{
    internal class ValueValidationRegistrar<T> : IValueFluentValidationRegistrar<T>
    {
        protected readonly IFluentValidationRegistrar<T> _parentRegistrar;
        protected readonly List<CorrectValueRule> _parentRulesRef;
        protected readonly ObjectValueContract _valueContract;

        public ValueValidationRegistrar(
            ObjectValueContract valueContract,
            List<CorrectValueRule> rules,
            ValueRuleMode mode,
            IFluentValidationRegistrar<T> parentRegistrar)
        {
            _parentRegistrar = parentRegistrar ?? throw new ArgumentNullException(nameof(parentRegistrar));
            _valueContract = valueContract ?? throw new ArgumentNullException(nameof(valueContract));
            ValueRuleBuilder = new CorrectValueRuleBuilder<T>(valueContract, mode);
            _parentRulesRef = rules;
        }

        public Type DeclaringType => _valueContract.DeclaringType;

        public Type MemberType => _valueContract.MemberType;

        protected CorrectValueRuleBuilder<T> ValueRuleBuilder { get; set; }

        #region ValueRules

        public IValueFluentValidationRegistrar<T> Empty()
        {
            ValueRuleBuilder.Empty();
            return this;
        }

        public IValueFluentValidationRegistrar<T> NotEmpty()
        {
            ValueRuleBuilder.NotEmpty();
            return this;
        }

        public IValueFluentValidationRegistrar<T> Required()
        {
            ValueRuleBuilder.Required();
            return this;
        }

        public IValueFluentValidationRegistrar<T> Null()
        {
            ValueRuleBuilder.Null();
            return this;
        }

        public IValueFluentValidationRegistrar<T> NotNull()
        {
            ValueRuleBuilder.NotNull();
            return this;
        }

        public IValueFluentValidationRegistrar<T> Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            ValueRuleBuilder.Range(from, to, options);
            return this;
        }

        public IValueFluentValidationRegistrar<T> RangeWithOpenInterval(object from, object to)
        {
            ValueRuleBuilder.RangeWithOpenInterval(from, to);
            return this;
        }

        public IValueFluentValidationRegistrar<T> RangeWithCloseInterval(object from, object to)
        {
            ValueRuleBuilder.RangeWithCloseInterval(from, to);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Length(int min, int max)
        {
            ValueRuleBuilder.Length(min, max);
            return this;
        }

        public IValueFluentValidationRegistrar<T> MinLength(int min)
        {
            ValueRuleBuilder.MinLength(min);
            return this;
        }

        public IValueFluentValidationRegistrar<T> MaxLength(int max)
        {
            ValueRuleBuilder.MaxLength(max);
            return this;
        }

        public IValueFluentValidationRegistrar<T> AtLeast(int count)
        {
            ValueRuleBuilder.AtLeast(count);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Equal(object value)
        {
            ValueRuleBuilder.Equal(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Equal(object value, IEqualityComparer comparer)
        {
            ValueRuleBuilder.Equal(value, comparer);
            return this;
        }

        public IValueFluentValidationRegistrar<T> NotEqual(object value)
        {
            ValueRuleBuilder.NotEqual(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T> NotEqual(object value, IEqualityComparer comparer)
        {
            ValueRuleBuilder.NotEqual(value, comparer);
            return this;
        }

        public IValueFluentValidationRegistrar<T> LessThan(object value)
        {
            ValueRuleBuilder.LessThan(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T> LessThanOrEqual(object value)
        {
            ValueRuleBuilder.LessThanOrEqual(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T> GreaterThan(object value)
        {
            ValueRuleBuilder.GreaterThan(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T> GreaterThanOrEqual(object value)
        {
            ValueRuleBuilder.GreaterThanOrEqual(value);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Matches(Regex regex)
        {
            ValueRuleBuilder.Matches(regex);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Matches(string regexExpression)
        {
            ValueRuleBuilder.Matches(regexExpression);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Matches(string regexExpression, RegexOptions options)
        {
            ValueRuleBuilder.Matches(regexExpression, options);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Matches(Func<object, Regex> regexFunc)
        {
            ValueRuleBuilder.Matches(regexFunc);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Matches(Func<object, string> regexExpressionFunc)
        {
            ValueRuleBuilder.Matches(regexExpressionFunc);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            ValueRuleBuilder.Matches(regexExpressionFunc, options);
            return this;
        }

        public IValueFluentValidationRegistrar<T> Func(Func<object, CustomVerifyResult> func)
        {
            ValueRuleBuilder.Func(func);
            return this;
        }

        public IWaitForMessageValidationRegistrar<T> Func(Func<object, bool> func)
        {
            return new ValidationRegistrarWithMessage<T>(this, func);
        }

        public IWaitForMessageValidationRegistrar<T> Predicate(Predicate<object> predicate)
        {
            return new ValidationRegistrarWithMessage<T>(this, predicate);
        }

        public IValueFluentValidationRegistrar<T> Must(Func<object, CustomVerifyResult> func)
        {
            ValueRuleBuilder.Must(func);
            return this;
        }

        public IWaitForMessageValidationRegistrar<T> Must(Func<object, bool> func)
        {
            return new ValidationRegistrarWithMessage<T>(this, func);
        }

        public IValueFluentValidationRegistrar<T> Any(Func<object, bool> func)
        {
            ValueRuleBuilder.Any(func);
            return this;
        }

        public IValueFluentValidationRegistrar<T> All(Func<object, bool> func)
        {
            ValueRuleBuilder.All(func);
            return this;
        }

        public IValueFluentValidationRegistrar<T> NotAny(Func<object, bool> func)
        {
            ValueRuleBuilder.NotAny(func);
            return this;
        }

        public IValueFluentValidationRegistrar<T> NotAll(Func<object, bool> func)
        {
            ValueRuleBuilder.NotAll(func);
            return this;
        }

        public IValueFluentValidationRegistrar<T> In(ICollection<object> collection)
        {
            ValueRuleBuilder.In(collection);
            return this;
        }

        public IValueFluentValidationRegistrar<T> In(params object[] objects)
        {
            ValueRuleBuilder.In(objects);
            return this;
        }

        public IValueFluentValidationRegistrar<T> NotIn(ICollection<object> collection)
        {
            ValueRuleBuilder.NotIn(collection);
            return this;
        }

        public IValueFluentValidationRegistrar<T> NotIn(params object[] objects)
        {
            ValueRuleBuilder.NotIn(objects);
            return this;
        }

        public IValueFluentValidationRegistrar<T> InEnum(Type enumType)
        {
            ValueRuleBuilder.InEnum(enumType);
            return this;
        }

        public IValueFluentValidationRegistrar<T> InEnum<TEnum>()
        {
            ValueRuleBuilder.InEnum<TEnum>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> IsEnumName(Type enumType, bool caseSensitive)
        {
            ValueRuleBuilder.IsEnumName(enumType, caseSensitive);
            return this;
        }

        public IValueFluentValidationRegistrar<T> IsEnumName<TEnum>(bool caseSensitive)
        {
            ValueRuleBuilder.IsEnumName<TEnum>(caseSensitive);
            return this;
        }

        public IValueFluentValidationRegistrar<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            ValueRuleBuilder.ScalePrecision(scale, precision, ignoreTrailingZeros);
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredType(Type type)
        {
            ValueRuleBuilder.RequiredType(type);
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes(params Type[] types)
        {
            ValueRuleBuilder.RequiredTypes(types);
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1>()
        {
            ValueRuleBuilder.RequiredTypes<T1>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
            return this;
        }

        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
            return this;
        }

        #endregion

        #region AndForMember

        public IValueFluentValidationRegistrar<T> AndForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForMember(memberName, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForMember(propertyInfo, mode);
        }

        public IValueFluentValidationRegistrar<T> AndForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForMember(fieldInfo, mode);
        }

        public IValueFluentValidationRegistrar<T, TVal> AndForMember<TVal>(Expression<Func<T, TVal>> expression, ValueRuleMode mode = ValueRuleMode.Append)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForMember(expression, mode);
        }

        #endregion

        #region AndForType

        public IFluentValidationRegistrar AndForType(Type type)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForType(type);
        }

        public IFluentValidationRegistrar AndForType(Type type, string name)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForType(type, name);
        }

        public IFluentValidationRegistrar<TType> AndForType<TType>()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForType<TType>();
        }

        public IFluentValidationRegistrar<TType> AndForType<TType>(string name)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForType<TType>(name);
        }

        #endregion

        #region Build

        internal void BuildMySelf()
        {
            var rule = ValueRuleBuilder.Build();

            _parentRulesRef.Add(rule);
        }

        public void Build()
        {
            BuildMySelf();
            _parentRegistrar.Build();
        }

        #endregion
    }
}