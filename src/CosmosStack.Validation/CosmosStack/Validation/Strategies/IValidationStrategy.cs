using System;

namespace CosmosStack.Validation.Strategies
{
    /// <summary>
    /// An interface for Validation Strategy <br />
    /// 验证策略接口
    /// </summary>
    public interface IValidationStrategy
    {
        Type SourceType { get; }
    }

    /// <summary>
    /// An interface for Validation Strategy <br />
    /// 验证策略接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValidationStrategy<T>
    {
        Type SourceType { get; }
    }
}