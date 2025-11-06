
namespace RDTrackR.Application.UseCases.PurchaseOrders.Delete
{
    public interface IDeletePurchaseOrderUseCase
    {
        Task Execute(long id);
    }
}