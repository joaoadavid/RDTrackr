using Bogus;
using RDTrackR.Communication.Requests.Product;

namespace CommonTestUtilities.Requests
{
    public static class RequestProductJsonBuilder
    {
        public static RequestRegisterProductJson Build()
        {
            return new Faker<RequestRegisterProductJson>()
                .RuleFor(p => p.Sku, f => f.Commerce.Ean13())
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Category, f => f.Commerce.Department())
                .RuleFor(p => p.UoM, "UN")
                .RuleFor(p => p.Price, f => f.Finance.Amount(10, 500))
                .RuleFor(p => p.Stock, f => f.Random.Int(0, 200))
                .RuleFor(p => p.ReorderPoint, f => f.Random.Int(1, 20));
        }
    }
}
