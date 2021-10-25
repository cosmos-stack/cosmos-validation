using System;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation
{
    public interface IValidationService
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
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IValidator RequiredResolve(Type type);

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IValidator RequiredResolve(Type type, string name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidator<T> RequiredResolve<T>();

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidator<T> RequiredResolve<T>(string name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Func<string, IValidator> ResolveScoped(Type type);

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Func<string, IValidator> ResolveScoped(Type type, string name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Func<string, IValidator<T>> ResolveScoped<T>();

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Func<string, IValidator<T>> ResolveScoped<T>(string name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Func<string, IValidator> RequiredResolveScoped(Type type);

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Func<string, IValidator> RequiredResolveScoped(Type type, string name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Func<string, IValidator<T>> RequiredResolveScoped<T>();

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Func<string, IValidator<T>> RequiredResolveScoped<T>(string name);
    }
}