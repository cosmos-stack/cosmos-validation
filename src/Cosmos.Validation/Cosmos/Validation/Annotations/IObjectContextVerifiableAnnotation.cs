using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// A ObjectContext Verify Interface
    /// </summary>
    public interface IObjectContextVerifiableAnnotation : IVerifiable
    {
        /// <summary>
        /// ObjectContext Verify
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        VerifyResult StrongVerify(ObjectContext context);
    }
}