using System;

namespace Cosmos.Validation.Validators
{
    /// <summary>
    /// Interface of Cosmos Validator
    /// </summary>
    public interface IValidator
    {
        string Name { get; }
        bool IsAnonymous { get; }
        VerifyResult Verify(Type type, object instance);
    }

    public interface IValidator<in T> : IValidator
    {
        VerifyResult Verify(T instance);
    }
}