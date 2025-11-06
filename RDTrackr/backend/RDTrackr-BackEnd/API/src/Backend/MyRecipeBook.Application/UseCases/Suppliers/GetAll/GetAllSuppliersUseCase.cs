using AutoMapper;
using RDTrackR.Communication.Responses.Supplier;
using RDTrackR.Domain.Repositories.Suppliers;

namespace RDTrackR.Application.UseCases.Suppliers.GetAll
{
    public class GetAllSuppliersUseCase : IGetAllSuppliersUseCase
    {
        private readonly ISupplierReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllSuppliersUseCase(
            ISupplierReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponseSupplierJson>> Execute()
        {
            var suppliers = await _repository.GetAllAsync();
            return _mapper.Map<List<ResponseSupplierJson>>(suppliers);
        }
    }
}
