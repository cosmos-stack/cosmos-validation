using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// Validation Parameter attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public abstract class ValidationParameterAttribute : ParameterInterceptorAttribute, IFlagAnnotation
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public virtual string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets name of parameter
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// Unexpected type returns success.
        /// </summary>
        public bool IgnoreUnexpectedType { get; set; } = true;
        
        /// <summary>
        /// Null object returns false.
        /// </summary>
        public virtual bool IgnoreNullObject { get; set; }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentInvalidException"></exception>
        public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            var valid = IsValid(context.Parameter);
            ValidationExceptionHelper.WrapAndRaise<ArgumentInvalidException>(valid, ErrorMessage, context.Parameter.Name);
            return next(context);
        }

        protected virtual bool IsValid(Parameter parameter)
        {
            return IsValidImpl(TypeConv.GetNonNullableType(parameter.Type), parameter.Name, () => parameter.Value);
        }

        internal virtual bool IsValid(VerifiableMemberContext context)
        {
            return IsValidImpl(context.MemberType, context.MemberName, context.GetValue);
        }

        /// <summary>
        /// Invoke internal impl
        /// </summary>
        /// <param name="memberType"></param>
        /// <param name="memberName"></param>
        /// <param name="memberValueGetter"></param>
        /// <returns></returns>
        protected abstract bool IsValidImpl(Type memberType, string memberName, Func<object> memberValueGetter);

        protected (bool Valid, Type ParameterType, string Message) Success(Type parameterType) => (true, parameterType, string.Empty);

        protected (bool Valid, Type ParameterType, string Message) Failure(Type parameterType, string message) => (false, parameterType, message);
    }
}