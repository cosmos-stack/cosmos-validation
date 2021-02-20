using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Internals.Rules;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Projects;
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

        public string Name => _nameOfProvider;

        public InternalValidationRegistrar(IValidationProvider targetProvider, RegisterMode mode, string nameOfProvider)
        {
            _targetProvider = targetProvider;
            _registerMode = mode;
            _nameOfProvider = nameOfProvider;
        }

        #region RegisterStrategy

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

        #region RegisterValidator

        public IValidationRegistrar ForValidator<TValidator>() where TValidator : CustomValidator, new()
        {
            InnerPtr.RegisterValidator<TValidator>();
            return this;
        }

        public IValidationRegistrar ForValidator<TValidator, T>() where TValidator : CustomValidator<T>, new()
        {
            InnerPtr.RegisterValidator<TValidator, T>();
            return this;
        }

        public IValidationRegistrar ForValidator(CustomValidator validator)
        {
            InnerPtr.RegisterValidator(validator);
            return this;
        }

        public IValidationRegistrar ForValidator<T>(CustomValidator<T> validator)
        {
            InnerPtr.RegisterValidator(validator);
            return this;
        }

        #endregion

        #region RegisterType

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
            return new(GetProjects(), InnerPtr.ExposeObjectResolver());
        }

        public ValidationHandler TempBuild(ValidationHandler handler)
        {
            return handler.Merge(GetProjects());
        }

        #endregion

        #region BuildForMember

        public void BuildForMember(Type type, string memberName, Func<IValueRuleBuilder, IValueRuleBuilder> func)
        {
            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var contract = ObjectContractManager.Resolve(type);
            var value = contract.GetValueContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{type.GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder(value);
            var rule = ((CorrectValueRuleBuilder) func(builder)).Build();
            AddOrUpdateValueRule(rule, type);
        }

        public void BuildForMember(Type type, string memberName, string name, Func<IValueRuleBuilder, IValueRuleBuilder> func)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                BuildForMember(type, memberName, func);
                return;
            }

            if (type is null)
                throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var contract = ObjectContractManager.Resolve(type);
            var value = contract.GetValueContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{type.GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder(value);
            var rule = ((CorrectValueRuleBuilder) func(builder)).Build();
            AddOrUpdateValueRule(rule, type, name);
        }

        public void BuildForMember(ObjectValueContract contract, Func<IValueRuleBuilder, IValueRuleBuilder> func)
        {
            if (contract is null)
                throw new ArgumentNullException(nameof(contract));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var builder = new CorrectValueRuleBuilder(contract);
            var rule = ((CorrectValueRuleBuilder) func(builder)).Build();
            AddOrUpdateValueRule(rule, contract.DeclaringType);
        }

        public void BuildForMember(ObjectValueContract contract, string name, Func<IValueRuleBuilder, IValueRuleBuilder> func)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                BuildForMember(contract, func);
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

        public void BuildForMember(CorrectValueRule rule)
        {
            if (rule is null)
                throw new ArgumentNullException(nameof(rule));
            AddOrUpdateValueRule(rule, rule.Contract.DeclaringType);
        }

        public void BuildForMember(string name, CorrectValueRule rule)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                BuildForMember(rule);
                return;
            }

            if (rule is null)
                throw new ArgumentNullException(nameof(rule));
            AddOrUpdateValueRule(rule, rule.Contract.DeclaringType, name);
        }

        public void BuildForMember<T>(string memberName, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func)
        {
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var contract = ObjectContractManager.Resolve<T>();
            var value = contract.GetValueContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{typeof(T).GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder<T>(value);
            var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
            AddOrUpdateValueRule(rule, typeof(T));
        }

        public void BuildForMember<T>(string memberName, string name, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                BuildForMember<T>(memberName, func);
                return;
            }

            if (string.IsNullOrWhiteSpace(memberName))
                throw new ArgumentNullException(nameof(memberName));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var contract = ObjectContractManager.Resolve<T>();
            var value = contract.GetValueContract(memberName);
            if (value is null)
                throw new ArgumentException($"Member name '{memberName}' is not a valid Member of type '{typeof(T).GetFriendlyName()}'");
            var builder = new CorrectValueRuleBuilder<T>(value);
            var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
            AddOrUpdateValueRule(rule, typeof(T), name);
        }

        public void BuildForMember<T>(ObjectValueContract contract, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func)
        {
            if (contract is null)
                throw new ArgumentNullException(nameof(contract));
            if (func is null)
                throw new ArgumentNullException(nameof(func));
            var builder = new CorrectValueRuleBuilder<T>(contract);
            var rule = ((CorrectValueRuleBuilder<T>) func(builder)).Build();
            AddOrUpdateValueRule(rule, contract.DeclaringType);
        }

        public void BuildForMember<T>(ObjectValueContract contract, string name, Func<IValueRuleBuilder<T>, IValueRuleBuilder<T>> func)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                BuildForMember(contract, func);
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

        public void BuildForMember<T>(CorrectValueRule rule)
        {
            if (rule is null)
                throw new ArgumentNullException(nameof(rule));
            AddOrUpdateValueRule(rule, rule.Contract.DeclaringType);
        }

        public void BuildForMember<T>(string name, CorrectValueRule rule)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                BuildForMember<T>(rule);
                return;
            }

            if (rule is null)
                throw new ArgumentNullException(nameof(rule));
            AddOrUpdateValueRule(rule, rule.Contract.DeclaringType, name);
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
            foreach (var project in ProjectFactory.CreateTypeProject(_typedRulesDictionary, InnerPtr.ExposeCustomValidatorManager()))
                yield return project;
            foreach (var project in ProjectFactory.CreateNamedTypeProject(_namedRulesDictionary, InnerPtr.ExposeCustomValidatorManager()))
                yield return project;
        }

        #endregion
    }
}