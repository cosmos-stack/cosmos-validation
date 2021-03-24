using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Strategies;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    internal class ValueValidationRegistrar<T> : IValueFluentValidationRegistrar<T>
    {
        protected readonly IValidationRegistrar _rootRegistrar;
        protected readonly IFluentValidationRegistrar<T> _parentRegistrar;
        protected readonly List<CorrectValueRule> _parentRulesRef;
        protected readonly VerifiableMemberContract _verifiableMemberContract;

        public ValueValidationRegistrar(
            VerifiableMemberContract verifiableMemberContract,
            List<CorrectValueRule> rules,
            ValueRuleMode mode,
            IFluentValidationRegistrar<T> parentRegistrar,
            IValidationRegistrar rootRegistrar)
        {
            _rootRegistrar = rootRegistrar ?? throw new ArgumentNullException(nameof(rootRegistrar));
            _parentRegistrar = parentRegistrar ?? throw new ArgumentNullException(nameof(parentRegistrar));
            _verifiableMemberContract = verifiableMemberContract ?? throw new ArgumentNullException(nameof(verifiableMemberContract));
            ValueRuleBuilder = new CorrectValueRuleBuilder<T>(verifiableMemberContract, mode);
            _parentRulesRef = rules;
        }

        public Type DeclaringType => _verifiableMemberContract.DeclaringType;

        public Type MemberType => _verifiableMemberContract.MemberType;

        #region ValueRuleBuilder

        protected CorrectValueRuleBuilder<T> ValueRuleBuilder { get; set; }

        internal CorrectValueRuleBuilder<T> ExposeValueRuleBuilder() => ValueRuleBuilder;

        #endregion

        #region WithConfig

        public IValueFluentValidationRegistrar<T> WithConfig(Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func)
        {
            var builder = func?.Invoke(ValueRuleBuilder);

            if (builder is not null)
                ValueRuleBuilder = (CorrectValueRuleBuilder<T>) builder;

            return this;
        }

        #endregion
        
        #region Condition

        public IValueFluentValidationRegistrar<T> And()
        {
            ValueRuleBuilder.And();
            return this;
        }
        
        public IValueFluentValidationRegistrar<T> Or()
        {
            ValueRuleBuilder.Or();
            return this;
        }

        #endregion

        #region ValueRules

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

        public IValueFluentValidationRegistrar<T> Func(Func<object, CustomVerifyResult> func)
        {
            ValueRuleBuilder.Func(func);
            return this;
        }

        public IWaitForMessageValidationRegistrar<T> Func(Func<object, bool> func)
        {
            return new ValidationRegistrarWithMessage<T>(this, _rootRegistrar, func);
        }

        public IWaitForMessageValidationRegistrar<T> Predicate(Predicate<object> predicate)
        {
            return new ValidationRegistrarWithMessage<T>(this, _rootRegistrar, predicate);
        }

        public IValueFluentValidationRegistrar<T> Must(Func<object, CustomVerifyResult> func)
        {
            ValueRuleBuilder.Must(func);
            return this;
        }

        public IWaitForMessageValidationRegistrar<T> Must(Func<object, bool> func)
        {
            return new ValidationRegistrarWithMessage<T>(this, _rootRegistrar, func);
        }

        public IWaitForMessageValidationRegistrar<T> Satisfies(Func<object, bool> func)
        {
            return new ValidationRegistrarWithMessage<T>(this, _rootRegistrar, func);
        }

        public IValueFluentValidationRegistrar<T> Satisfies(Func<object, bool> func,string message)
        {
            return Satisfies(func).WithMessage(message);
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

        /// <summary>
        /// Limit the scale and precision of the value.
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="precision"></param>
        /// <param name="ignoreTrailingZeros"></param>
        /// <returns></returns>
        public IValueFluentValidationRegistrar<T> ScalePrecision(int scale, int precision, bool ignoreTrailingZeros = false)
        {
            ValueRuleBuilder.ScalePrecision(scale, precision, ignoreTrailingZeros);
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1>()
        {
            ValueRuleBuilder.RequiredTypes<T1>();
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2>();
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <returns></returns>
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3>();
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <returns></returns>
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4>();
            return this;
        }

        /// <summary>
        /// To restrict the type, it must be one of the given types (equal, or a derived class).
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <returns></returns>
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>();
            return this;
        }

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
        public IValueFluentValidationRegistrar<T> RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>()
        {
            ValueRuleBuilder.RequiredTypes<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>();
            return this;
        }

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

        #region AndForStrategy

        public IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy<TStrategy>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TStrategy, TType>(StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<TType>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy<TStrategy, TType>(mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy(strategy, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TType>(IValidationStrategy<TType> strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
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

        public IFluentValidationRegistrar AndForStrategy<TStrategy, TType>(string name, StrategyMode mode = StrategyMode.OverallOverwrite)
            where TStrategy : class, IValidationStrategy<TType>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy<TStrategy, TType>(name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy(strategy, name, mode);
        }

        public IFluentValidationRegistrar AndForStrategy<TType>(IValidationStrategy<TType> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForStrategy(strategy, name, mode);
        }

        #endregion

        #region AndForCustomValidator

        public IFluentValidationRegistrar AndForCustomValidator<TValidator>() where TValidator : CustomValidator, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForCustomValidator<TValidator>();
        }

        public IFluentValidationRegistrar AndForCustomValidator<TValidator, TType>() where TValidator : CustomValidator<TType>, new()
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForCustomValidator<TValidator, TType>();
        }

        public IFluentValidationRegistrar AndForCustomValidator(CustomValidator validator)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForCustomValidator(validator);
        }

        public IFluentValidationRegistrar AndForCustomValidator<TType>(CustomValidator<TType> validator)
        {
            //step 1: build this register
            BuildMySelf();

            //step 2: create a new register
            return _parentRegistrar.AndForCustomValidator(validator);
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

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver)
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild(objectResolver);
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver, ValidationOptions options)
        {
            BuildMySelf();
            return _parentRegistrar.TempBuild(objectResolver, options);
        }

        #endregion

        #region TakeEffect

        public void TakeEffect()
        {
            BuildMySelf();
            _parentRegistrar.TakeEffect();
        }

        public IValidationRegistrar TakeEffectAndBack()
        {
            TakeEffect();
            return _rootRegistrar;
        }

        #endregion
    }
}