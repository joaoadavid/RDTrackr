using RDTrackR.Communication.Responses.Product;

namespace RDTrackR.Application.UseCases.Products.GetById
{
    public interface IGetProductByIdUseCase
    {
        Task<ResponseProductJson> Execute(long id);
    }
}