using System;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// A Strong Verify Interface
    /// </summary>
    public interface IStrongVerifiableAnnotation : IVerifiable
    {
        /// <summary>
        /// Strong Verify
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult StrongVerify<T>(T instance);

        /// <summary>
        /// Strong Verify
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult StrongVerify(Type type, object instance);
    }

    /// <summary>
    /// A Strong Verify Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IStrongVerifiableAnnotation<in T> : IStrongVerifiableAnnotation
    {
        /// <summary>
        /// Strong Verify
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        VerifyResult StrongVerify(T instance);
    }
}