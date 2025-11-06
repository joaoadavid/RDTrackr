using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Suppliers;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.Suppliers.Delete
{
    public class DeleteSupplierUseCase : IDeleteSupplierUseCase
    {
        private readonly ISupplierReadOnlyRepository _readRepository;
        private readonly ISupplierWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierUseCase(
            ISupplierReadOnlyRepository readRepository,
            ISupplierWriteOnlyRepository writeRepository,
            IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var supplier = await _readRepository.GetByIdAsync(id)
                ?? throw new NotFoundException("Supplier not found");

            await _writeRepository.DeleteAsync(supplier);
            await _unitOfWork.Commit();
        }
    }
}
