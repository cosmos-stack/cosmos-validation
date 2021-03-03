using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy.Parameters;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// Valid date
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Class)]
    public class ValidEmailValueAttribute : ValidationParameterAttribute,
        IQuietVerifiableAnnotation, IStrongVerifiableAnnotation<string>, IObjectContextVerifiableAnnotation
    {
        public bool AllowTopLevelDomains { get; set; }

        public bool AllowInternational { get; set; }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            var validator = EmailValidator.Instance;
            var parameter = context.Parameter;
            var result = parameter.Type == TypeClass.StringClazz
                ? validator.Verify((string) parameter.Value, ParamName, AllowTopLevelDomains, AllowInternational) // valid for field or property
                : validator.Verify(parameter.Type, parameter.Value); // valid for class

            if (!result.IsValid)
                result.Raise(ErrorMessage);

            return next(context);
        }

        #region StrongVerify

        /// <summary>
        /// Strong Verify
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult StrongVerify(string instance)
        {
            return EmailValidator.Instance.Verify(instance, ParamName, AllowTopLevelDomains, AllowInternational);
        }

        /// <summary>
        /// Strong Verify
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult StrongVerify<T>(T instance)
        {
            if (instance is null)
                return VerifyResult.NullReference;

            if (instance is string str)
                return StrongVerify(str);

            if (instance is VerifiableObjectContext ctx1)
                return EmailValidator.Instance.VerifyViaContext(ctx1);

            if (instance is VerifiableMemberContext ctx2)
                return EmailValidator.Instance.VerifyViaContext(ctx2.ConvertToObjectContext());

            return EmailValidator.Instance.Verify(typeof(T), instance);
        }

        /// <summary>
        /// Strong Verify
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult StrongVerify(Type type, object instance)
        {
            if (instance is null)
                return VerifyResult.NullReference;

            if (instance is string str)
                return StrongVerify(str);

            if (instance is VerifiableObjectContext ctx1)
                return EmailValidator.Instance.VerifyViaContext(ctx1);

            if (instance is VerifiableMemberContext ctx2)
                return EmailValidator.Instance.VerifyViaContext(ctx2.ConvertToObjectContext());

            return EmailValidator.Instance.Verify(type, instance);
        }

        /// <summary>
        /// ObjectContext Verify
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public VerifyResult StrongVerify(VerifiableObjectContext context)
        {
            return EmailValidator.Instance.VerifyViaContext(context);
        }

        #endregion

        #region QuietVerify

        /// <summary>
        /// Quiet Verify
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool QuietVerify<T>(T instance)
        {
            return StrongVerify(instance).IsValid;
        }

        /// <summary>
        /// Quiet Verify
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool QuietVerify(Type type, object instance)
        {
            return StrongVerify(type, instance).IsValid;
        }

        #endregion

        public override string Name => "ValidEmailValueAnnotation";
    }
}