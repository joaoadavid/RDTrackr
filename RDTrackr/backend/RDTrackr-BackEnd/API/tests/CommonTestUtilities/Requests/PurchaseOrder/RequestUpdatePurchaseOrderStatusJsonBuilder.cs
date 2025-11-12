using Bogus;
using RDTrackR.Communication.Requests.PurchaseOrders;
using RDTrackR.Domain.Enums;

namespace CommonTestUtilities.Requests.PurchaseOrder
{
    public static class RequestUpdatePurchaseOrderStatusJsonBuilder
    {
        public static RequestUpdatePurchaseOrderStatusJson Build()
        {
            return new Faker<RequestUpdatePurchaseOrderStatusJson>()
                .RuleFor(p => p.Status, f => f.PickRandom<PurchaseOrderStatus>().ToString());
        }
    }
}
