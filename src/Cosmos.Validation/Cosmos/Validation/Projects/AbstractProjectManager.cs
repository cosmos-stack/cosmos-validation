using System;

namespace Cosmos.Validation.Projects
{
    public abstract class AbstractProjectManager : IValidationProjectManager
    {
        public abstract void Register(IProject project);

        public abstract IProject Resolve(Type type);
        
        public abstract IProject Resolve(Type type, string name);
        
        public abstract  bool TryResolve(Type type, out IProject project);
        
        public abstract bool TryResolve(Type type, string name, out IProject project);
    }
}