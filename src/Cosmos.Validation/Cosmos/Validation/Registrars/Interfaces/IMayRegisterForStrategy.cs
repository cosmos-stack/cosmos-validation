using Cosmos.Validation.Strategies;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayRegisterForStrategy
    {
        IValidationRegistrar ForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IValidationRegistrar ForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IValidationRegistrar ForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IValidationRegistrar ForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IValidationRegistrar ForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IValidationRegistrar ForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IValidationRegistrar ForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
        IValidationRegistrar ForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
    }

    public interface IMayContinueRegisterForStrategy
    {
        IFluentValidationRegistrar AndForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IFluentValidationRegistrar AndForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IFluentValidationRegistrar AndForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IFluentValidationRegistrar AndForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
        IFluentValidationRegistrar AndForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
    }
}