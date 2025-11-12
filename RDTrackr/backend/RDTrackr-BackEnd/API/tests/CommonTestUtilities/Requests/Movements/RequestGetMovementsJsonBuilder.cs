using Bogus;
using RDTrackR.Communication.Enums;
using RDTrackR.Communication.Requests.Movements;

namespace CommonTestUtilities.Requests.Movements
{
    public static class RequestGetMovementsJsonBuilder
    {
        public static RequestGetMovementsJson Build()
        {
            return new Faker<RequestGetMovementsJson>()
                .RuleFor(m => m.WarehouseId, f => f.Random.Bool() ? f.Random.Long(1, 10) : null)
                .RuleFor(m => m.Type, f => f.Random.Bool() ? f.PickRandom<MovementType>() : null)
                .RuleFor(m => m.StartDate, f => f.Random.Bool() ? f.Date.Past(2) : null)
                .RuleFor(m => m.EndDate, f => f.Random.Bool() ? f.Date.Recent(1) : null);
        }
    }
}
