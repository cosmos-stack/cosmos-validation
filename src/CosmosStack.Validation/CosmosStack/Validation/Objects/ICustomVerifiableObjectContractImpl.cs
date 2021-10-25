using System.Collections.Generic;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// An interface of implementation for custom verifiable object contract <br />
    /// 实现自定义可验证对象约定的接口
    /// </summary>
    public interface ICustomVerifiableObjectContractImpl : IVerifiableObjectContract
    {
        Dictionary<string, VerifiableMemberContract> GetMemberContractMap();
    }
}