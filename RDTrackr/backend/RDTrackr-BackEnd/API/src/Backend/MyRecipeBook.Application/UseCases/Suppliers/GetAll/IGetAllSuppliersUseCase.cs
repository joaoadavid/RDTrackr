using RDTrackR.Communication.Responses.Supplier;

namespace RDTrackR.Application.UseCases.Suppliers.GetAll
{
    public interface IGetAllSuppliersUseCase
    {
        Task<List<ResponseSupplierJson>> Execute();
    }
}