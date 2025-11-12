using RDTrackR.Communication.Requests.Movements;
using RDTrackR.Communication.Responses.Movements;

namespace RDTrackR.Application.UseCases.Movements.GetAll
{
    public interface IGetAllMovementsUseCase
    {
        Task<List<ResponseMovementJson>> Execute(RequestGetMovementsJson request);
    }
}