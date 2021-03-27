using System;
using Cosmos.Date;
using Cosmos.Reflection;
using Cosmos.Text;

namespace Cosmos.Validation.Annotations
{
    /// <summary>
    /// Valid date
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
    public class ValidDateValueAttribute : VerifiableParamsAttribute, IQuietVerifiableAnnotation
    {
        /// <summary>
        /// Name of this Attribute/Annotation
        /// </summary>
        public override string Name => "Valid-Date-Value Annotation";

        /// <summary>
        /// Gets or sets message<br />
        /// 消息
        /// </summary>
        public override string ErrorMessage { get; set; } = "The current value is not a valid time or date.";

        /// <summary>
        /// Invoke internal impl
        /// </summary>
        /// <param name="memberType"></param>
        /// <param name="memberName"></param>
        /// <param name="memberValueGetter"></param>
        /// <returns></returns>
        protected override bool IsValidImpl(Type memberType, string memberName, Func<object> memberValueGetter)
        {
            (bool Valid, Type ParameterType, string Message) valid;

            if (memberValueGetter() is null && !IgnoreNullObject)
                valid = Failure(memberType, ErrorMessage);
            else if (memberType.Is(TypeClass.DateTimeClazz).Valid)
                valid = memberValueGetter().Check<DateTime?>(v => v.CheckInvalidDate(memberName, ErrorMessage));
            else if (memberType.Is(TypeClass.StringClazz).Valid)
                valid = (memberValueGetter()._TryTo<string>().IsDateTime(), memberType, ErrorMessage);
            else
                valid = IgnoreUnexpectedType
                    ? Success(memberType)
                    : (memberValueGetter().ToString().IsDateTime(), memberType, ErrorMessage);

            return valid.Valid;
        }

        /// <summary>
        /// Quiet Verify
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool QuietVerify<T>(T instance)
        {
            return IsValidImpl(typeof(T), nameof(instance), () => instance);
        }

        /// <summary>
        /// Quiet Verify
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public bool QuietVerify(Type type, object instance)
        {
            return IsValidImpl(type, nameof(instance), () => instance);
        }
    }
}