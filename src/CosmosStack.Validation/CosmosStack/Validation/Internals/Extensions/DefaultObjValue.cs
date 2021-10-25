using System;
using System.Runtime.CompilerServices;
using CosmosStack.Reflection;
using CosmosStack.Validation.Objects;

namespace CosmosStack.Validation.Internals.Extensions
{
    internal static class DefaultObjValue
    {
        public static object Get(Type type)
        {
            if (type == TypeClass.ByteClazz)
                return default(byte);
            
            if (type == TypeClass.ShortClazz)
                return default(short);
            
            if (type == TypeClass.IntClazz)
                return default(int);
            
            if (type == TypeClass.LongClazz)
                return default(long);
            
            if (type == TypeClass.SByteClazz)
                return default(sbyte);
            
            if (type == TypeClass.UShortClazz)
                return default(ushort);
            
            if (type == TypeClass.UIntClazz)
                return default(uint);
            
            if (type == TypeClass.ULongClazz)
                return default(ulong);
            
            if (type == TypeClass.CharClazz)
                return default(char);
            
            if (type == TypeClass.FloatClazz)
                return default(float);
            
            if (type == TypeClass.DoubleClazz)
                return default(double);
            
            if (type == TypeClass.DecimalClazz)
                return default(decimal);
            
            if (type == TypeClass.DateTimeClazz)
                return default(DateTime);
            
            if (type == TypeClass.DateTimeOffsetClazz)
                return default(DateTimeOffset);
            
            if (type == TypeClass.TimeSpanClazz)
                return default(TimeSpan);

            if (type.IsEnum)
                return default;

            return default;
        }
    }
    
    internal static class DefaultObjValueExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetDefaultValue(this VerifiableMemberContext context)
        {
            return DefaultObjValue.Get(context.MemberType);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object GetDefaultValue(this VerifiableMemberContract context)
        {
            return DefaultObjValue.Get(context.MemberType);
        }
    }
}