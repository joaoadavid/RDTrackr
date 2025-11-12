using RDTrackR.Communication.Requests.Replenishment;
using RDTrackR.Communication.Responses.PurchaseOrders;

namespace RDTrackR.Application.UseCases.Replenishment.Register
{
    public interface IGeneratePoFromReplenishmentUseCase
    {
        Task<ResponsePurchaseOrderJson> Execute(RequestGeneratePoFromReplenishmentJson request);
    }
}