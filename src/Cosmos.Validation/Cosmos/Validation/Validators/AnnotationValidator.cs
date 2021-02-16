﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cosmos.Validation.Annotations;
using Cosmos.Validation.Annotations.Core;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    /// <summary>
    /// Annotation Validator
    /// </summary>
    public class AnnotationValidator : IValidator
    {
        private readonly IValidationObjectResolver _objectResolver;

        private AnnotationValidator() : this(new BuildInObjectResolver()) { }

        private AnnotationValidator(IValidationObjectResolver objectResolver)
        {
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
        }

        public static AnnotationValidator Instance { get; } = new();

        public static AnnotationValidator GetInstance() => Instance;

        public static AnnotationValidator GetInstance(IValidationObjectResolver objectResolver) => new(objectResolver);

        public string Name => "Annotation Validator";

        public bool IsAnonymous => false;

        public VerifyResult Verify(Type type, object instance)
        {
            if (instance is null)
                return BuildInVerifyResults.NullObjectReference;
            if (instance is ObjectContext context)
                return Verify(context);
            if (instance is ObjectValueContext valueContext)
                return Verify(valueContext.ToObjectContext());
            return Verify(_objectResolver.Resolve(type, instance));
        }

        public VerifyResult Verify<T>(T instance)
        {
            if (instance is null)
                return BuildInVerifyResults.NullObjectReference;
            if (instance is ObjectContext context)
                return Verify(context);
            if (instance is ObjectValueContext valueContext)
                return Verify(valueContext.ToObjectContext());
            return Verify(_objectResolver.Resolve(instance));
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
                return BuildInVerifyResults.NullObjectReference;
            if (instance is ObjectContext context)
                return Verify(context);
            if (instance is ObjectValueContext valueContext)
                return Verify(valueContext.ToObjectContext());
            return Verify(_objectResolver.Resolve(type, instance));
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
                return BuildInVerifyResults.NullObjectReference;
            if (instance is ObjectContext context)
                return Verify(context);
            if (instance is ObjectValueContext valueContext)
                return Verify(valueContext.ToObjectContext());
            return Verify(_objectResolver.Resolve(instance));
        }

        public VerifyResult Verify(ObjectContext context)
        {
            if (context is null)
                return VerifyResult.NullReference;

            if (!context.IncludeAnnotations)
                return VerifyResult.Success;

            var slaveResults = new List<VerifyResult>();

            foreach (var valueWithAnnotation in context.GetValuesWithAttribute())
            {
                VerifyViaFlagAnnotations(valueWithAnnotation, slaveResults);
                VerifyViaVerifiableAnnotations(valueWithAnnotation, slaveResults);
            }

            return VerifyResult.MakeTogether(slaveResults);
        }

        private static void VerifyViaFlagAnnotations(ObjectValueContext context, List<VerifyResult> results)
        {
            var annotations = context.GetFlagAnnotations(true).ToList();

            if (annotations.Any())
            {
                if (!AnnotationVerificationEngine.Verify(context, annotations, out var failure))
                    results.Add(new VerifyResult(failure));
            }
        }

        private static void VerifyViaVerifiableAnnotations(ObjectValueContext context, List<VerifyResult> results)
        {
            var annotations = context.GetVerifiableAnnotations(true).ToList();

            var errors = new List<VerifyError>();

            if (annotations.Any())
            {
                foreach (var annotation in annotations)
                {
                    // 先检查是否为静默验证特性
                    if (annotation is IQuietVerifiableAnnotation quietVerifiableAnnotation)
                    {
                        if (!quietVerifiableAnnotation.QuietVerify(context.MemberType, context.Value))
                        {
                            var error = new VerifyError
                            {
                                ErrorMessage = quietVerifiableAnnotation.ErrorMessage,
                                ValidatorName = quietVerifiableAnnotation.GetType().FullName,
                                ViaValidatorType = ValidatorType.BuildIn
                            };

                            errors.Add(error);
                        }
                    }
                    // 如果不是，则检查是否为强验证特性
                    else if (annotation is IStrongVerifiableAnnotation strongVerifiableAnnotation)
                    {
                        var result = strongVerifiableAnnotation.StrongVerify(context.MemberType, context.Value);

                        if (!result.IsValid)
                        {
                            var localErrors = result.Errors[0].Details.Select(x => new VerifyError
                            {
                                ErrorMessage = x.ErrorMessage,
                                ValidatorName = strongVerifiableAnnotation.GetType().FullName,
                                ViaValidatorType = ValidatorType.BuildIn
                            });

                            errors.AddRange(localErrors);
                        }
                    }
                    // 如果不是，则检查是否为对象上下文验证特性
                    else if (annotation is IObjectContextVerifiableAnnotation contextVerifiableAnnotation)
                    {
                        var result = contextVerifiableAnnotation.StrongVerify(context.ToObjectContext());

                        if (!result.IsValid)
                        {
                            var localErrors = result.Errors[0].Details.Select(x => new VerifyError
                            {
                                ErrorMessage = x.ErrorMessage,
                                ValidatorName = contextVerifiableAnnotation.GetType().FullName,
                                ViaValidatorType = ValidatorType.BuildIn
                            });

                            errors.AddRange(localErrors);
                        }
                    }
                }
            }

            if (errors.Any())
            {
                var failure = new VerifyFailure(context.MemberName, $"There are multiple errors in this Member '{context.MemberName}'.", context.Value);

                failure.Details.AddRange(errors);

                results.Add(new(failure));
            }
        }
    }
}