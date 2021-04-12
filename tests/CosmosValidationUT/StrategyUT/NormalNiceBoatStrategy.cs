using Cosmos.Validation;
using Cosmos.Validation.Strategies;
using CosmosValidationUT.Models;

namespace CosmosValidationUT.StrategyUT
{
    public class NormalNiceBoatStrategy : ValidationStrategy
    {
        public NormalNiceBoatStrategy() : base(typeof(NiceBoat))
        {
            ForMember("Name").NotEmpty();
            ForMember("Length").GreaterThanOrEqual(0);
            ForMember("Width").GreaterThanOrEqual(0);
        }
    }
}