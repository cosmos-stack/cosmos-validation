using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// A ObjectContext Verify Interface <br />
    /// 带对象上下文的可验证接口
    /// </summary>
    public interface IObjectContextVerifiableAnnotation : IVerifiable
    {
        /// <summary>
        /// ObjectContext Verify <br />
        /// 验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        VerifyResult StrongVerify(VerifiableObjectContext context);
    }
}