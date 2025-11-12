using Moq;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Repositories.StockItems;

namespace CommonTestUtilities.Repositories.StockItems
{
    public class StockItemRepositoryBuilder
    {
        private readonly Mock<IStockItemReadOnlyRepository> _readMock = new();
        private readonly Mock<IStockItemWriteOnlyRepository> _writeMock = new();

        public StockItemRepositoryBuilder HasStock(StockItem stockItem)
        {
            _readMock.Setup(r => r.GetByProductAndWarehouseAsync(stockItem.ProductId, stockItem.WarehouseId))
                     .ReturnsAsync(stockItem);
            return this;
        }

        public StockItemRepositoryBuilder NoStock(long productId, long warehouseId)
        {
            _readMock.Setup(r => r.GetByProductAndWarehouseAsync(productId, warehouseId))
                     .ReturnsAsync((StockItem?)null);
            return this;
        }

        public StockItemRepositoryBuilder Add()
        {
            _writeMock.Setup(r => r.AddAsync(It.IsAny<StockItem>()))
                      .Returns(Task.CompletedTask);
            return this;
        }

        public StockItemRepositoryBuilder Update()
        {
            _writeMock.Setup(r => r.UpdateAsync(It.IsAny<StockItem>()))
                      .Returns(Task.CompletedTask);
            return this;
        }

        public IStockItemReadOnlyRepository BuildRead() => _readMock.Object;
        public IStockItemWriteOnlyRepository BuildWrite() => _writeMock.Object;
    }
}
