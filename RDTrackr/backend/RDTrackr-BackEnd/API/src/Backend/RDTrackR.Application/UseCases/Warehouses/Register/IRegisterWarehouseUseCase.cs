using RDTrackR.Communication.Requests.Warehouse;
using RDTrackR.Communication.Responses.Warehouse;

namespace RDTrackR.Application.UseCases.Warehouses.Register
{
    public interface IRegisterWarehouseUseCase
    {
        Task<ResponseWarehouseJson> Execute(RequestRegisterWarehouseJson request);
    }
}