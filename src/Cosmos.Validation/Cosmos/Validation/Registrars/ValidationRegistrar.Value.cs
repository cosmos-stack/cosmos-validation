using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Strategies;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    internal class ValueValidationRegistrar : IValueFluentValidationRegistrar
    {
        private readonly IFluentValidationRegistrar _parentRegistrar;
        private readonly List<CorrectValueRule> _parentRulesRef;
        private readonly ObjectValueContract _valueContract;

        public ValueValidationRegistrar(
            ObjectValueContract valueContract,
            List<CorrectValueRule> rules,
            ValueRuleMode mode,
            IFluentValidationRegistrar parentRegistrar)
        {
            _parentRegistrar = parentRegistrar ?? throw new ArgumentNullException(nameof(parentRegistrar));
            _valueContract = valueContract ?? throw new ArgumentNullException(nameof(valueContract));
            ValueRuleBuilder = new CorrectValueRuleBuilder(valueContract, mode);
            _parentRulesRef = rules;
        }

        public Type DeclaringType => _valueContract.DeclaringType;

        public Type MemberType => _valueContract.MemberType;

        private CorrectValueRuleBuilder ValueRuleBuilder { get; set; }

        #region WithConfig

        public IValueFluentValidationRegistrar WithConfig(Func<IValueRuleBuilder, IValueRuleBuilder> func)
        {
            var builder = func?.Invoke(ValueRuleBuilder);

            if (builder is not null)
                ValueRuleBuilder = (CorrectValueRuleBuilder) builder;

            return this;
        }

        #endregion

        #region ValueRules

        public IValueFluentValidationRegistrar Empty()
        {
            ValueRuleBuilder.Empty();
            return this;
        }

        public IValueFluentValidationRegistrar NotEmpty()
        {
            ValueRuleBuilder.NotEmpty();
            return this;
        }

        public IValueFluentValidationRegistrar Required()
        {
            ValueRuleBuilder.Required();
            return this;
        }

        public IValueFluentValidationRegistrar Null()
        {
            ValueRuleBuilder.Null();
            return this;
        }

        public IValueFluentValidationRegistrar NotNull()
        {
            ValueRuleBuilder.NotNull();
            return this;
        }

        public IValueFluentValidationRegistrar Range(object from, object to, RangeOptions options = RangeOptions.OpenInterval)
        {
            ValueRuleBuilder.Range(from, to, options);
            return this;
        }

        public IValueFluentValidationRegistrar RangeWithOpenInterval(object from, object to)
        {
            ValueRuleBuilder.RangeWithOpenInterval(from, to);
            return this;
        }

        public IValueFluentValidationRegistrar RangeWithCloseInterval(object from, object to)
        {
            ValueRuleBuilder.RangeWithCloseInterval(from, to);
            return this;
        }

        public IValueFluentValidationRegistrar Length(int min, int max)
        {
            ValueRuleBuilder.Length(min, max);
            return this;
        }

        public IValueFluentValidationRegistrar MinLength(int min)
        {
            ValueRuleBuilder.MinLength(min);
            return this;
        }

        public IValueFluentValidationRegistrar MaxLength(int max)
        {
            ValueRuleBuilder.MaxLength(max);
            return this;
        }

        public IValueFluentValidationRegistrar AtLeast(int count)
        {
            ValueRuleBuilder.AtLeast(count);
            return this;
        }

        public IValueFluentValidationRegistrar Equal(object value)
        {
            ValueRuleBuilder.Equal(value);
            return this;
        }

        public IValueFluentValidationRegistrar Equal(object value, IEqualityComparer comparer)
        {
            ValueRuleBuilder.Equal(value, comparer);
            return this;
        }

        public IValueFluentValidationRegistrar NotEqual(object value)
        {
            ValueRuleBuilder.NotEqual(value);
            return this;
        }

        public IValueFluentValidationRegistrar NotEqual(object value, IEqualityComparer comparer)
        {
            ValueRuleBuilder.NotEqual(value, comparer);
            return this;
        }

        public IValueFluentValidationRegistrar LessThan(object value)
        {
            ValueRuleBuilder.LessThan(value);
            return this;
        }

        public IValueFluentValidationRegistrar LessThanOrEqual(object value)
        {
            ValueRuleBuilder.LessThanOrEqual(value);
            return this;
        }

        public IValueFluentValidationRegistrar GreaterThan(object value)
        {
            ValueRuleBuilder.GreaterThan(value);
            return this;
        }

        public IValueFluentValidationRegistrar GreaterThanOrEqual(object value)
        {
            ValueRuleBuilder.GreaterThanOrEqual(value);
            return this;
        }

        public IValueFluentValidationRegistrar Matches(Regex regex)
        {
            ValueRuleBuilder.Matches(regex);
            return this;
        }

        public IValueFluentValidationRegistrar Matches(string regexExpression)
        {
            ValueRuleBuilder.Matches(regexExpression);
            return this;
        }

        public IValueFluentValidationRegistrar Matches(string regexExpression, RegexOptions options)
        {
            ValueRuleBuilder.Matches(regexExpression, options);
            return this;
        }

        public IValueFluentValidationRegistrar Matches(Func<object, Regex> regexFunc)
        {
            ValueRuleBuilder.Matches(regexFunc);
            return this;
        }

        public IValueFluentValidationRegistrar Matches(Func<object, string> regexExpressionFunc)
        {
            ValueRuleBuilder.Matches(regexExpressionFunc);
            return this;
        }

        public IValueFluentValidationRegistrar Matches(Func<object, string> regexExpressionFunc, RegexOptions options)
        {
            ValueRuleBuilder.Matches(regexExpressionFunc, options);
            return this;
        }

        public IValueFluentValidationRegistrar Func(Func<object, CustomVerifyResult> func)
        {
            ValueRuleBuilder.Func(func);
            return this;
        }

        public IWaitForMessageValidationRegistrar Func(Func<object, bool> func)
        {
            return new ValidationRegistrarWithMessage(this, func);
        }

        public IWaitForMessageValidationRegistrar Predicate(Predicate<object> predicate)
        {
            return new ValidationRegistrarWithMessage(this, predicate);
        }

        public IValueFluentValidationRegistrar Must(Func<object, CustomVerifyResult> func)
        {
            ValueRuleBuilder.Must(func);
            return this;
        }

        public IWaitForMessageValidationRegistrar Must(Func<object, bool> func)
        {
            return new ValidationRegistrarWithMessage(this, func);
        }

        public IValueFluentValidationRegistrar Any(Func<object, bool> func)
        {
            ValueRuleBuilder.Any(func);
            return this;
        }

        public IValueFluentValidationRegistrar All(Func<object, bool> func)
        {
            ValueRuleBuilder.All(func);
            return this;
        }

        public IValueFluentValidationRegistrar NotAny(Func<object, bool> func)
        {
            ValueRuleBuilder.NotAny(func);
            return this;
        }

        public IValueFluentValidationRegistrar NotAll(Func<object, bool> func)
        {
            ValueRuleBuilder.NotAll(func);
            return this;
        }

        public IValueFluentValidationRegistrar In(ICollection<object> collection)
        {
            ValueRuleBuilder.In(collection);
            return this;
        }

        public IValueFluentValidationRegistrar In(params object[] objects)
        {
            ValueRuleBuilder.In(objects);
            return this;
        }

        public IValueFluentValidationRegistrar NotIn(ICollection<object> collection)
        {
            ValueRuleBuilder.NotIn(collection);
            return this;
        }

        public IValueFluentValidationRegistrar NotIn(params object[] objects)
        {
            ValueRuleBuilder.NotIn(objects);
            return this;
        }

        public IValueFluentValidationRegistrar InEnum(Type enumType)
        {
            ValueRuleBuilder.InEnum(enumType);
            return this;
        }

        public IValueFluentValidationRegistrar InEnum<TEnum>()
        {
            ValueRuleBuilder.InEnum<TEnum>();
            return this;
        }

        public IValueFluentValidationRegistrar IsEnumName(Type enumType, bool caseSensitive)
        {
            ValueRuleBuilder.IsEnumName(enumType, caseSensitive);
            return this;
        }

        public IValueFluentValidationRegistrar IsEnumName<TEnum>(bool caseSensitive)
        {
            ValueRuleBuilder.IsEnumName<TEnum>(caseSensitive);
            return this;
        }

        public IValueFluentValidationRegistrar ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            ValueRuleBuilder.ScalePrecision(scale, precision, ignoreTrailingZeros);
            return this;
        }

        public IValueFluentValidationRegistrar RequiredType(Type type)
        {
            ValueRuleBuilder.RequiredType(type);
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes(params Type[] types)
        {
            ValueRuleBuilder.RequiredTypes(types);
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1>()
        {
            ValueRuleBuilder.RequiredTypes<T1>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
            return this;
        }

        public IValueFluentValidationRegistrar RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>();
            return this;
        }

        #endregion

        #region AndForMember

        public IValueFluentValidationRegistrar AndForMember(string memberName, ValueRuleMode mode = ValueRuleMode.Append)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForMember(memberName, mode);
        }

        public IValueFluentValidationRegistrar AndForMember(PropertyInfo propertyInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForMember(propertyInfo, mode);
        }

        public IValueFluentValidationRegistrar AndForMember(FieldInfo fieldInfo, ValueRuleMode mode = ValueRuleMode.Append)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.ForMember(fieldInfo, mode);
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

        public IFluentValidationRegistrar<T> AndForType<T>()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForType<T>();
        }

        public IFluentValidationRegistrar<T> AndForType<T>(string name)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForType<T>(name);
        }

        #endregion

        #region AndForStrategy

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy<TStrategy>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<T>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy<TStrategy, T>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy(strategy, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy(strategy, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy<TStrategy>(name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<T>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy<TStrategy, T>(name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy(strategy, name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy(strategy, name, mode);
        }

        #endregion
        
        #region RegisterValidator

        public IFluentValidationRegistrar AndForValidator<TValidator>() where TValidator : CustomValidator, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForValidator<TValidator>();
        }

        public IFluentValidationRegistrar AndForValidator<TValidator, T>() where TValidator : CustomValidator<T>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForValidator<TValidator, T>();
        }

        public IFluentValidationRegistrar AndForValidator(CustomValidator validator)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForValidator(validator);
        }

        public IFluentValidationRegistrar AndForValidator<T>(CustomValidator<T> validator)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForValidator(validator);
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

        public ValidationHandler TempBuild()
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild();
        }

        public ValidationHandler TempBuild(ValidationOptions options)
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild(options);
        }

        public ValidationHandler TempBuild(Action<ValidationOptions> optionsAct)
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild(optionsAct);
        }

        #endregion
    }
}