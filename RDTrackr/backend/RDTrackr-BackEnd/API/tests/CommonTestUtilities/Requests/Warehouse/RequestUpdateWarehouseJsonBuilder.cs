using Bogus;
using RDTrackR.Communication.Requests.Warehouse;

namespace CommonTestUtilities.Requests.Warehouse
{
    public static class RequestUpdateWarehouseJsonBuilder
    {
        public static RequestUpdateWarehouseJson Build()
        {
            return new Faker<RequestUpdateWarehouseJson>()
                .RuleFor(w => w.Name, f => $"Depósito {f.Address.City()}")
                .RuleFor(w => w.Location, f => $"{f.Address.City()}, {f.Address.State()}")
                .RuleFor(w => w.Capacity, f => f.Random.Int(100, 5000))
                .RuleFor(w => w.Items, f => f.Random.Int(0, 200));
        }
    }
}
