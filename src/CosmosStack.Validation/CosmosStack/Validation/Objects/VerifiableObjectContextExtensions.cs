using System;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Verifiable Object Context extensions <br />
    /// 可验证对象上下文扩展
    /// </summary>
    public static class VerifiableObjectContextExtensions
    {
        /// <summary>
        /// Get instance <br />
        /// 获取实例
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static object GetInstance(this VerifiableObjectContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            return context.Instance;
        }

        /// <summary>
        /// Get instance <br />
        /// 获取实例
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T GetInstance<T>(this VerifiableObjectContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            return context.Instance.As<T>();
        }
    }
}