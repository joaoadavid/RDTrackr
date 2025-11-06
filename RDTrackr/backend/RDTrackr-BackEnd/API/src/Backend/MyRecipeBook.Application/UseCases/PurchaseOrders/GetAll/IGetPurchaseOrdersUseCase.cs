using RDTrackR.Communication.Responses.PurchaseOrders;

namespace RDTrackR.Application.UseCases.PurchaseOrders.GetAll
{
    public interface IGetPurchaseOrdersUseCase
    {
        Task<List<ResponsePurchaseOrderJson>> Execute();
    }
}