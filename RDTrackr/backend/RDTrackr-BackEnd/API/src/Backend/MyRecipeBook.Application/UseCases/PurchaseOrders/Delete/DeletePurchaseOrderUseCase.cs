using RDTrackR.Domain.Repositories.PurchaseOrders;
using RDTrackR.Domain.Repositories;
using RDTrackR.Exceptions.ExceptionBase;
using RDTrackR.Exceptions;

namespace RDTrackR.Application.UseCases.PurchaseOrders.Delete
{
    public class DeletePurchaseOrderUseCase : IDeletePurchaseOrderUseCase
    {
        private readonly IPurchaseOrderReadOnlyRepository _readRepository;
        private readonly IPurchaseOrderWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePurchaseOrderUseCase(
            IPurchaseOrderReadOnlyRepository readRepository,
            IPurchaseOrderWriteOnlyRepository writeRepository,
            IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id)
        {
            var order = await _readRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(ResourceMessagesException.PURCHASE_ORDER_NOT_FOUND);

            await _writeRepository.DeleteAsync(order);
            await _unitOfWork.Commit();
        }
    }

}
