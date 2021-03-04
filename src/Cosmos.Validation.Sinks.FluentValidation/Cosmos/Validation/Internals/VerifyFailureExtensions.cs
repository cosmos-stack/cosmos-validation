﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Internals.Extensions;
using FluentValidation.Results;

namespace Cosmos.Validation.Internals
{
    internal static class VerifyFailureExtensions
    {
        public static IEnumerable<VerifyFailure> ConvertToVerifyFailures(this IList<ValidationFailure> originalFailures, Type typeOfValidator)
        {
            var targetFailures = new List<VerifyFailure>();
            var failureHolding = new Dictionary<string, VerifyFailure>();

            foreach (var originalFailure in originalFailures)
            {
                if (!failureHolding.TryGetValue(originalFailure.PropertyName, out var targetFailure))
                {
                    targetFailure = new VerifyFailure(originalFailure.PropertyName, $"There are multiple errors in this Member '{originalFailure.PropertyName}'.", originalFailure.AttemptedValue);
                    failureHolding[originalFailure.PropertyName] = targetFailure;
                }

                var error = new VerifyError
                {
                    ErrorMessage = originalFailure.ErrorMessage,
                    ViaValidatorType = ValidatorType.Custom,
                    ValidatorName = $"FluentValidation {typeOfValidator.GetFriendlyName()} Validator"
                };

                targetFailure.Details.Add(error);
            }

            return failureHolding.Select(x => x.Value);
        }
    }
}