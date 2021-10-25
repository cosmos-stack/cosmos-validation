using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CosmosStack.Reflection;
using CosmosStack.Validation.Annotations;
using CosmosStack.Validation.Internals;
using CosmosStack.Validation.Internals.Extensions;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Validators
{
    /// <summary>
    /// Annotation Validator <br />
    /// 注解验证器
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

        /// <summary>
        /// Name of validation <br />
        /// 验证倾名称
        /// </summary>
        public string Name => "Annotation Validator";

        /// <summary>
        /// Mark whether the validator is anonymous. <br />
        /// 标记是否为匿名验证器
        /// </summary>
        public bool IsAnonymous => false;

        /// <inheritdoc />
        bool ICorrectValidator.IsTypeBinding => false;

        /// <inheritdoc />
        Type ICorrectValidator.SourceType => TypeClass.ObjectClazz;

        #region GetInstance

        /// <summary>
        /// Create a new instance of Annotation Validator
        /// </summary>
        public static AnnotationValidator Instance { get; } = new();

        /// <summary>
        /// Create a new instance of Annotation Validator
        /// </summary>
        /// <returns></returns>
        public static AnnotationValidator GetInstance() => Instance;

        /// <summary>
        /// Create a new instance of Annotation Validator with given options
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static AnnotationValidator GetInstance(ValidationOptions options)
        {
            return new(new DefaultVerifiableObjectResolver(), options);
        }

        /// <summary>
        /// Create a new instance of Annotation Validator with given options and Object Resolver
        /// </summary>
        /// <param name="objectResolver"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static AnnotationValidator GetInstance(IVerifiableObjectResolver objectResolver, ValidationOptions options)
        {
            return new(objectResolver, options);
        }

        #endregion

        #region Verify

        /// <summary>
        /// Verify the entire entity <br />
        /// 验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Verify the entire entity <br />
        /// 验证入口
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Verify the entire entity <br />
        /// 验证入口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="memberValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance)
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
            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOne(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance)));
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance<T, TVal>(TVal memberValue, string memberName, T instance)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (memberValue is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (memberValue is IDictionary<string, object> keyValueCollection)
                return VerifyMany<T>(keyValueCollection);
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOne(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance)));
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="memberValue"></param>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue, T instance)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (memberValue is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (memberValue is IDictionary<string, object> keyValueCollection)
                return VerifyMany<T>(keyValueCollection);
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOne(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance)));
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            if (declaringType is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (memberValue is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (memberValue is IDictionary<string, object> keyValueCollections)
                return VerifyMany(declaringType, keyValueCollections);
            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOne(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection)));
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary<T, TVal>(TVal memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (memberValue is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (memberValue is IDictionary<string, object> keyValueCollections)
                return VerifyMany<T>(keyValueCollections);
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOne(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection)));
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="memberValue"></param>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue, IDictionary<string, object> keyValueCollection)
        {
            if (memberValue is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (memberValue is VerifiableObjectContext objectContext)
                return Verify(objectContext);
            if (memberValue is VerifiableMemberContext memberContext)
                return VerifyOne(memberContext);
            if (memberValue is IDictionary<string, object> keyValueCollections)
                return VerifyMany<T>(keyValueCollections);
            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            return VerifyOne(VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection)));
        }

        /// <summary>
        /// Verify a member of the entity. <br />
        /// 成员验证入口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary. <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            if (declaringType is null)
                return _options.ReturnNullReferenceOrSuccess();
            if (keyValueCollections is null)
                return _options.ReturnNullReferenceOrSuccess();
            return VerifyMany(_objectResolver.Resolve(declaringType, keyValueCollections));
        }

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary. <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyMany<T>(IDictionary<string, object> keyValueCollections)
        {
            if (keyValueCollections is null)
                return _options.ReturnNullReferenceOrSuccess();
            return VerifyMany(_objectResolver.Resolve<T>(keyValueCollections));
        }

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary. <br />
        /// 字典验证入口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
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
                            var localErrors = result.Errors.First().Details.Select(x => new VerifyError
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
                            var localErrors = result.Errors.First().Details.Select(x => new VerifyError
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