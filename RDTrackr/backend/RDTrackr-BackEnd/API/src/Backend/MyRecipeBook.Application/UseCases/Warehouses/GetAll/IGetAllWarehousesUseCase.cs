using RDTrackR.Communication.Responses.Warehouse;

namespace RDTrackR.Application.UseCases.Warehouses.GetAll
{
    public interface IGetAllWarehousesUseCase
    {
        Task<List<ResponseWarehouseJson>> Execute();
    }
}