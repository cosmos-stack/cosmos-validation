using System;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayTempBuild
    {
        ValidationHandler TempBuild();
        ValidationHandler TempBuild(ValidationOptions options);
        ValidationHandler TempBuild(Action<ValidationOptions> optionsAct);
    }
}