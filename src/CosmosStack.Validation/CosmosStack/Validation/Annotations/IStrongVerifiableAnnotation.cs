using System;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// A Strong Verify Interface <br />
    /// 强验证接口
    /// </summary>
    public interface IStrongVerifiableAnnotation : IVerifiable
    {
        /// <summary>
        /// Strong Verify <br />
        /// 强验证
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult StrongVerify<T>(T instance);

        /// <summary>
        /// Strong Verify <br />
        /// 强验证
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult StrongVerify(Type type, object instance);
    }

    /// <summary>
    /// A Strong Verify Interface <br />
    /// 强验证接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStrongVerifiableAnnotation<in T> : IStrongVerifiableAnnotation
    {
        /// <summary>
        /// Strong Verify <br />
        /// 强验证
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult StrongVerify(T instance);
    }
}