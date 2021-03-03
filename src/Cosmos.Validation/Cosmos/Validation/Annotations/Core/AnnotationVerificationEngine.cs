using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Annotations.Core
{
    internal static partial class AnnotationVerificationEngine
    {
        public static bool Verify(VerifiableMemberContext context, IEnumerable<IFlagAnnotation> annotations, out VerifyFailure failure)
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            var errors = new List<VerifyError>();

            foreach (var annotation in annotations)
            {
                VerifyAnnotation(context, annotation, errors);
            }

            if (errors.Any())
            {
                failure = new VerifyFailure(context.MemberName, $"There are multiple errors in this Member '{context.MemberName}'.", context.Value);
                failure.Details.AddRange(errors);
                return false;
            }
            else
            {
                failure = null;
                return true;
            }
        }

        private static void VerifyAnnotation(VerifiableMemberContext context, IFlagAnnotation annotation, List<VerifyError> errors)
        {
            switch (annotation)
            {
                #region TypeJudge condition

                case MustNumericTypeAttribute attr:
                    VerifyImpls.MustNumericType(context, attr, errors);
                    break;

                case MustIntTypeAttribute attr:
                    VerifyImpls.MustIntType(context, attr, errors);
                    break;

                case MustLongTypeAttribute attr:
                    VerifyImpls.MustLongType(context, attr, errors);
                    break;

                case MustStringTypeAttribute attr:
                    VerifyImpls.MustStringType(context, attr, errors);
                    break;

                #endregion

                #region Date/Time conditions

                case ValidDateValueAttribute attr:
                    VerifyImpls.ValidDateValue(context, attr, errors);
                    break;

                case NotInTheFutureAttribute attr:
                    VerifyImpls.NotInTheFuture(context, attr, errors);
                    break;

                case NotInThePastAttribute attr:
                    VerifyImpls.NotInThePast(context, attr, errors);
                    break;

                #endregion

                #region Positive/Negative conditions

                case MustPositiveOrZeroAttribute attr:
                    VerifyImpls.NotNegative(context, attr, errors);
                    break;

                case MustPositiveAttribute attr:
                    VerifyImpls.NotNegativeOrZero(context, attr, errors);
                    break;

                case MustNegativeOrZeroAttribute attr:
                    VerifyImpls.NotPositive(context, attr, errors);
                    break;

                case MustNegativeAttribute attr:
                    VerifyImpls.NotPositiveOrZero(context, attr, errors);
                    break;

                case NotNegativeAttribute attr:
                    VerifyImpls.NotNegative(context, attr, errors);
                    break;

                case NotNegativeOrZeroAttribute attr:
                    VerifyImpls.NotNegativeOrZero(context, attr, errors);
                    break;

                case NotPositiveAttribute attr:
                    VerifyImpls.NotPositive(context, attr, errors);
                    break;

                case NotPositiveOrZeroAttribute attr:
                    VerifyImpls.NotPositiveOrZero(context, attr, errors);
                    break;

                #endregion

                #region Null/Space/Length cindition

                case NotNullAttribute attr:
                    VerifyImpls.NotNull(context, attr, errors);
                    break;

                case NotOutOfLengthAttribute attr:
                    VerifyImpls.NotOutOfLength(context, attr, errors);
                    break;

                case BetweenAttribute attr:
                    VerifyImpls.NotOutOfRange(context, attr, errors);
                    break;

                case NotOutOfRangeAttribute attr:
                    VerifyImpls.NotOutOfRange(context, attr, errors);
                    break;

                case NotBlankAttribute attr:
                    VerifyImpls.NotWhiteSpace(context, attr, errors);
                    break;

                case NotWhiteSpaceAttribute attr:
                    VerifyImpls.NotWhiteSpace(context, attr, errors);
                    break;

                #endregion

                #region Email annotation

                case ValidEmailValueAttribute attr:
                    VerifyImpls.ValidEmailValue(context, attr, errors);
                    break;

                #endregion

                #region CustomAnnotations

                // 对自定义（或第三方）验证注解的检查
                case CustomAnnotationAttribute attr:
                    var condition = attr.IsValidInternal(context);
                    if (!condition)
                        CreateAndUpdateErrors(attr.ErrorMessage, attr.Name, errors, ValidatorType.Custom);
                    break;

                #endregion
            }
        }

        private static void CreateAndUpdateErrors(string errorMessage, string validatorName, List<VerifyError> errors, ValidatorType type = ValidatorType.BuildIn)
        {
            var error = new VerifyError
            {
                ErrorMessage = errorMessage,
                ValidatorName = validatorName,
                ViaValidatorType = type
            };

            errors.Add(error);
        }
    }
}