using RDTrackR.Communication.Responses.PurchaseOrders;

namespace RDTrackR.Application.UseCases.PurchaseOrders.GetAll
{
    public interface IGetByIdPurchaseOrdersUseCase
    {
        Task<List<ResponsePurchaseOrderJson>> Execute(long id);
    }
}