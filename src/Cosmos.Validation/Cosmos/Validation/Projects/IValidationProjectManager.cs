using System;

namespace Cosmos.Validation.Projects
{
    public interface IValidationProjectManager
    {
        void Register(IProject project);

        IProject Resolve(Type type);

        IProject Resolve(Type type, string name);

        bool TryResolve(Type type, out IProject project);
        
        bool TryResolve(Type type, string name, out IProject project);
    }
}