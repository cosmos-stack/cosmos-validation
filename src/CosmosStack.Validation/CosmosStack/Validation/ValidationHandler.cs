﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CosmosStack.Collections;
using CosmosStack.Validation.Internals;
using CosmosStack.Validation.Internals.Extensions;
using CosmosStack.Validation.Objects;
using CosmosStack.Validation.Projects;
using CosmosStack.Validation.Registrars;
using CosmosStack.Validation.Strategies;
using CosmosStack.Validation.Validators;

namespace CosmosStack.Validation
{
    /// <summary>
    /// The verification processor is generated by the short-circuit construction mode. <br />
    /// 验证处理器
    /// </summary>
    public class ValidationHandler
    {
        private readonly Dictionary<(int, int), IProject> _namedTypeProjects = new();
        private readonly Dictionary<int, IProject> _typedProjects = new();

        private readonly IVerifiableObjectResolver _objectResolver;
        private readonly ICustomValidatorManager _customValidatorManager;
        private readonly ValidationOptions _options;

        internal ValidationHandler(
            IEnumerable<IProject> projects,
            IVerifiableObjectResolver objectResolver,
            ICustomValidatorManager customValidatorManager,
            ValidationOptions options)
        {
            _objectResolver = objectResolver ?? throw new ArgumentNullException(nameof(objectResolver));
            _customValidatorManager = customValidatorManager ?? throw new ArgumentNullException(nameof(customValidatorManager));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            if (projects is not null)
                foreach (var project in projects)
                    UpdateProject(project);

            AnnotationValidator = options.AnnotationEnabled
                ? AnnotationValidator.GetInstance(_objectResolver, _options)
                : default;
        }

        private void UpdateProject(IProject project)
        {
            if (project is not null)
            {
                switch (project.Class)
                {
                    case ProjectClass.Typed:
                        _typedProjects.AddValueOrOverride(project.Type.GetHashCode(), project);
                        break;

                    case ProjectClass.Named:
                        _namedTypeProjects.AddValueOrOverride((project.Type.GetHashCode(), project.Name.GetHashCode()), project);
                        break;

                    default:
                        throw new InvalidOperationException("Unknown validation project.");
                }
            }
        }

        private AnnotationValidator AnnotationValidator { get; set; }

        #region Verify

        /// <summary>
        /// Verify the entire object.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult Verify(Type declaringType, object instance)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));
            return Verify(_objectResolver.Resolve(declaringType, instance), "");
        }

