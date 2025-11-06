using RDTrackR.Communication.Responses.Overview;
using RDTrackR.Domain.Repositories.Products;
using RDTrackR.Domain.Repositories.Warehouses;
using RDTrackR.Domain.Repositories.Movements;
using RDTrackR.Domain.Repositories.StockItems;

namespace RDTrackR.Application.UseCases.Overview.Get
{
    public class GetOverviewUseCase : IGetOverviewUseCase
    {
        private readonly IProductReadOnlyRepository _productRepo;
        private readonly IWarehouseReadOnlyRepository _warehouseRepo;
        private readonly IMovementReadOnlyRepository _movementRepo;
        private readonly IStockItemReadOnlyRepository _stockItemRepo;

        public GetOverviewUseCase(
            IProductReadOnlyRepository productRepo,
            IWarehouseReadOnlyRepository warehouseRepo,
            IMovementReadOnlyRepository movementRepo,
            IStockItemReadOnlyRepository stockItemRepo)
        {
            _productRepo = productRepo;
            _warehouseRepo = warehouseRepo;
            _movementRepo = movementRepo;
            _stockItemRepo = stockItemRepo;
        }

        public async Task<ResponseOverviewJson> Execute()
        {
            var totalProducts = await _productRepo.CountAsync();
            var totalWarehouses = await _warehouseRepo.CountAsync();
            var totalMovements = await _movementRepo.CountAsync();
            var stockItems = await _stockItemRepo.GetAllAsync();

            return new ResponseOverviewJson
            {
                TotalProducts = totalProducts,
                TotalWarehouses = totalWarehouses,
                TotalMovements = totalMovements,
                TotalStockItems = stockItems.Count,
                TotalInventoryQuantity = stockItems.Sum(s => s.Quantity)
            };
        }
    }
}
