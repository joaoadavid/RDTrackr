using RDTrackR.Communication.Requests.Product;
using RDTrackR.Communication.Responses.Product;

namespace RDTrackR.Application.UseCases.Products.Register
{
    public interface IRegisterProductUseCase
    {
        Task<ResponseProductJson> Execute(RequestRegisterProductJson request);
    }
}