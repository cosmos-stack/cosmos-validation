using System;
using System.Collections.Generic;
using Cosmos.Reflection;
using Cosmos.Validation.Objects;
using FluentValidation;

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
            var @params = new object[] { instance };

            if (instance is null)
            {
                var ctor = t.GetConstructor(new[] { declaringType });
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

        public static IValidationContext Resolve(VerifiableObjectContext context)
        {
            return Resolve(context.Type, context.Instance);
        }
    }
}