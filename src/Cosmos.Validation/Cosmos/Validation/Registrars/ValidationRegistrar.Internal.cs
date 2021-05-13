using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
using Cosmos.Validation.Registrars.Interfaces;
using Cosmos.Validation.Strategies;
using Cosmos.Validation.Validators;

// ReSharper disable PossibleMultipleEnumeration

namespace Cosmos.Validation.Registrars
{
    internal class InternalValidationRegistrar : IValidationRegistrar, ICorrectRegistrar
    {
        private readonly RegisterMode _registerMode;
        private readonly string _nameOfProvider;
        private readonly IValidationProvider _targetProvider;

        protected readonly Dictionary<Type, List<CorrectValueRule>> _typedRulesDictionary = new();
        protected readonly Dictionary<(Type, string), List<CorrectValueRule>> _namedRulesDictionary = new();
        protected readonly object _valueRuleLockObj = new();

        private ICorrectProvider InnerPtr => (ICorrectProvider) _targetProvider;

        private ICustomValidatorManager CustomValidatorManager => InnerPtr.ExposeCustomValidatorManager();

        public string Name => _nameOfProvider;

        public InternalValidationRegistrar(
            IValidationProvider targetProvider,
            RegisterMode mode,
            string nameOfProvider)
        {
            _targetProvider = targetProvider;
            _registerMode = mode;
            _nameOfProvider = nameOfProvider;
        }

        #region ForStrategy

        public IValidationRegistrar ForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new()
        {
            var rel = (ICorrectStrategy) new TStrategy();

            AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), rel.SourceType, mode);

