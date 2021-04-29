using System;
using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;
using FluentValidation;
using FluentValidation.Internal;

namespace Cosmos.Validation.Internals
{
    internal static class FluentValidationContextFactory
    {
        static FluentValidationContextFactory()
        {
#if !NETFRAMEWORK
            NatashaInitializer.InitializeAndPreheating();
#endif
        }

        public static IValidationContext Resolve(Type declaringType, object instance)
        {
            var t = typeof(ValidationContext<>).MakeGenericType(declaringType);
#if NETFRAMEWORK
            var @params = new object[] {instance};

            if (instance is null)
            {
                var ctor = t.GetConstructor(new[] {declaringType});
                return ctor!.Invoke(@params).AsOrDefault<IValidationContext>();
            }
            else
            {
                return TypeVisit.CreateInstance<IValidationContext>(t, @params);
            }
#else
            var args = new List<ArgumentDescriptor>
            {
                new ArgumentDescriptor("instanceToValidate", instance, declaringType)
            };
            return TypeVisit.CreateInstance<IValidationContext>(t, args);
#endif
        }

        public static IValidationContext Resolve(Type declaringType, string[] properties, object instance)
        {
            var t = typeof(ValidationContext<>).MakeGenericType(declaringType);
#if NETFRAMEWORK
            var @params = new object[] {instance, new PropertyChain(), new MemberNameValidatorSelector(properties)};

            if (instance is null)
            {
                var ctor = t.GetConstructor(new[] {declaringType, typeof(PropertyChain), typeof(IValidatorSelector)});
                return ctor!.Invoke(@params).AsOrDefault<IValidationContext>();
            }
            else
            {
                return TypeVisit.CreateInstance<IValidationContext>(t, @params);
            }
#else
            var args = new List<ArgumentDescriptor>
            {
                new ArgumentDescriptor("instanceToValidate", instance, declaringType),
                new ArgumentDescriptor("propertyChain", new PropertyChain(), typeof(PropertyChain)),
                new ArgumentDescriptor("validatorSelector", new MemberNameValidatorSelector(properties), typeof(IValidatorSelector))
            };
            return TypeVisit.CreateInstance<IValidationContext>(t, args);
#endif
        }

        public static IValidationContext Resolve(VerifiableObjectContext context)
        {
            return Resolve(context.Type, context.Instance);
        }
    }
}