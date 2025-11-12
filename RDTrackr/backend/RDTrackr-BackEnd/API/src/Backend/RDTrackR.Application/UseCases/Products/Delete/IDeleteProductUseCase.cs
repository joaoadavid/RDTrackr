
namespace RDTrackR.Application.UseCases.Product.Delete
{
    public interface IDeleteProductUseCase
    {
        Task Execute(long id);
    }
}