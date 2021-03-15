using System;

namespace Cosmos.Validation.Objects
{
    public static class VerifiableMemberContextExtensions
    {
        public static object GetValue(this VerifiableMemberContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            return context.Value;
        }

        public static TVal GetValue<TVal>(this VerifiableMemberContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            return context.Value.As<TVal>();
        }

        public static object RefreshAndGetValue(this VerifiableMemberContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            context.RefreshValue();
            return context.Value;
        }

        public static TVal RefreshAndGetValue<TVal>(this VerifiableMemberContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            context.RefreshValue();
            return context.Value.As<TVal>();
        }
    }
}