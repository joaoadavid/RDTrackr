using RDTrackR.Communication.Requests.PurchaseOrders;

namespace RDTrackR.Application.UseCases.PurchaseOrders.Update
{
    public interface IUpdatePurchaseOrderItemsUseCase
    {
        Task Execute(long id, RequestUpdatePurchaseOrderItemsJson request);
    }
}