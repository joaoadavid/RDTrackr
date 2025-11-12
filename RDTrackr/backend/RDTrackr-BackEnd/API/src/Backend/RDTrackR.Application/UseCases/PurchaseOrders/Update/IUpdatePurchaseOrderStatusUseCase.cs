
using RDTrackR.Communication.Requests.PurchaseOrders;

namespace RDTrackR.Application.UseCases.PurchaseOrders.Update
{
    public interface IUpdatePurchaseOrderStatusUseCase
    {
        Task Execute(long id, RequestUpdatePurchaseOrderStatusJson request);
    }
}