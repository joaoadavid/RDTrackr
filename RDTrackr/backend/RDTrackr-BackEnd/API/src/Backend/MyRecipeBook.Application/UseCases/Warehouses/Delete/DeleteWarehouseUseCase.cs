using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.Warehouses;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.Warehouses.Delete
{
    public class DeleteWarehouseUseCase : IDeleteWarehouseUseCase
    {
        private readonly IWarehouseReadOnlyRepository _readOnlyRepository;
        private readonly IWarehouseWriteOnlyRepository _writeOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWarehouseUseCase(
            IWarehouseReadOnlyRepository readOnlyRepository,
            IWarehouseWriteOnlyRepository writeOnlyRepository,
            IUnitOfWork unitOfWork)
        {
            _readOnlyRepository = readOnlyRepository;
            _writeOnlyRepository = writeOnlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var warehouse = await _readOnlyRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(ResourceMessagesException.WAREHOUSE_NOT_FOUND);

            await _writeOnlyRepository.DeleteAsync(warehouse);
            await _unitOfWork.Commit();
        }
    }
}
