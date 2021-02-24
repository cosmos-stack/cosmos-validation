using Cosmos.Validation.Strategies;
using CosmosValidationUT.Models;

namespace CosmosValidationUT.StrategyUT
{
    public class GenericNiceBoatStrategy : ValidationStrategy<NiceBoat>
    {
        public GenericNiceBoatStrategy()
        {
            ForMember(x => x.Name).NotEmpty();
            ForMember(x => x.Length).GreaterThanOrEqual(0);
            ForMember(x => x.Width).GreaterThanOrEqual(0);
        }
    }
}