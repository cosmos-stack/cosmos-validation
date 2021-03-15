using System;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation
{
    /// <summary>
    /// An interface of validation provider
    /// </summary>
    public interface IValidationProvider
    {
        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IValidator Resolve(Type type);
        
        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IValidator Resolve(Type type, string name);
        
        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidator<T> Resolve<T>();
        
        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidator<T> Resolve<T>(string name);

        /// <summary>
        /// Override the configuration of the validator.
        /// </summary>
        /// <param name="options"></param>
        void UpdateOptions(ValidationOptions options);
        
        /// <summary>
        /// Update the configuration of the validator.
        /// </summary>
        /// <param name="optionAct"></param>
        void UpdateOptions(Action<ValidationOptions> optionAct);
    }
}