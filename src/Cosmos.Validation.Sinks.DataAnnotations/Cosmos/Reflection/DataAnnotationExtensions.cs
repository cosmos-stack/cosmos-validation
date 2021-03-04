using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AspectCore.Extensions.Reflection;

namespace Cosmos.Reflection
{
    public static class DataAnnotationExtensions
    {
        public static bool HasDataAnnotationAttribute(this Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));

            return type.GetReflector()
                       .CustomAttributeReflectors
                       .Any(x => x.AttributeType.IsDerivedFrom<ValidationAttribute>());
        }

        public static IEnumerable<Attribute> GetDataAnnotationAttributes(this Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));

            return type.GetReflector()
                       .CustomAttributeReflectors
                       .Where(x => x.AttributeType.IsDerivedFrom<ValidationAttribute>())
                       .Select(x => x.Invoke());
        }
    }
}