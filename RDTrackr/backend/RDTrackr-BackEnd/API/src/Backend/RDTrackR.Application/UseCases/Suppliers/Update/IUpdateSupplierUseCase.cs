using RDTrackR.Communication.Requests.Supplier;
using RDTrackR.Communication.Responses.Supplier;

namespace RDTrackR.Application.UseCases.Suppliers.Update
{
    public interface IUpdateSupplierUseCase
    {
        Task<ResponseSupplierJson> Execute(long id, RequestUpdateSupplierJson request);
    }
}