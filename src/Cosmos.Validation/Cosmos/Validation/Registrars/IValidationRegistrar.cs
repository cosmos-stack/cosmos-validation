using System;
using Cosmos.Validation.Strategies;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Registrars
{
    public interface IValidationRegistrar
    {
        IValidationRegistrar ForStrategy<TStrategy>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IValidationRegistrar ForStrategy<TStrategy, T>(StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IValidationRegistrar ForStrategy(IValidationStrategy strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IValidationRegistrar ForStrategy<T>(IValidationStrategy<T> strategy, StrategyMode mode = StrategyMode.OverallOverwrite);
        IValidationRegistrar ForStrategy<TStrategy>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy, new();
        IValidationRegistrar ForStrategy<TStrategy, T>(string name, StrategyMode mode = StrategyMode.OverallOverwrite) where TStrategy : class, IValidationStrategy<T>, new();
        IValidationRegistrar ForStrategy(IValidationStrategy strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
        IValidationRegistrar ForStrategy<T>(IValidationStrategy<T> strategy, string name, StrategyMode mode = StrategyMode.OverallOverwrite);
        IValidationRegistrar ForValidator<TValidator>() where TValidator : CustomValidator, new();
        IValidationRegistrar ForValidator<TValidator, T>() where TValidator : CustomValidator<T>, new();
        IValidationRegistrar ForValidator(CustomValidator validator);
        IValidationRegistrar ForValidator<T>(CustomValidator<T> validator);
        IFluentValidationRegistrar ForType(Type type);
        IFluentValidationRegistrar ForType(Type type, string name);
        IFluentValidationRegistrar<T> ForType<T>();
        IFluentValidationRegistrar<T> ForType<T>(string name);
        void Build();
        ValidationHandler TempBuild();
    }
}