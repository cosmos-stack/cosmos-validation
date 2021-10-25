using System;
using System.Collections.Generic;

namespace CosmosStack.Validation.Validators
{
    internal sealed class VerifyRulePackageValidator : IValidator
    {
        public VerifyRulePackageValidator(VerifyRulePackage package)
        {
            if (package is null) throw new ArgumentNullException(nameof(package));
            Handler = ValidationHandler.CreateByRulePackage(package);
            Name = "Shortcut Validator for 'VerifyRulePackage'";
        }

        public VerifyRulePackageValidator(VerifyRulePackage package, ValidationOptions options)
        {
            if (package is null) throw new ArgumentNullException(nameof(package));
            if (options is null) throw new ArgumentNullException(nameof(options));
            Handler = ValidationHandler.CreateByRulePackage(package, options);
            Name = "Shortcut Validator for 'VerifyRulePackage'";
        }

        private ValidationHandler Handler { get; }

        /// <summary>
        /// Name of validation
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Mark whether the validator is anonymous.
        /// </summary>
        public bool IsAnonymous => true;

        /// <summary>
        /// Verify the entire entity
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult Verify(Type declaringType, object instance)
        {
            return Handler.Verify(declaringType, instance);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public VerifyResult VerifyOne(Type declaringType, object memberValue, string memberName)
        {
            return Handler.VerifyOne(declaringType, memberValue, memberName);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithInstance(Type declaringType, object memberValue, string memberName, object instance)
        {
            return Handler.VerifyOneWithInstance(declaringType, memberValue, memberName, instance);
        }
        
        VerifyResult IValidator.VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return VerifyMany(declaringType, keyValueCollections);
        }

        /// <summary>
        /// Verify a member of the entity.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="memberValue"></param>
        /// <param name="memberName"></param>
        /// <param name="keyValueCollection"></param>
        /// <returns></returns>
        public VerifyResult VerifyOneWithDictionary(Type declaringType, object memberValue, string memberName, IDictionary<string, object> keyValueCollection)
        {
            return Handler.VerifyOneWithDictionary(declaringType, memberValue, memberName, keyValueCollection);
        }

        /// <summary>
        /// Verify each member in the non-strongly typed dictionary.
        /// </summary>
        /// <param name="declaringType"></param>
        /// <param name="keyValueCollections"></param>
        /// <returns></returns>
        public VerifyResult VerifyMany(Type declaringType, IDictionary<string, object> keyValueCollections)
        {
            return Handler.VerifyMany(declaringType, keyValueCollections);
        }
    }
}