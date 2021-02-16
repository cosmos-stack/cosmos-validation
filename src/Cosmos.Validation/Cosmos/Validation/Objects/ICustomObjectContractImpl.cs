using System.Collections.Generic;

namespace Cosmos.Validation.Objects
{
    public interface ICustomObjectContractImpl : IObjectContract
    {
        Dictionary<string, ObjectValueContract> GetValueContractMap();
    }
}