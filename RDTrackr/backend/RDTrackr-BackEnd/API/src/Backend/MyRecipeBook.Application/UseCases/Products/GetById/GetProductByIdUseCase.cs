using AutoMapper;
using RDTrackR.Communication.Responses.Product;
using RDTrackR.Domain.Repositories.Products;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.Products.GetById
{
    public class GetProductByIdUseCase : IGetProductByIdUseCase
    {
        private readonly IProductReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdUseCase(IProductReadOnlyRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseProductJson> Execute(long id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product is null)
                throw new NotFoundException(ResourceMessagesException.PRODUCT_NOT_FOUND);

            return _mapper.Map<ResponseProductJson>(product);
        }
    }
}
