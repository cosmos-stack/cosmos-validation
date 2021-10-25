using System;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation.Internals
{
    internal class InternalValidationService : IValidationService
    {
        private T Required<T>(T t) where T : class
        {
            if (t is null)
                throw new ArgumentException("The value should not be null.");
            return t;
        }

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IValidator Resolve(Type type) => ValidationMe.Resolve(type);

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IValidator Resolve(Type type, string name) => ValidationMe.Resolve(type, name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IValidator<T> Resolve<T>() => ValidationMe.Resolve<T>();

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IValidator<T> Resolve<T>(string name) => ValidationMe.Resolve<T>(name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IValidator RequiredResolve(Type type) => Required(ValidationMe.Resolve(type));

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IValidator RequiredResolve(Type type, string name) => Required(ValidationMe.Resolve(type, name));

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IValidator<T> RequiredResolve<T>() => Required(ValidationMe.Resolve<T>());

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IValidator<T> RequiredResolve<T>(string name) => Required(ValidationMe.Resolve<T>(name));

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Func<string, IValidator> ResolveScoped(Type type) => scopeName => ValidationMe.Use(scopeName).Resolve(type);

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Func<string, IValidator> ResolveScoped(Type type, string name) => scopeName => ValidationMe.Use(scopeName).Resolve(type, name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Func<string, IValidator<T>> ResolveScoped<T>() => scopeName => ValidationMe.Use(scopeName).Resolve<T>();

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Func<string, IValidator<T>> ResolveScoped<T>(string name) => scopeName => ValidationMe.Use(scopeName).Resolve<T>(name);

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Func<string, IValidator> RequiredResolveScoped(Type type) => scopeName => Required(ValidationMe.Use(scopeName).Resolve(type));

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Func<string, IValidator> RequiredResolveScoped(Type type, string name) => scopeName => Required(ValidationMe.Use(scopeName).Resolve(type, name));

        /// <summary>
        /// Resolve a validator based on a given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Func<string, IValidator<T>> RequiredResolveScoped<T>() => scopeName => Required(ValidationMe.Use(scopeName).Resolve<T>());

        /// <summary>
        /// Resolve a validator based on a given type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Func<string, IValidator<T>> RequiredResolveScoped<T>(string name) => scopeName => Required(ValidationMe.Use(scopeName).Resolve<T>(name));
    }
}