        /// <summary>
        /// Verify the entire object.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult Verify(Type declaringType, object instance, string projectName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));
            return Verify(_objectResolver.Resolve(declaringType, instance), projectName);
        }

        /// <summary>
        /// Verify the entire object.
        /// </summary>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult Verify<T>(T instance) => Verify(typeof(T), instance);

        /// <summary>
        /// Verify the entire object.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult Verify<T>(T instance, string projectName) => Verify(typeof(T), instance, projectName);

        /// <summary>
        /// Verify the entire object.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal VerifyResult Verify(VerifiableObjectContext context, string projectName = "")
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            IProject project;
            VerifyResult result1 = null, result2 = null, result3 = null;

            if (string.IsNullOrWhiteSpace(projectName))
            {
                if (_typedProjects.TryGetValue(context.Type.GetHashCode(), out project))
                    result1 = project.Verify(context);
            }
            else
            {
                if (_namedTypeProjects.TryGetValue((context.Type.GetHashCode(), projectName.GetHashCode()), out project))
                    result1 = project.Verify(context);
            }

            if (_options.CustomValidatorEnabled)
                result2 = CorrectEngine.ValidViaCustomValidators(context, _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result3 = AnnotationValidator.Verify(context);

            if (result1 is null && result2 is null && result3 is null)
                return _options.ReturnUnexpectedTypeOrSuccess();

            return result1 is null
                ? VerifyResult.MakeTogether(result2, result3)
                : VerifyResult.Merge(result1, result2, result3);
        }

        #endregion

        #region VerifyOne

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var memberContract = VerifiableObjectContractManager.Resolve(declaringType)?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract);

            return VerifyOne(memberContext, declaringType, "");
        }

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName, string projectName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var valueContract = VerifiableObjectContractManager.Resolve(declaringType)?.GetMemberContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = VerifiableMemberContext.Create(memberValue, valueContract);

            return VerifyOne(valueContext, declaringType, projectName);
        }

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOne<T>(object memberValue, string memberName)
            => VerifyOne(typeof(T), memberValue, memberName);

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOne<T>(object memberValue, string memberName, string projectName)
            => VerifyOne(typeof(T), memberValue, memberName, projectName);

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="memberValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOne<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var valueContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = VerifiableMemberContext.Create(memberValue, valueContract);

            return VerifyOne(valueContext, typeof(T), "");
        }

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="memberValue"></param>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOne<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue, string projectName)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var valueContract = VerifiableObjectContractManager.Resolve<T>()?.GetMemberContract(memberName);
            if (valueContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = VerifiableMemberContext.Create(memberValue, valueContract);

            return VerifyOne(valueContext, typeof(T), projectName);
        }

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="declaringType"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal VerifyResult VerifyOne(VerifiableMemberContext context, Type declaringType = default, string projectName = "")
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            IProject project;
            VerifyResult result1 = null, result2 = null, result3 = null;

            if (string.IsNullOrWhiteSpace(projectName))
            {
                var t = declaringType ?? context.DeclaringType;
                if (_typedProjects.TryGetValue(t.GetHashCode(), out project))
                    result1 = project.VerifyOne(context);
            }
            else
            {
                var t = declaringType ?? context.DeclaringType;
                if (_namedTypeProjects.TryGetValue((t.GetHashCode(), projectName.GetHashCode()), out project))
                    result1 = project.VerifyOne(context);
            }

            if (_options.CustomValidatorEnabled)
                result2 = CorrectEngine.ValidViaCustomValidators(context, _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result3 = AnnotationValidator.VerifyOne(context);

            if (result1 is null && result2 is null && result3 is null)
                return _options.ReturnUnexpectedTypeOrSuccess();

            return result1 is null
                ? VerifyResult.MakeTogether(result2, result3)
                : VerifyResult.Merge(result1, result2, result3);
        }

        #endregion

        #region VerifyOneWithInstance

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance));

            return VerifyOne(memberContext, declaringType, "");
        }

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance, string projectName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance));

            return VerifyOne(memberContext, declaringType, projectName);
        }

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance<T>(object memberValue, string memberName, T instance)
            => VerifyOneWithInstance(typeof(T), memberValue, memberName, instance);

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance<T>(object memberValue, string memberName, T instance, string projectName)
            => VerifyOneWithInstance(typeof(T), memberValue, memberName, instance, projectName);

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="memberValue"></param>
        /// <param name="instance"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOneWithInstance<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue, T instance)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance));

            return VerifyOne(memberContext, typeof(T), "");
        }

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="memberValue"></param>
        /// <param name="instance"></param>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOneWithInstance<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue, T instance, string projectName)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithInstance(instance));

            return VerifyOne(valueContext, typeof(T), projectName);
        }

        #endregion

        #region VerifyOneWithDictionary

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection));

            return VerifyOne(memberContext, declaringType, "");
        }

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection, string projectName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var parentContract = VerifiableObjectContractManager.Resolve(declaringType);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection));

            return VerifyOne(memberContext, declaringType, projectName);
        }


        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary<T>(object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
            => VerifyOneWithDictionary(typeof(T), memberValue, memberName, keyValueCollection);

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary<T>(object memberValue, string memberName, IDictionary<string, object> keyValueCollection, string projectName) =>
            VerifyOneWithDictionary(typeof(T), memberValue, memberName, keyValueCollection, projectName);


        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="memberValue"></param>
        /// <param name="keyValueCollection"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOneWithDictionary<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue, IDictionary<string, object> keyValueCollection)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var memberContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection));

            return VerifyOne(memberContext, typeof(T), "");
        }

        /// <summary>
        /// Verify a member of the object.
        /// </summary>
        /// <param name="propertySelector"></param>
        /// <param name="memberValue"></param>
        /// <param name="keyValueCollection"></param>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyOneWithDictionary<T, TVal>(Expression<Func<T, TVal>> propertySelector, TVal memberValue, IDictionary<string, object> keyValueCollection, string projectName)
        {
            if (propertySelector is null)
                throw new ArgumentNullException(nameof(propertySelector));

            var parentContract = VerifiableObjectContractManager.Resolve<T>();
            var memberName = PropertySelector.GetPropertyName(propertySelector);
            var memberContract = parentContract?.GetMemberContract(memberName);
            if (memberContract is null)
                return VerifyResult.MemberIsNotExists(memberName);
            var valueContext = VerifiableMemberContext.Create(memberValue, memberContract, parentContract.WithDictionary(keyValueCollection));

            return VerifyOne(valueContext, typeof(T), projectName);
        }

        #endregion

        #region VerifyMany

        /// <summary>
        /// Verify the dictionary type object. <br />
        /// (The key of the dictionary is the member name of the object, and the value of the dictionary is the value of the object member)
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var context = _objectResolver.Resolve(declaringType, keyValueCollections);

            return VerifyMany(context, "");
        }

        /// <summary>
        /// Verify the dictionary type object. <br />
        /// (The key of the dictionary is the member name of the object, and the value of the dictionary is the value of the object member)
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections, string projectName)
        {
            if (declaringType is null)
                throw new ArgumentNullException(nameof(declaringType));

            var context = _objectResolver.Resolve(declaringType, keyValueCollections);

            return VerifyMany(context, projectName);
        }

        /// <summary>
        /// Verify the dictionary type object. <br />
        /// (The key of the dictionary is the member name of the object, and the value of the dictionary is the value of the object member)
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyMany<T>(IDictionary<string, object> keyValueCollections) => VerifyMany(typeof(T), keyValueCollections);

        /// <summary>
        /// Verify the dictionary type object. <br />
        /// (The key of the dictionary is the member name of the object, and the value of the dictionary is the value of the object member)
        /// </summary>
        /// <param name="keyValueCollections"></param>
        /// <param name="projectName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public VerifyResult VerifyMany<T>(IDictionary<string, object> keyValueCollections, string projectName) => VerifyMany(typeof(T), keyValueCollections, projectName);

        /// <summary>
        /// Verify the dictionary type object. <br />
        /// (The key of the dictionary is the member name of the object, and the value of the dictionary is the value of the object member)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        internal VerifyResult VerifyMany(VerifiableObjectContext context, string projectName = "")
        {
            if (context is null)
                throw new ArgumentNullException(nameof(context));

            IProject project;
            VerifyResult result1 = null, result2 = null, result3 = null;

            if (string.IsNullOrWhiteSpace(projectName))
            {
                if (_typedProjects.TryGetValue(context.Type.GetHashCode(), out project))
                    result1 = project.VerifyMany(context.GetValueMap());
            }
            else
            {
                if (_namedTypeProjects.TryGetValue((context.Type.GetHashCode(), projectName.GetHashCode()), out project))
                    result1 = project.VerifyMany(context.GetValueMap());
            }

            if (_options.CustomValidatorEnabled)
                result2 = CorrectEngine.ValidViaCustomValidators(context.GetValueMap(), _customValidatorManager.ResolveAll());

            if (_options.AnnotationEnabled)
                result3 = AnnotationValidator.VerifyMany(context);

            if (result1 is null && result2 is null && result3 is null)
                return _options.ReturnUnexpectedTypeOrSuccess();

            return result1 is null
                ? VerifyResult.MakeTogether(result2, result3)
                : VerifyResult.Merge(result1, result2, result3);
        }

        #endregion

        #region Merge

        /// <summary>
        /// Merge projects
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        internal ValidationHandler Merge(IEnumerable<IProject> projects)
        {
            if (projects is not null)
                foreach (var project in projects)
                    UpdateProject(project);
            return this;
        }

        #endregion

        #region CreateByStrategy

        internal static ValidationHandler CreateByStrategy<TStrategy>() where TStrategy : class, IValidationStrategy, new()
        {
            return ValidationRegistrar.DefaultRegistrar.ForStrategy<TStrategy>().TempBuild();
        }

        internal static ValidationHandler CreateByStrategy<TStrategy, T>() where TStrategy : class, IValidationStrategy<T>, new()
        {
            return ValidationRegistrar.DefaultRegistrar.ForStrategy<TStrategy, T>().TempBuild();
        }

        internal static ValidationHandler CreateByStrategy<TStrategy>(ValidationOptions options) where TStrategy : class, IValidationStrategy, new()
        {
            return ValidationRegistrar.DefaultRegistrar.ForStrategy<TStrategy>().TempBuild(options);
        }

        internal static ValidationHandler CreateByStrategy<TStrategy, T>(ValidationOptions options) where TStrategy : class, IValidationStrategy<T>, new()
        {
            return ValidationRegistrar.DefaultRegistrar.ForStrategy<TStrategy, T>().TempBuild(options);
        }

        internal static ValidationHandler CreateByStrategy(IValidationStrategy strategy)
        {
            return ValidationRegistrar.DefaultRegistrar.ForStrategy(strategy).TempBuild();
        }

        internal static ValidationHandler CreateByStrategy<T>(IValidationStrategy<T> strategy)
        {
            return ValidationRegistrar.DefaultRegistrar.ForStrategy(strategy).TempBuild();
        }

        internal static ValidationHandler CreateByStrategy(IValidationStrategy strategy, ValidationOptions options)
        {
            return ValidationRegistrar.DefaultRegistrar.ForStrategy(strategy).TempBuild(options);
        }

        internal static ValidationHandler CreateByStrategy<T>(IValidationStrategy<T> strategy, ValidationOptions options)
        {
            return ValidationRegistrar.DefaultRegistrar.ForStrategy(strategy).TempBuild(options);
        }

        #endregion

        #region CreateByRulePackage

        internal static ValidationHandler CreateByRulePackage(VerifyRulePackage package)
        {
            return ValidationRegistrar.DefaultRegistrar.ForRulePackage(package).TempBuild();
        }

        internal static ValidationHandler CreateByRulePackage(VerifyRulePackage package, ValidationOptions options)
        {
            return ValidationRegistrar.DefaultRegistrar.ForRulePackage(package).TempBuild(options);
        }

        #endregion

        #region ExposeVerifyRulePackage

        public VerifyRulePackage ExposeRulePackage<T>(string projectName = "")
        {
            return GetProject(typeof(T), projectName)?.ExposeRules() ?? VerifyRulePackage.Empty;
        }

        public VerifyRulePackage ExposeRulePackage(Type declaringType, string projectName = "")
        {
            if (declaringType is null)
                return VerifyRulePackage.Empty;
            return GetProject(declaringType, projectName)?.ExposeRules() ?? VerifyRulePackage.Empty;
        }

        public VerifyMemberRulePackage ExposeMemberRulePackage<T>(string memberName, string projectName = "")
        {
            return GetProject(typeof(T), projectName)?.ExposeMemberRules(memberName) ?? VerifyMemberRulePackage.Empty;
        }

        public VerifyMemberRulePackage ExposeMemberRulePackage(Type declaringType, string memberName, string projectName = "")
        {
            if (declaringType is null)
                return VerifyMemberRulePackage.Empty;
            return GetProject(declaringType, projectName)?.ExposeMemberRules(memberName) ?? VerifyMemberRulePackage.Empty;
        }

        private IProject GetProject(Type declaringType, string projectName)
        {
            IProject project;
            if (string.IsNullOrWhiteSpace(projectName))
            {
                if (_typedProjects.TryGetValue(declaringType.GetHashCode(), out project))
                    return project;
            }
            else
            {
                if (_namedTypeProjects.TryGetValue((declaringType.GetHashCode(), projectName.GetHashCode()), out project))
                    return project;
            }

            return default;
        }

        #endregion
    }
}