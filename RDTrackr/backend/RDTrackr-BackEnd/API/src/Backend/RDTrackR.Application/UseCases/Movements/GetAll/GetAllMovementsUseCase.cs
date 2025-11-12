using AutoMapper;
using RDTrackR.Communication.Requests.Movements;
using RDTrackR.Communication.Responses.Movements;
using RDTrackR.Domain.Repositories.Movements;

namespace RDTrackR.Application.UseCases.Movements.GetAll
{
    public class GetAllMovementsUseCase : IGetAllMovementsUseCase
    {
        private readonly IMovementReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllMovementsUseCase(
            IMovementReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseMovementJson>> Execute(RequestGetMovementsJson request)
        {
            var type = request.Type.HasValue
                ? (RDTrackR.Domain.Enums.MovementType?)(request.Type.Value)
                : null;

            var movements = await _repository.GetFilteredAsync(
                request.WarehouseId,
                type,
                request.StartDate,
                request.EndDate
            );

            return _mapper.Map<List<ResponseMovementJson>>(movements);
        }
    }
}
