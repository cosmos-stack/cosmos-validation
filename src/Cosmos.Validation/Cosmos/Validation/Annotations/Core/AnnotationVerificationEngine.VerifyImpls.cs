using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;
using Cosmos.Validation.Validators;

namespace Cosmos.Validation.Annotations.Core
{
    internal static partial class AnnotationVerificationEngine
    {
        private static class VerifyImpls
        {
            public static void ValidEmailValue(VerifiableMemberContext context, ValidEmailValueAttribute attr, List<VerifyError> errors)
            {
                VerifyResult result;

                if (context.Is(TypeClass.StringClazz).Valid && context.Value is string emailStr)
                {
                    result = EmailValidator.Instance.Verify(
                        emailStr,
                        context.MemberName,
                        attr.AllowTopLevelDomains,
                        attr.AllowInternational);
                }
                else
                {
                    result = attr.StrongVerify(context.ConvertToObjectContext());
                }

                result.IsValid.IfFalseThenInvoke(() => CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors));
            }
        }
    }
}