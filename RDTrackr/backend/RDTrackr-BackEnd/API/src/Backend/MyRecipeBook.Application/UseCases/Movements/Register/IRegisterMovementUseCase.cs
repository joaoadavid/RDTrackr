using RDTrackR.Communication.Requests.Movements;
using RDTrackR.Communication.Responses.Movements;

namespace RDTrackR.Application.UseCases.Movements.Register
{
    public interface IRegisterMovementUseCase
    {
        Task<ResponseMovementJson> Execute(RequestRegisterMovementJson request);
    }
}