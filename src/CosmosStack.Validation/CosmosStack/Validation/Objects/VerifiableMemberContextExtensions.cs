using System;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// Verifiable member context extensions <br />
    /// 可验证成员上下文扩展
    /// </summary>
    public static class VerifiableMemberContextExtensions
    {
        /// <summary>
        /// Get value <br />
        /// 获取值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static object GetValue(this VerifiableMemberContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            return context.Value;
        }

        /// <summary>
        /// Get value <br />
        /// 获取值
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TVal GetValue<TVal>(this VerifiableMemberContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            return context.Value.As<TVal>();
        }

        /// <summary>
        /// Refresh and get value <br />
        /// 刷新并获取值
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static object RefreshAndGetValue(this VerifiableMemberContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            context.RefreshValue();
            return context.Value;
        }

        /// <summary>
        /// Refresh and get value <br />
        /// 刷新并获取值
        /// </summary>
        /// <param name="context"></param>
        /// <typeparam name="TVal"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static TVal RefreshAndGetValue<TVal>(this VerifiableMemberContext context)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));
            context.RefreshValue();
            return context.Value.As<TVal>();
        }
    }
}