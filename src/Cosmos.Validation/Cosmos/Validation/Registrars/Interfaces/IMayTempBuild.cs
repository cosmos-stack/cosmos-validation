using System;
using Cosmos.Validation.Objects;

namespace Cosmos.Validation.Registrars.Interfaces
{
    public interface IMayTempBuild
    {
        ValidationHandler TempBuild();
        ValidationHandler TempBuild(ValidationOptions options);
        ValidationHandler TempBuild(Action<ValidationOptions> optionsAct);
        ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver);
        ValidationHandler TempBuild(IVerifiableObjectResolver objectResolver, ValidationOptions options);
    }
}