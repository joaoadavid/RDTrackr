using Bogus;
using RDTrackR.Communication.Requests.PurchaseOrders;

namespace CommonTestUtilities.Requests.PurchaseOrder
{
    public static class RequestPurchaseOrderJsonBuilder
    {
        public static RequestCreatePurchaseOrderJson Build()
        {
            return new Faker<RequestCreatePurchaseOrderJson>()
                .RuleFor(p => p.SupplierId, f => f.Random.Long(1, 100))
                .RuleFor(p => p.Items, f => f.Make(3, () => new RequestCreatePurchaseOrderItemJson
                {
                    ProductId = f.Random.Long(1, 100),
                    Quantity = f.Random.Decimal(1, 20),
                    UnitPrice = f.Random.Decimal(5, 500)
                }))
                .Generate();
        }
    }
}
