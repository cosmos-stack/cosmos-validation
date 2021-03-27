using System;
using Cosmos.Reflection;
using Cosmos.Validation.Annotations.Core;
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
        /// Invoke internal impl
        /// </summary>
        /// <param name="memberType"></param>
        /// <param name="memberName"></param>
        /// <param name="memberValueGetter"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(Type memberType, string memberName, Func<object> memberValueGetter)
        {
            var validator = EmailValidator.Instance;

            var valid = memberType.Is(TypeClass.StringClazz).Valid && memberValueGetter() is string emailStr
                ? validator.Verify(emailStr, ParamName, AllowTopLevelDomains, AllowInternational) // valid for field or property
                : validator.Verify(memberType, memberValueGetter()); // valid for class

            return valid.IsValid;
        }

        #region StrongVerify

        /// <summary>
        /// Strong Verify
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult StrongVerify(string instance)
            => EmailValidator.Instance.Verify(instance, ParamName, AllowTopLevelDomains, AllowInternational);

        /// <summary>
        /// Strong Verify
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult StrongVerify<T>(T instance)
            => StrongVerify(typeof(T), instance);

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
            => EmailValidator.Instance.VerifyViaContext(context);

        #endregion

        #region QuietVerify

        /// <summary>
        /// Quiet Verify
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool QuietVerify<T>(T instance) => StrongVerify(instance).IsValid;

        /// <summary>
        /// Quiet Verify
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool QuietVerify(Type type, object instance) => StrongVerify(type, instance).IsValid;

        #endregion

        public override string Name => "ValidEmailValueAnnotation";
    }
}