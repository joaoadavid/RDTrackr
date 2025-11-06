using RDTrackR.Communication.Requests.PurchaseOrders;
using RDTrackR.Domain.Enums;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.PurchaseOrders;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.PurchaseOrders.Update
{
    public class UpdatePurchaseOrderStatusUseCase : IUpdatePurchaseOrderStatusUseCase
    {
        private readonly IPurchaseOrderReadOnlyRepository _readRepository;
        private readonly IPurchaseOrderWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePurchaseOrderStatusUseCase(
            IPurchaseOrderReadOnlyRepository readRepository,
            IPurchaseOrderWriteOnlyRepository writeRepository,
            IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id, RequestUpdatePurchaseOrderStatusJson request)
        {
            var order = await _readRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(ResourceMessagesException.PURCHASE_ORDER_NOT_FOUND);

            if (!Enum.TryParse<PurchaseOrderStatus>(request.Status, true, out var status))
                throw new ErrorOnValidationException(new List<string> { "Invalid status value." });

            order.Status = status;

            await _writeRepository.UpdateAsync(order);
            await _unitOfWork.Commit();
        }
    }
}