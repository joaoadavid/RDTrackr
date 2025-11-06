using AutoMapper;
using RDTrackR.Communication.Responses.Warehouse;
using RDTrackR.Domain.Repositories.Warehouses;

namespace RDTrackR.Application.UseCases.Warehouses.GetAll
{
    public class GetAllWarehousesUseCase : IGetAllWarehousesUseCase
    {
        private readonly IWarehouseReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllWarehousesUseCase(
            IWarehouseReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseWarehouseJson>> Execute()
        {
            var warehouses = await _repository.GetAllAsync();

            return _mapper.Map<List<ResponseWarehouseJson>>(warehouses);
        }
    }
}