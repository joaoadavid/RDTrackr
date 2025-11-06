using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Products;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.Product.Delete
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductReadOnlyRepository _readRepository;
        private readonly IProductWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteProductUseCase(
            IProductReadOnlyRepository readRepository,
            IProductWriteOnlyRepository writeRepository,
            IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var product = await _readRepository.GetByIdAsync(id);

            if (product == null)
                throw new NotFoundException("Produto não encontrado");

            await _writeRepository.DeleteAsync(product);
            await _unitOfWork.Commit();
        }
    }
}
