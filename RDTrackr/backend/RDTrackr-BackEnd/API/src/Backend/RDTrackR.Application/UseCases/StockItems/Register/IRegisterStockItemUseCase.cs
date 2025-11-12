using RDTrackR.Communication.Requests.StockItem;
using RDTrackR.Communication.Responses.StockItem;

namespace RDTrackR.Application.UseCases.StockItems.Register
{
    public interface IRegisterStockItemUseCase
    {
        Task<ResponseStockItemJson> Execute(RequestRegisterStockItemJson request);
    }
}