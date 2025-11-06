
namespace RDTrackR.Application.UseCases.Suppliers.Delete
{
    public interface IDeleteSupplierUseCase
    {
        Task Execute(long id);
    }
}