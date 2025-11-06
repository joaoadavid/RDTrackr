using RDTrackR.Communication.Requests.Warehouse;
using RDTrackR.Communication.Responses.Warehouse;

namespace RDTrackR.Application.UseCases.Warehouses.Update
{
    public interface IUpdateWarehouseUseCase
    {
        Task<ResponseWarehouseJson> Execute(long id, RequestUpdateWarehouseJson request);
    }
}