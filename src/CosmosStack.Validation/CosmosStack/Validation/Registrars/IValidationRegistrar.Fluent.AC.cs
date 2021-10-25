// ReSharper disable InconsistentNaming

using CosmosStack.Validation.Registrars.Interfaces;

namespace CosmosStack.Validation.Registrars
{
    /// <summary>
    /// Validation Register Interface with Predicate <br />
    /// 带条件的验证注册器接口
    /// </summary>
    public interface IPredicateValidationRegistrar : IValueFluentValidationRegistrar, IMayUseActivationConditions { }

    /// <summary>
    /// Validation Register Interface with Predicate <br />
    /// 带条件的验证注册器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPredicateValidationRegistrar<T> : IValueFluentValidationRegistrar<T>, IMayUseActivationConditions<T> { }

    /// <summary>
    /// Validation Register Interface with Predicate <br />
    /// 带条件的验证注册器接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TVal"></typeparam>
    public interface IPredicateValidationRegistrar<T, TVal> : IValueFluentValidationRegistrar<T, TVal>, IMayUseActivationConditions<T, TVal> { }
}