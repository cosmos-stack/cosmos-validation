using System;
using Cosmos.Validation.Registrars;
using Cosmos.Validation.Strategies;

namespace Cosmos.Validation
{
    public sealed class ValidationHandlerBuilder
    {
        private IValidationRegistrar Registrar { get; set; }

        private ValidationHandlerBuilder()
        {
            Registrar = ValidationRegistrar.DefaultRegistrar;
        }

        #region Strategy

        public void ForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new()
        {
            Registrar.ForStrategy<TStrategy>(mode).TakeEffect();
        }

        public void ForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new()
        {
            Registrar.ForStrategy<TStrategy, T>(mode).TakeEffect();
        }

        public void ForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            Registrar.ForStrategy(strategy, mode).TakeEffect();
        }

        public void ForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            Registrar.ForStrategy(strategy, mode).TakeEffect();
        }

        public void ForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new()
        {
            Registrar.ForStrategy<TStrategy>(name, mode).TakeEffect();
        }

        public void ForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new()
        {
            Registrar.ForStrategy<TStrategy, T>(name, mode).TakeEffect();
        }

        public void ForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            Registrar.ForStrategy(strategy, name, mode).TakeEffect();
        }

        public void ForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite)
        {
            Registrar.ForStrategy(strategy, name, mode).TakeEffect();
        }

        #endregion

        #region Type

        public void ForType(Type type, Action<IFluentValidationRegistrar> registerAct)
        {
            var registrar = Registrar.ForType(type);
            registerAct?.Invoke(registrar);
            registrar.TakeEffect();
        }

        public void ForType(Type type, string name, Action<IFluentValidationRegistrar> registerAct)
        {
            var registrar = Registrar.ForType(type, name);
            registerAct?.Invoke(registrar);
            registrar.TakeEffect();
        }

        public void ForType<T>(Action<IFluentValidationRegistrar<T>> registerAct)
        {
            var registrar = Registrar.ForType<T>();
            registerAct?.Invoke(registrar);
            registrar.TakeEffect();
        }

        public void ForType<T>(string name, Action<IFluentValidationRegistrar<T>> registerAct)
        {
            var registrar = Registrar.ForType<T>(name);
            registerAct?.Invoke(registrar);
            registrar.TakeEffect();
        }

        #endregion

        public ValidationHandler Build() => Registrar.TempBuild();

        public static ValidationHandlerBuilder Create() => new();
    }
}