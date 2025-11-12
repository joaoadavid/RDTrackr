using RDTrackR.Communication.Responses.Product;

namespace RDTrackR.Application.UseCases.Product.GetAll
{
    public interface IGetAllProductsUseCase
    {
        Task<List<ResponseProductJson>> Execute();
    }
}