using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Suppliers;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.Suppliers.Delete
{
    public class DeleteSupplierUseCase : IDeleteSupplierUseCase
    {
        private readonly ISupplierReadOnlyRepository _readRepository;
        private readonly ISupplierWriteOnlyRepository _writeRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSupplierUseCase(
            ISupplierReadOnlyRepository readRepository,
            ISupplierWriteOnlyRepository writeRepository,
            ILoggedUser loggedUser,
            IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.User();

            var supplier = await _readRepository.GetByIdAsync(id,loggedUser)
                ?? throw new NotFoundException(ResourceMessagesException.SUPPLIER_NOT_FOUND);

            await _writeRepository.DeleteAsync(supplier);
            await _unitOfWork.Commit();
        }
    }
}
