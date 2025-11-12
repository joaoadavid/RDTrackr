using RDTrackR.Communication.Requests.PurchaseOrders;
using RDTrackR.Communication.Responses.PurchaseOrders;

namespace RDTrackR.Application.UseCases.PurchaseOrders.Register
{
    public interface IRegisterPurchaseOrderUseCase
    {
        Task<ResponsePurchaseOrderJson> Execute(RequestCreatePurchaseOrderJson request);
    }
}