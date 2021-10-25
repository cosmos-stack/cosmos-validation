using System;

namespace CosmosStack.Validation.Objects
{
    /// <summary>
    /// An interface of implementation for custom verifiable member contract <br />
    /// 实现自定义可验证成员约定的接口
    /// </summary>
    public interface ICustomVerifiableMemberContractImpl : IVerifiableMemberContract
    {
        /// <summary>
        /// Has attribute defined <br />
        /// 标记特性是否定义
        /// </summary>
        /// <typeparam name="TAttr"></typeparam>
        /// <returns></returns>
        bool HasAttributeDefined<TAttr>()
            where TAttr : Attribute;

        /// <summary>
        /// Has attribute defined <br />
        /// 标记特性是否定义
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <returns></returns>
        bool HasAttributeDefined<TAttr1, TAttr2>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute;

        /// <summary>
        /// Has attribute defined <br />
        /// 标记特性是否定义
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <returns></returns>
        bool HasAttributeDefined<TAttr1, TAttr2, TAttr3>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute;

        /// <summary>
        /// Has attribute defined <br />
        /// 标记特性是否定义
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <typeparam name="TAttr4"></typeparam>
        /// <returns></returns>
        bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute;

        /// <summary>
        /// Has attribute defined <br />
        /// 标记特性是否定义
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <typeparam name="TAttr4"></typeparam>
        /// <typeparam name="TAttr5"></typeparam>
        /// <returns></returns>
        bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute;

        /// <summary>
        /// Has attribute defined <br />
        /// 标记特性是否定义
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <typeparam name="TAttr4"></typeparam>
        /// <typeparam name="TAttr5"></typeparam>
        /// <typeparam name="TAttr6"></typeparam>
        /// <returns></returns>
        bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
            where TAttr6 : Attribute;

        /// <summary>
        /// Has attribute defined <br />
        /// 标记特性是否定义
        /// </summary>
        /// <typeparam name="TAttr1"></typeparam>
        /// <typeparam name="TAttr2"></typeparam>
        /// <typeparam name="TAttr3"></typeparam>
        /// <typeparam name="TAttr4"></typeparam>
        /// <typeparam name="TAttr5"></typeparam>
        /// <typeparam name="TAttr6"></typeparam>
        /// <typeparam name="TAttr7"></typeparam>
        /// <returns></returns>
        bool HasAttributeDefined<TAttr1, TAttr2, TAttr3, TAttr4, TAttr5, TAttr6, TAttr7>()
            where TAttr1 : Attribute
            where TAttr2 : Attribute
            where TAttr3 : Attribute
            where TAttr4 : Attribute
            where TAttr5 : Attribute
            where TAttr6 : Attribute
            where TAttr7 : Attribute;
    }
}