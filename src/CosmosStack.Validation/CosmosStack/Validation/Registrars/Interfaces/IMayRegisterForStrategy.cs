using CosmosStack.Validation.Strategies;

namespace CosmosStack.Validation.Registrars.Interfaces
{
    /// <summary>
    /// May register for strategy interface <br />
    /// 可为策略注册
    /// </summary>
    public interface IMayRegisterForStrategy
    {
        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <returns></returns>
        IValidationRegistrar ForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidationRegistrar ForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValidationRegistrar ForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite);

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="mode"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidationRegistrar ForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite);

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <returns></returns>
        IValidationRegistrar ForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidationRegistrar ForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IValidationRegistrar ForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidationRegistrar ForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
    }

    /// <summary>
    /// May register for strategy continue interface <br />
    /// 可继续为策略注册
    /// </summary>
    public interface IMayContinueRegisterForStrategy
    {
        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar AndForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite);

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="mode"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite);

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar AndForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <typeparam name="TStrategy"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar AndForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);

        /// <summary>
        /// Register for strategy <br />
        /// 注册策略
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="name"></param>
        /// <param name="mode"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
    }
}