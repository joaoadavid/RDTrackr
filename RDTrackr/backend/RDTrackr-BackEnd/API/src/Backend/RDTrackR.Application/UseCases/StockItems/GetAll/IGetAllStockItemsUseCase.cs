using RDTrackR.Communication.Responses.StockItem;

namespace RDTrackR.Application.UseCases.StockItems.GetAll
{
    public interface IGetAllStockItemsUseCase
    {
        Task<List<ResponseStockItemJson>> Execute();
    }
}