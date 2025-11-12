using AutoMapper;
using RDTrackR.Communication.Responses.StockItem;
using RDTrackR.Domain.Repositories.StockItems;

namespace RDTrackR.Application.UseCases.StockItems.GetAll
{
    public class GetAllStockItemsUseCase : IGetAllStockItemsUseCase
    {
        private readonly IStockItemReadOnlyRepository _readRepository;
        private readonly IMapper _mapper;

        public GetAllStockItemsUseCase(
            IStockItemReadOnlyRepository readRepository,
            IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<List<ResponseStockItemJson>> Execute()
        {
            var stockItems = await _readRepository.GetAllAsync();
            return _mapper.Map<List<ResponseStockItemJson>>(stockItems);
        }
    }
}
