using System;
using CosmosStack.Validation.Registrars;
using CosmosStack.Validation.Strategies;

namespace CosmosStack.Validation
{
    /// <summary>
    /// Validation Handler Builder <br />
    /// 验证处理器构造器程序
    /// </summary>
    public sealed class ValidationHandlerBuilder
    {
        private IValidationRegistrar Registrar { get; set; }

        private ValidationHandlerBuilder()
        {
            Registrar = ValidationRegistrar.DefaultRegistrar;
        }

        #region Strategy

        /// <summary>
        /// Use a strategy <br />
        /// 使用一条策略
        /// </summary>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        public void ForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new()
        {
            Registrar.ForStrategy<TStrategy>(mode).TakeEffect();
        }

        /// <summary>
        /// Use a strategy <br />
        /// 使用一条策略
        /// </summary>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <typeparam name="T"></typeparam>
        public void ForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new()
        {
            Registrar.ForStrategy<TStrategy, T>(mode).TakeEffect();
        }

        /// <summary>
        /// Use a strategy <br />
        /// 使用一条策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="mode"></param>
        public void ForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            Registrar.ForStrategy(strategy, mode).TakeEffect();
        }

        /// <summary>
        /// Use a strategy <br />
        /// 使用一条策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="mode"></param>
        /// <typeparam name="T"></typeparam>
        public void ForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            Registrar.ForStrategy(strategy, mode).TakeEffect();
        }

        /// <summary>
        /// Use a strategy <br />
        /// 使用一条策略
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        public void ForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new()
        {
            Registrar.ForStrategy<TStrategy>(name, mode).TakeEffect();
        }

        /// <summary>
        /// Use a strategy <br />
        /// 使用一条策略
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <typeparam name="T"></typeparam>
        public void ForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new()
        {
            Registrar.ForStrategy<TStrategy, T>(name, mode).TakeEffect();
        }

        /// <summary>
        /// Use a strategy <br />
        /// 使用一条策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        public void ForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            Registrar.ForStrategy(strategy, name, mode).TakeEffect();
        }

        /// <summary>
        /// Use a strategy <br />
        /// 使用一条策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <typeparam name="T"></typeparam>
        public void ForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            Registrar.ForStrategy(strategy, name, mode).TakeEffect();
        }

        #endregion

        #region Type

        /// <summary>
        /// Use rules for specified types <br />
        /// 对指定的类型使用规则
        /// </summary>
        /// <param name="type"></param>
        /// <param name="registerAct"></param>
        public void ForType(Type type, Action<IFluentValidationRegistrar> registerAct)
        {
            var registrar = Registrar.ForType(type);
            registerAct?.Invoke(registrar);
            registrar.TakeEffect();
        }

        /// <summary>
        /// Use rules for specified types <br />
        /// 对指定的类型使用规则
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="registerAct"></param>
        public void ForType(Type type, string name, Action<IFluentValidationRegistrar> registerAct)
        {
            var registrar = Registrar.ForType(type, name);
            registerAct?.Invoke(registrar);
            registrar.TakeEffect();
        }

        /// <summary>
        /// Use rules for specified types <br />
        /// 对指定的类型使用规则
        /// </summary>
        /// <param name="registerAct"></param>
        /// <typeparam name="T"></typeparam>
        public void ForType<T>(Action<IFluentValidationRegistrar<T>> registerAct)
        {
            var registrar = Registrar.ForType<T>();
            registerAct?.Invoke(registrar);
            registrar.TakeEffect();
        }

        /// <summary>
        /// Use rules for specified types <br />
        /// 对指定的类型使用规则
        /// </summary>
        /// <param name="name"></param>
        /// <param name="registerAct"></param>
        /// <typeparam name="T"></typeparam>
        public void ForType<T>(string name, Action<IFluentValidationRegistrar<T>> registerAct)
        {
            var registrar = Registrar.ForType<T>(name);
            registerAct?.Invoke(registrar);
            registrar.TakeEffect();
        }

        #endregion

        #region RulePackage

        /// <summary>
        /// Use a rule package <br />
        /// 使用一个验证规则包
        /// </summary>
        /// <param name="package"></param>
        /// <param name="mode"></param>
        public void ForRulePackage(VerifyRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Overwrite)
        {
            Registrar.ForRulePackage(package, mode).TakeEffect();
        }

        /// <summary>
        /// Use a rule package <br />
        /// 使用一个验证规则包
        /// </summary>
        /// <param name="package"></param>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        public void ForRulePackage(VerifyRulePackage package, string name, VerifyRuleMode mode = VerifyRuleMode.Overwrite)
        {
            Registrar.ForRulePackage(package, name, mode).TakeEffect();
        }

        #endregion

        #region MemberRulePackage

        /// <summary>
        /// Use a member rule package <br />
        /// 使用一条成员验证规则包
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="package"></param>
        /// <param name="mode"></param>
        public void ForMemberRulePackage(string memberName, VerifyMemberRulePackage package, VerifyRuleMode mode = VerifyRuleMode.Overwrite)
        {
            Registrar.ForType(package.DeclaringType).ForMember(memberName, mode).WithMemberRulePackage(package, mode).TakeEffect();
        }

        /// <summary>
        /// Use a member rule package <br />
        /// 使用一条成员验证规则包
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="package"></param>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        public void ForMemberRulePackage(string memberName, VerifyMemberRulePackage package, string name, VerifyRuleMode mode = VerifyRuleMode.Overwrite)
        {
            Registrar.ForType(package.DeclaringType, name).ForMember(memberName, mode).WithMemberRulePackage(package, mode).TakeEffect();
        }

        #endregion

        /// <summary>
        /// Build  <br />
        /// 构建
        /// </summary>
        /// <returns></returns>
        public ValidationHandler Build() => Registrar.TempBuild();

        /// <summary>
        /// Create <br />
        /// 创建
        /// </summary>
        /// <returns></returns>
        public static ValidationHandlerBuilder Create() => new();
    }
}