            return this;
        }

        public IValidationRegistrar ForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new()
        {
            var rel = (ICorrectStrategy<T>) new TStrategy();

            AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), rel.SourceType, mode);

            return this;
        }

        public IValidationRegistrar ForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            if (strategy is null) throw new ArgumentNullException(nameof(strategy));

            var rel = (ICorrectStrategy) strategy;

            AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), rel.SourceType, mode);

            return this;
        }

        public IValidationRegistrar ForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            if (strategy is null) throw new ArgumentNullException(nameof(strategy));

            var rel = (ICorrectStrategy<T>) strategy;

            AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), rel.SourceType, mode);

            return this;
        }

        public IValidationRegistrar ForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new()
        {
            var rel = (ICorrectStrategy) new TStrategy();

            AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), rel.SourceType, name, mode);

            return this;
        }

        public IValidationRegistrar ForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new()
        {
            var rel = (ICorrectStrategy<T>) new TStrategy();

            AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), rel.SourceType, name, mode);

            return this;
        }

        public IValidationRegistrar ForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            if (strategy is null) throw new ArgumentNullException(nameof(strategy));

            var rel = (ICorrectStrategy) strategy;

            AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), rel.SourceType, name, mode);

            return this;
        }

        public IValidationRegistrar ForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            if (strategy is null) throw new ArgumentNullException(nameof(strategy));

            var rel = (ICorrectStrategy<T>) strategy;

            AddOrUpdateValueRules(rel.GetValueRuleBuilders().Select(builder => builder.Build()), rel.SourceType, name, mode);

            return this;
        }

        #endregion

        #region ForRulePackage

        public IValidationRegistrar ForRulePackage(VerifyRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            if (package is null) throw new ArgumentNullException(nameof(package));

            var strategyMode = mode switch
            {
                VerifyRuleMode.Append => StrategyMode.Append,
                VerifyRuleMode.Overwrite => StrategyMode.ItemOverwrite,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };

            AddOrUpdateValueRules(package.ExposeRules(), package.DeclaringType, strategyMode);

            return this;
        }

        public IValidationRegistrar ForRulePackage(VerifyRulePackage package, string name, VerifyRuleMode mode = VerifyRuleMode.Append)
        {
            if (package is null) throw new ArgumentNullException(nameof(package));

            var strategyMode = mode switch
            {
                VerifyRuleMode.Append => StrategyMode.Append,
                VerifyRuleMode.Overwrite => StrategyMode.ItemOverwrite,
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };

            AddOrUpdateValueRules(package.ExposeRules(), package.DeclaringType, name, strategyMode);

            return this;
        }

        #endregion

        #region ForCustomValidator

        public IValidationRegistrar ForCustomValidator<TValidator>() where TValidator : CustomValidator, new()
        {
            CustomValidatorManager.Register<TValidator>();
            return this;
        }

        public IValidationRegistrar ForCustomValidator<TValidator, T>() where TValidator : CustomValidator<T>, new()
        {
            CustomValidatorManager.Register<TValidator, T>();
            return this;
        }

        public IValidationRegistrar ForCustomValidator(CustomValidator validator)
        {
            CustomValidatorManager.Register(validator);
            return this;
        }

        public IValidationRegistrar ForCustomValidator<T>(CustomValidator<T> validator)
        {
            CustomValidatorManager.Register(validator);
            return this;
        }

        #endregion

        #region ForType

        public IFluentValidationRegistrar ForType(Type type)
        {
            return new FluentValidationRegistrar(type, this);
        }

        public IFluentValidationRegistrar ForType(Type type, string name)
        {
            return new FluentValidationRegistrar(type, name, this);
        }

        public IFluentValidationRegistrar<T> ForType<T>()
        {
            return new FluentValidationRegistrar<T>(this);
        }

        public IFluentValidationRegistrar<T> ForType<T>(string name)
        {
            return new FluentValidationRegistrar<T>(name, this);
        }

        #endregion

        #region Build

        public void Build()
        {
            var manager = GetProjectManager();

            if (manager is null)
                throw new InvalidOperationException("Invalid operation because cannot get valid ProjectManager.");

            foreach (var project in GetProjects())
                manager.Register(project);
        }

        public ValidationHandler TempBuild()
        {
            return new(
                GetProjects(),
                InnerPtr.ExposeObjectResolver(),
                CustomValidatorManager,
                InnerPtr.ExposeValidationOptions());
        }

        public ValidationHandler TempBuild(ValidationOptions options)
        {
            return new(
                GetProjects(),
                InnerPtr.ExposeObjectResolver(),
                CustomValidatorManager,
                options ?? InnerPtr.ExposeValidationOptions());
        }

        public ValidationHandler TempBuild(Action<ValidationOptions> optionsAct)
        {
            var options = new ValidationOptions();
            optionsAct?.Invoke(options);

            return new(
                GetProjects(),
                InnerPtr.ExposeObjectResolver(),
                CustomValidatorManager,
                options);
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver)
        {
            return new(
                GetProjects(),
                objectResolver ?? InnerPtr.ExposeObjectResolver(),
                CustomValidatorManager,
                InnerPtr.ExposeValidationOptions());
        }

        public ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver, ValidationOptions options)
        {
            return new(
                GetProjects(),
                objectResolver ?? InnerPtr.ExposeObjectResolver(),
                CustomValidatorManager,
                options ?? InnerPtr.ExposeValidationOptions());
        }

        public ValidationHandler TempBuild(ValidationHandler handler)
        {
            return handler.Merge(GetProjects());
        }

        #endregion

        #region BuildForMember

        void ICorrectRegistrar.BuildForMember(Type type, string memberName, Func<IValueRuleBuilder, IValueRuleBuilder> func)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var contract = VerifiableObjectContractManager.Resolve(type);
            var value = contract.GetMemberContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{type.GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder(value);
            var rule = ((CorrectValueRuleBuilder) func(builder)).Build();
            AddOrUpdateValueRule(rule, type);
        }

        void ICorrectRegistrar.BuildForMember(Type type, string memberName, string name, Func<IValueRuleBuilder, IValueRuleBuilder> func)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ((ICorrectRegistrar) this).BuildForMember(type, memberName, func);
                return;
            }

            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var contract = VerifiableObjectContractManager.Resolve(type);
            var value = contract.GetMemberContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{type.GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder(value);
            var rule = ((CorrectValueRuleBuilder) func(builder)).Build();
            AddOrUpdateValueRule(rule, type, name);
        }

        void ICorrectRegistrar.BuildForMember(VerifiableMemberContract contract, Func<IValueRuleBuilder, IValueRuleBuilder> func)
        {
            if (contract is null)
                throw new ArgumentNullException(nameof(contract));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var builder = new CorrectValueRuleBuilder(contract);
            var rule = ((CorrectValueRuleBuilder) func(builder)).Build();
            AddOrUpdateValueRule(rule, contract.DeclaringType);
        }

        void ICorrectRegistrar.BuildForMember(VerifiableMemberContract contract, string name, Func<IValueRuleBuilder, IValueRuleBuilder> func)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ((ICorrectRegistrar) this).BuildForMember(contract, func);
                return;
            }

            if (contract is null)
                throw new ArgumentNullException(nameof(contract));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var builder = new CorrectValueRuleBuilder(contract);
            var rule = ((CorrectValueRuleBuilder) func(builder)).Build();
            AddOrUpdateValueRule(rule, contract.DeclaringType, name);
        }

        void ICorrectRegistrar.BuildForMember(CorrectValueRule rule)
        {
            if (rule is null)
                throw new ArgumentNullException(nameof(rule));
            AddOrUpdateValueRule(rule, rule.Contract.DeclaringType);
        }

        void ICorrectRegistrar.BuildForMember(string name, CorrectValueRule rule)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ((ICorrectRegistrar) this).BuildForMember(rule);
                return;
            }

            if (rule is null)
                throw new ArgumentNullException(nameof(rule));
            AddOrUpdateValueRule(rule, rule.Contract.DeclaringType, name);
        }

        void ICorrectRegistrar.BuildForMember<T>(string memberName, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func)
        {
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var contract = VerifiableObjectContractManager.Resolve<T>();
            var value = contract.GetMemberContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{typeof(T).GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder<T>(value);
            var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
            AddOrUpdateValueRule(rule, typeof(T));
        }

        void ICorrectRegistrar.BuildForMember<T>(string memberName, string name, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ((ICorrectRegistrar) this).BuildForMember(memberName, func);
                return;
            }

            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var contract = VerifiableObjectContractManager.Resolve<T>();
            var value = contract.GetMemberContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{typeof(T).GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder<T>(value);
            var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
            AddOrUpdateValueRule(rule, typeof(T), name);
        }

        void ICorrectRegistrar.BuildForMember<T>(VerifiableMemberContract contract, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func)
        {
            if (contract is null)
                throw new ArgumentNullException(nameof(contract));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var builder = new CorrectValueRuleBuilder<T>(contract);
            var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
            AddOrUpdateValueRule(rule, contract.DeclaringType);
        }

        void ICorrectRegistrar.BuildForMember<T>(VerifiableMemberContract contract, string name, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ((ICorrectRegistrar) this).BuildForMember(contract, func);
                return;
            }

            if (contract is null)
                throw new ArgumentNullException(nameof(contract));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var builder = new CorrectValueRuleBuilder<T>(contract);
            var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
            AddOrUpdateValueRule(rule, contract.DeclaringType, name);
        }

        void ICorrectRegistrar.BuildForMember<T>(CorrectValueRule rule)
        {
            if (rule is null)
                throw new ArgumentNullException(nameof(rule));
            AddOrUpdateValueRule(rule, rule.Contract.DeclaringType);
        }

        void ICorrectRegistrar.BuildForMember<T>(string name, CorrectValueRule rule)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ((ICorrectRegistrar) this).BuildForMember<T>(rule);
                return;
            }

            if (rule is null)
                throw new ArgumentNullException(nameof(rule));
            AddOrUpdateValueRule(rule, rule.Contract.DeclaringType, name);
        }

        void ICorrectRegistrar.BuildForMember<T, TVal>(Expression<Func<T, TVal>> expression, Func<IValueRuleBuilder<T, TVal>, IValueRuleBuilder<T, TVal>> func)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var memberName = PropertySelector.GetPropertyName(expression);
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentException("No member can be matched.", nameof(memberName));
            var contract = VerifiableObjectContractManager.Resolve<T>();
            var value = contract.GetMemberContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{typeof(T).GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder<T, TVal>(value);
            var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
            AddOrUpdateValueRule(rule, typeof(T));
        }

        void ICorrectRegistrar.BuildForMember<T, TVal>(Expression<Func<T, TVal>> expression, string name, Func<IValueRuleBuilder<T, TVal>, IValueRuleBuilder<T, TVal>> func)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                ((ICorrectRegistrar) this).BuildForMember(expression, func);
                return;
            }

            if (expression is null)
                throw new ArgumentNullException(nameof(expression));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var memberName = PropertySelector.GetPropertyName(expression);
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentException("No member can be matched.", nameof(memberName));
            var contract = VerifiableObjectContractManager.Resolve<T>();
            var value = contract.GetMemberContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{typeof(T).GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder<T, TVal>(value);
            var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
            AddOrUpdateValueRule(rule, typeof(T), name);
        }

        #endregion

        #region TakeEffect

        public void TakeEffect() { }

        public IValidationRegistrar TakeEffectAndBack()
        {
            TakeEffect();
            return this;
        }

        #endregion

        #region AddOrUpdateValueRule

        protected void AddOrUpdateValueRules(IEnumerable<CorrectValueRule> rules, Type sourceType, StrategyMode mode)
        {
            lock (_valueRuleLockObj)
            {
                if (_typedRulesDictionary.TryGetValue(sourceType, out var gotRules))
                {
                    if (mode == StrategyMode.OverallOverwrite)
                        gotRules.Clear();
                }
                else
                {
                    gotRules = new List<CorrectValueRule>();
                    _typedRulesDictionary[sourceType] = gotRules;
                }

                foreach (var rule in rules)
                {
                    var target = gotRules.FirstOrDefault(r => r.MemberName == rule.MemberName);

                    if (target is null)
                    {
                        // If the value of StrategyMode is OverallOverwrite,
                        // this branch must be entered.
                        gotRules.Add(rule);
                    }

                    else if (mode == StrategyMode.ItemOverwrite)
                    {
                        gotRules.Remove(target);
                        gotRules.Add(rule);
                    }

                    else if (mode == StrategyMode.Append)
                    {
                        target.Merge(rule);
                    }
                }
            }
        }

        protected void AddOrUpdateValueRules(IEnumerable<CorrectValueRule> rules, Type sourceType, string name, StrategyMode mode)
        {
            lock (_valueRuleLockObj)
            {
                if (_namedRulesDictionary.TryGetValue((sourceType, name), out var gotRules))
                {
                    if (mode == StrategyMode.OverallOverwrite)
                        gotRules.Clear();
                }
                else
                {
                    gotRules = new List<CorrectValueRule>();
                    _namedRulesDictionary[(sourceType, name)] = gotRules;
                }

                foreach (var rule in rules)
                {
                    var target = gotRules.FirstOrDefault(r => r.MemberName == rule.MemberName);

                    if (target is null)
                    {
                        // If the value of StrategyMode is OverallOverwrite,
                        // this branch must be entered.
                        gotRules.Add(rule);
                    }

                    else if (mode == StrategyMode.ItemOverwrite)
                    {
                        gotRules.Remove(target);
                        gotRules.Add(rule);
                    }

                    else if (mode == StrategyMode.Append)
                    {
                        target.Merge(rule);
                    }
                }
            }
        }

        protected void AddOrUpdateValueRule(CorrectValueRule rule, Type sourceType)
        {
            lock (_valueRuleLockObj)
            {
                if (!_typedRulesDictionary.TryGetValue(sourceType, out var gotRules))
                {
                    gotRules = new List<CorrectValueRule>();
                    _typedRulesDictionary[sourceType] = gotRules;
                }

                var target = gotRules.FirstOrDefault(r => r.MemberName == rule.MemberName);

                if (target is null)
                {
                    // Regardless of the value of ValueRuleMode, as long as the rule does not exist,
                    // this branch is always entered.
                    gotRules.Add(rule);
                }

                else if (rule.Mode == CorrectValueRuleMode.Overwrite)
                {
                    gotRules.Remove(target);
                    gotRules.Add(rule);
                }

                else if (rule.Mode == CorrectValueRuleMode.Append)
                {
                    target.Merge(rule);
                }
            }
        }

        protected void AddOrUpdateValueRule(CorrectValueRule rule, Type sourceType, string name)
        {
            lock (_valueRuleLockObj)
            {
                if (!_namedRulesDictionary.TryGetValue((sourceType, name), out var gotRules))
                {
                    gotRules = new List<CorrectValueRule>();
                    _namedRulesDictionary[(sourceType, name)] = gotRules;
                }

                var target = gotRules.FirstOrDefault(r => r.MemberName == rule.MemberName);

                if (target is null)
                {
                    // Regardless of the value of ValueRuleMode, as long as the rule does not exist,
                    // this branch is always entered.
                    gotRules.Add(rule);
                }

                else if (rule.Mode == CorrectValueRuleMode.Overwrite)
                {
                    gotRules.Remove(target);
                    gotRules.Add(rule);
                }

                else if (rule.Mode == CorrectValueRuleMode.Append)
                {
                    target.Merge(rule);
                }
            }
        }

        #endregion

        #region GetProjects/GetProjectManager

        private IValidationProjectManager GetProjectManager()
        {
            ICorrectProvider provider;

            if (_registerMode == RegisterMode.Direct)
            {
                // 直接注册到 build in provider 内
                provider = (ICorrectProvider) ValidationMe.ExposeDefaultProvider();
            }

            else if (_registerMode == RegisterMode.Hosted && ValidationProvider.IsDefault(Name))
            {
                // 直接注册到 Main Custom Provider 内
                provider = (ICorrectProvider) ValidationMe.ExposeValidationProvider();
            }

            else if (_registerMode == RegisterMode.Hosted)
            {
                // 找到对应 Name 的 Provider，然后用给定的策略名称执行注册
                // 如果不存在，则使用兜底方案（build in provider）
                provider = (ICorrectProvider) ValidationMe.ExposeValidationProvider(Name)
                        ?? (ICorrectProvider) ValidationMe.ExposeDefaultProvider();
            }
            else
            {
                // 兜底默认，使用 build in provider
                provider = (ICorrectProvider) ValidationMe.ExposeDefaultProvider();
            }

            return provider.ExposeProjectManager();
        }

        private IEnumerable<IProject> GetProjects()
        {
            foreach (var project in ProjectFactory.CreateTypeProject(_typedRulesDictionary))
                yield return project;
            foreach (var project in ProjectFactory.CreateNamedTypeProject(_namedRulesDictionary))
                yield return project;
        }

        internal (Dictionary<Type, List<CorrectValueRule>>, Dictionary<(Type, string), List<CorrectValueRule>>) GetCorrectValueRulesForUnitTests()
        {
            return (_typedRulesDictionary, _namedRulesDictionary);
        }

        #endregion

        #region ExposeVerifyRulePackage

        public VerifyRulePackage ExposeRulePackage<T>(string projectName = "")
        {
            var manager = GetProjectManager();

            if (manager is null)
                return VerifyRulePackage.Empty;

            if (manager.TryResolve(typeof(T), projectName, out var project))
                return project.ExposeRules();

            return VerifyRulePackage.Empty;
        }

        public VerifyRulePackage ExposeRulePackage(Type declaringType, string projectName = "")
        {
            if (declaringType is null)
                return VerifyRulePackage.Empty;

            var manager = GetProjectManager();

            if (manager is null)
                return VerifyRulePackage.Empty;

            if (manager.TryResolve(declaringType, projectName, out var project))
                return project.ExposeRules();

            return VerifyRulePackage.Empty;
        }

        public VerifyRulePackage ExposeUnregisteredRulePackage<T>(string projectName = "")
        {
            lock (_valueRuleLockObj)
            {
                if (string.IsNullOrWhiteSpace(projectName))
                {
                    if (_typedRulesDictionary.TryGetValue(typeof(T), out var rules))
                        return new VerifyRulePackage(typeof(T), rules);
                }
                else
                {
                    if (_namedRulesDictionary.TryGetValue((typeof(T), projectName), out var rules))
                        return new VerifyRulePackage(typeof(T), rules);
                }

                return VerifyRulePackage.Empty;
            }
        }

        public VerifyRulePackage ExposeUnregisteredRulePackage(Type declaringType, string projectName = "")
        {
            if (declaringType is null)
            {
                return VerifyRulePackage.Empty;
            }

            lock (_valueRuleLockObj)
            {
                if (string.IsNullOrWhiteSpace(projectName))
                {
                    if (_typedRulesDictionary.TryGetValue(declaringType, out var rules))
                        return new VerifyRulePackage(declaringType, rules);
                }
                else
                {
                    if (_namedRulesDictionary.TryGetValue((declaringType, projectName), out var rules))
                        return new VerifyRulePackage(declaringType, rules);
                }

                return VerifyRulePackage.Empty;
            }
        }

        #endregion
    }
}