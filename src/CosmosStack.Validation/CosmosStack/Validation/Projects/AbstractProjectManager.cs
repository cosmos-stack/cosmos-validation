using System;

namespace CosmosStack.Validation.Projects
{
    /// <summary>
    /// An abstract project manager <br />
    /// 类型 Project 抽象管理器
    /// </summary>
    public abstract class AbstractProjectManager : IValidationProjectManager
    {
        /// <inheritdoc />
        public abstract void Register(IProject project);

        /// <inheritdoc />
        public abstract IProject Resolve(Type type);
        
        /// <inheritdoc />
        public abstract IProject Resolve(Type type, string name);
        
        /// <inheritdoc />
        public abstract  bool TryResolve(Type type, out IProject project);
        
        /// <inheritdoc />
        public abstract bool TryResolve(Type type, string name, out IProject project);
    }
}