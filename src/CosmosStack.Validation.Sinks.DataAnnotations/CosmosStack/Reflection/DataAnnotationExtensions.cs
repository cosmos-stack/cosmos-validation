using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AspectCore.Extensions.Reflection;

namespace CosmosStack.Reflection
{
    /// <summary>
    /// DataAnnotation Extensions <br />
    /// 数据注解扩展
    /// </summary>
    public static class DataAnnotationExtensions
    {
        /// <summary>
        /// Has DataAnnotation Attribute <br />
        /// 是否存在数据注解特性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool HasDataAnnotationAttribute(this Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));

            return type.GetReflector()
                       .CustomAttributeReflectors
                       .Any(x => x.AttributeType.IsDerivedFrom<ValidationAttribute>());
        }

        /// <summary>
        /// Get all DataAnnotation Attributes <br />
        /// 获取所有数据注解特性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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