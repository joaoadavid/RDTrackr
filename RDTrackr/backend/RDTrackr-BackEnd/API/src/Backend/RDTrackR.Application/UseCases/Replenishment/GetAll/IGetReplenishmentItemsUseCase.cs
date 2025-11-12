using RDTrackR.Communication.Responses.Replenishment;

namespace RDTrackR.Application.UseCases.Replenishment.GetAll
{
    public interface IGetReplenishmentItemsUseCase
    {
        Task<List<ResponseReplenishmentItemJson>> Execute();
    }
}