﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cosmos.Reflection;
using Cosmos.Validation.Annotations;
using Cosmos.Validation.Annotations.Core;
using Cosmos.Validation.Internals;
using Cosmos.Validation.Internals.Extensions;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Validators
{
    /// <summary>
    /// Annotation Validator
    /// </summary>
    public class AnnotationValidator : IValidator, ICorrectValidator
    {
        private readonly IVerifiableObjectResolver _objectResolver;
        private readonly ValidationOptions _options;

        private AnnotationValidator() : this(new DefaultVerifiableObjectResolver(), new ValidationOptions()) { }

        private AnnotationValidator(IVerifiableObjectResolver objectResolver, ValidationOptions options)
        {
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public string Name => "Annotation Validator";

        public bool IsAnonymous => false;

        bool ICorrectValidator.IsTypeBinding => false;

        Type ICorrectValidator.SourceType => TypeClass.ObjectClazz;

        #region GetInstance

        public static AnnotationValidator Instance { get; } = new();

        public static AnnotationValidator GetInstance() => Instance;

        public static AnnotationValidator GetInstance(ValidationOptions options)
        {
            return new(new DefaultVerifiableObjectResolver(), options);
        }

        public static AnnotationValidator GetInstance(IVerifiableObjectResolver objectResolver, ValidationOptions options)
        {
            return new(objectResolver, options);
        }

        #endregion

        #region Verify

        public VerifyResult Verify(Type declaringType, object instance)
        {
            if (declaringType is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (instance is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (instance is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (instance is IDictionary<string, object> keyValueCollection)
                return VerifyMany(declaringType, keyValueCollection);
            return Verify(_objectResolver.Resolve(declaringType, instance));
        }

        public VerifyResult Verify<T>(T instance)
        {
            if (instance is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (instance is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (instance is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (instance is IDictionary<string, object>)
                return new VerifyResult(new VerifyFailure("KeyValueCollections", "Dictionary objects should specify specific types", instance));
            return Verify(_objectResolver.Resolve(instance));
        }

        public VerifyResult Verify(VerifiableObjectContext context)
        {
            if (context is null)
                return _options.ReturnNullReferenceOrSuccess();

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

        #endregion

        #region VerifyOne

        public VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            if (declaringType is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (memberValue is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (memberValue is IDictionary<string, object> keyValueCollection)
                return VerifyMany(declaringType, keyValueCollection);
            var memberContract = VerifiableObjectContractManager.Resolve(declaringType)?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOne(VerifiableMemberContext.Create(memberValue, memberContract));
        }

        public VerifyResult VerifyOne<T, TVal>(TVal memberValue, string memberName)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (memberValue is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (memberValue is IDictionary<string, object> keyValueCollection)
                return VerifyMany<T>(keyValueCollection);
            var memberContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOne(VerifiableMemberContext.Create(memberValue, memberContract));
        }

        public VerifyResult VerifyOne<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (memberValue is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (memberValue is IDictionary<string, object> keyValueCollection)
                return VerifyMany<T>(keyValueCollection);
            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var memberContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOne(VerifiableMemberContext.Create(memberValue, memberContract));
        }

        public VerifyResult VerifyOne(VerifiableMemberContext context)
        {
            if (context is null)
                return _options.ReturnNullReferenceOrSuccess();

            if (!context.IncludeAnnotations)
                return VerifyResult.Success;

            var slaveResults = new List<VerifyResult>();

            VerifyViaFlagAnnotations(context, slaveResults);
            VerifyViaVerifiableAnnotations(context, slaveResults);

            return VerifyResult.MakeTogether(slaveResults);
        }

        #endregion

        #region VerifyMany

        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            if (declaringType is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (keyValueCollections is null)
                return _options.ReturnNullReferenceOrSuccess();
            return VerifyMany(_objectResolver.Resolve(declaringType, keyValueCollections));
        }

        public VerifyResult VerifyMany<T>(IDictionary<string, object> keyValueCollections)
        {
            if (keyValueCollections is null)
                return _options.ReturnNullReferenceOrSuccess();
            return VerifyMany(_objectResolver.Resolve<T>(keyValueCollections));
        }

        public VerifyResult VerifyMany(VerifiableObjectContext context)
        {
            if (context is null)
                return _options.ReturnNullReferenceOrSuccess();

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

        #endregion

        private static void VerifyViaFlagAnnotations(VerifiableMemberContext context, List<VerifyResult> results)
        {
            var annotations = context.GetFlagAnnotations(true).ToList();

            if (annotations.Any())
            {
                if (!AnnotationVerificationEngine.Verify(context, annotations, out var failure))
                    results.Add(new VerifyResult(failure));
            }
        }

        private static void VerifyViaVerifiableAnnotations(VerifiableMemberContext context, List<VerifyResult> results)
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
                        var result = contextVerifiableAnnotation.StrongVerify(context.ConvertToObjectContext());

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