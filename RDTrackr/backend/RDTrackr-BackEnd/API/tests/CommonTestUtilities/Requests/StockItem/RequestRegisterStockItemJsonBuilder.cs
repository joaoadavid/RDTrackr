using Bogus;
using RDTrackR.Communication.Requests.StockItem;

namespace CommonTestUtilities.Requests.StockItem
{
    public static class RequestRegisterStockItemJsonBuilder
    {
        public static RequestRegisterStockItemJson Build(long? productId = null, long? warehouseId = null, decimal? quantity = null)
        {
            return new Faker<RequestRegisterStockItemJson>()
                .RuleFor(s => s.ProductId, _ => productId ?? new Random().Next(1, 50))
                .RuleFor(s => s.WarehouseId, _ => warehouseId ?? new Random().Next(1, 10))
                .RuleFor(s => s.Quantity, f => quantity ?? f.Random.Decimal(1, 200));
        }
    }
}
