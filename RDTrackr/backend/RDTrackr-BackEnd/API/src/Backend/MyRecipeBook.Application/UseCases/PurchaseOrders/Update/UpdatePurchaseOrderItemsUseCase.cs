using RDTrackR.Communication.Requests.PurchaseOrders;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Enums;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.PurchaseOrders;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.PurchaseOrders.Update
{

    public class UpdatePurchaseOrderItemsUseCase : IUpdatePurchaseOrderItemsUseCase
    {
        private readonly IPurchaseOrderReadOnlyRepository _readRepository;
        private readonly IPurchaseOrderWriteOnlyRepository _writeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePurchaseOrderItemsUseCase(
            IPurchaseOrderReadOnlyRepository readRepository,
            IPurchaseOrderWriteOnlyRepository writeRepository,
            IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id, RequestUpdatePurchaseOrderItemsJson request)
        {
            var order = await _readRepository.GetByIdAsync(id)
                ?? throw new NotFoundException(ResourceMessagesException.PURCHASE_ORDER_NOT_FOUND);

            if (order.Status != PurchaseOrderStatus.DRAFT)
                throw new ErrorOnValidationException([ResourceMessagesException.PO_CANNOT_EDIT_ITEMS]);

            order.Items.Clear();

            // Recria itens
            foreach (var item in request.Items)
            {
                order.Items.Add(new PurchaseOrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
            }

            await _writeRepository.UpdateAsync(order);
            await _unitOfWork.Commit();
        }
    }

}
