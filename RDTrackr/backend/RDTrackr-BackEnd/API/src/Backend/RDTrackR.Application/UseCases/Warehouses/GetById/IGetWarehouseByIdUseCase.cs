using RDTrackR.Communication.Responses.Warehouse;

namespace RDTrackR.Application.UseCases.Warehouses.GetById
{
    public interface IGetWarehouseByIdUseCase
    {
        Task<ResponseWarehouseJson> Execute(long id);
    }
}