using System;

namespace Cosmos.Validation.Objects
{
    public static class VerifiableObjectContextExtensions
    {
        public static object GetInstance(this VerifiableObjectContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            return context.Instance;
        }

        public static T GetInstance<T>(this VerifiableObjectContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            return context.Instance.As<T>();
        }
    }
}