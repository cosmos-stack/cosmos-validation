using System;

namespace CosmosStack.Validation.Projects
{
    /// <summary>
    /// Validation Project Manager Interface <br />
    /// 验证类型 project 管理器接口
    /// </summary>
    public interface IValidationProjectManager
    {
        /// <summary>
        /// Register project <br />
        /// 注册项目
        /// </summary>
        /// <param name="project"></param>
        void Register(IProject project);

        /// <summary>
        /// Revolve <br />
        /// 解析项目
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IProject Resolve(Type type);

        /// <summary>
        /// Revolve <br />
        /// 解析项目
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IProject Resolve(Type type, string name);

        /// <summary>
        /// Try to revolve <br />
        /// 尝试解析项目
        /// </summary>
        /// <param name="type"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        bool TryResolve(Type type, out IProject project);
        
        /// <summary>
        /// Try to revolve <br />
        /// 尝试解析项目
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        bool TryResolve(Type type, string name, out IProject project);
    }
}