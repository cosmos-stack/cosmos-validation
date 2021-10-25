﻿using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using CosmosStack.Reflection;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Annotations
{
    /// <summary>
    /// Validation Parameter attribute <br />
    /// 验证参数特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public abstract class VerifiableParamsAttribute : ParameterInterceptorAttribute, IFlagAnnotation
    {
        /// <summary>
        /// Name of this Attribute/Annotation <br />
        /// 注解名
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public virtual string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets name of parameter <br />
        /// 参数名
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// Unexpected type returns success. <br />
        /// 如果是非期待的类型，是否返回异常。默认为 True。
        /// </summary>
        public bool IgnoreUnexpectedType { get; set; } = true;

        /// <summary>
        /// Null object returns false. <br />
        /// 忽略 Null
        /// </summary>
        public virtual bool IgnoreNullObject { get; set; }

        /// <summary>
        /// Invoke <br />
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentInvalidException"></exception>
        public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            var valid = IsValid(context.Parameter);
            ValidationExceptionHelper.WrapAndRaise<ArgumentInvalidException>(valid, ErrorMessage, GetParamName(context.Parameter));
            return next(context);
        }

        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected virtual bool IsValid(Parameter parameter)
        {
            return IsValidImpl(TypeConv.GetNonNullableType(parameter.Type), GetParamName(parameter), () => parameter.Value);
        }

        /// <summary>
        /// IsValid
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal virtual bool IsValid(VerifiableMemberContext context)
        {
            return IsValidImpl(context.MemberType, GetParamName(context), context.GetValue);
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

        private string GetParamName(Parameter parameter)
        {
            if (string.IsNullOrWhiteSpace(ParamName))
                return parameter.Name;
            return ParamName;
        }

        private string GetParamName(VerifiableMemberContext context)
        {
            if (string.IsNullOrWhiteSpace(ParamName))
                return context.MemberName;
            return ParamName;
        }
    }
}