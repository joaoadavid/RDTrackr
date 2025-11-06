using AutoMapper;
using RDTrackR.Communication.Responses.PurchaseOrders;
using RDTrackR.Domain.Repositories.PurchaseOrders;

namespace RDTrackR.Application.UseCases.PurchaseOrders.GetAll
{
    public class GetPurchaseOrdersUseCase : IGetPurchaseOrdersUseCase
    {
        private readonly IPurchaseOrderReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetPurchaseOrdersUseCase(
            IPurchaseOrderReadOnlyRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ResponsePurchaseOrderJson>> Execute()
        {
            var orders = await _repository.GetAllAsync();
            return _mapper.Map<List<ResponsePurchaseOrderJson>>(orders);
        }
    }
}
