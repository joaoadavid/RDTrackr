using RDTrackR.Communication.Requests.Product;

namespace RDTrackR.Application.UseCases.Product.Update
{
    public interface IUpdateProductUseCase
    {
        Task Execute(long id, RequestRegisterProductJson request);
    }
}