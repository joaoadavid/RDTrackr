using RDTrackR.Communication.Responses.Replenishment;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.Products;
using RDTrackR.Domain.Repositories.StockItems;

namespace RDTrackR.Application.UseCases.Replenishment.GetAll
{
    public class GetReplenishmentItemsUseCase : IGetReplenishmentItemsUseCase
    {
        private readonly IStockItemReadOnlyRepository _stockRepo;
        private readonly IProductReadOnlyRepository _productRepo;

        public GetReplenishmentItemsUseCase(IStockItemReadOnlyRepository stockRepo,
                                            IProductReadOnlyRepository productRepo)
        {
            _stockRepo = stockRepo;
            _productRepo = productRepo;
        }

        public async Task<List<ResponseReplenishmentItemJson>> Execute()
        {
            var items = await _stockRepo.GetReplenishmentCandidatesAsync();
            return items.Select(i => new ResponseReplenishmentItemJson
            {
                ProductId = i.Product.Id,
                Sku = i.Product.Sku,
                Name = i.Product.Name,
                Category = i.Product.Category,
                Uom = i.Product.UoM,
                CurrentStock = i.Quantity,
                ReorderPoint = i.Product.ReorderPoint,
                DailyConsumption = i.Product.DailyConsumption,
                LeadTimeDays = i.Product.LeadTimeDays,
                SuggestedQty = CalculateSuggested(i),
                IsCritical = i.Quantity <= i.Product.ReorderPoint,
                UnitPrice = i.Product.LastPurchasePrice
            }).ToList();
        }

        private decimal CalculateSuggested(StockItem item)
        {
            return (item.Product.LeadTimeDays * item.Product.DailyConsumption)
                    + item.Product.SafetyStock
                    - item.Quantity;
        }
    }

}
