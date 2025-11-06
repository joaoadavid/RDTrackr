using AutoMapper;
using RDTrackR.Domain.Repositories;
using RDTrackR.Communication.Responses.Product;
using RDTrackR.Domain.Repositories.Products;

namespace RDTrackR.Application.UseCases.Product.GetAll
{
    public class GetAllProductsUseCase : IGetAllProductsUseCase
    {
        private readonly IProductReadOnlyRepository _readRepository;
        private readonly IMapper _mapper;

        public GetAllProductsUseCase(
            IProductReadOnlyRepository readRepository,
            IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<List<ResponseProductJson>> Execute()
        {
            var products = await _readRepository.GetAllAsync();
            return _mapper.Map<List<ResponseProductJson>>(products);
        }
    }
}
