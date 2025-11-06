using AutoMapper;
using RDTrackR.Domain.Repositories;
using RDTrackR.Communication.Requests.Product;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using RDTrackR.Domain.Repositories.Products;

namespace RDTrackR.Application.UseCases.Product.Update
{
    public class UpdateProductUseCase : IUpdateProductUseCase
    {
        private readonly IProductReadOnlyRepository _readRepository;
        private readonly IProductWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateProductUseCase(
            IProductReadOnlyRepository readRepository,
            IProductWriteOnlyRepository writeRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Execute(long id, RequestRegisterProductJson request)
        {
            var product = await _readRepository.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException("Produto não encontrado");

            _mapper.Map(request, product);
            product.UpdatedAt = DateTime.UtcNow;

            await _writeRepository.UpdateAsync(product);
            await _unitOfWork.Commit();
        }
    }
}
