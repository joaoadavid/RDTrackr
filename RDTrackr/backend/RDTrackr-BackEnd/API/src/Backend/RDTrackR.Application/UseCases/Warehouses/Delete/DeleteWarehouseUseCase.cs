using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Warehouses;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.Warehouses.Delete
{
    public class DeleteWarehouseUseCase : IDeleteWarehouseUseCase
    {
        private readonly IWarehouseReadOnlyRepository _readOnlyRepository;
        private readonly IWarehouseWriteOnlyRepository _writeOnlyRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWarehouseUseCase(
            IWarehouseReadOnlyRepository readOnlyRepository,
            IWarehouseWriteOnlyRepository writeOnlyRepository,
            ILoggedUser loggedUser,
            IUnitOfWork unitOfWork)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.User();

            var warehouse = await _readOnlyRepository.GetByIdAsync(id,loggedUser)
                ?? throw new NotFoundException(ResourceMessagesException.WAREHOUSE_NOT_FOUND);

            await _writeOnlyRepository.DeleteAsync(warehouse);
            await _unitOfWork.Commit();
        }
    }
}
