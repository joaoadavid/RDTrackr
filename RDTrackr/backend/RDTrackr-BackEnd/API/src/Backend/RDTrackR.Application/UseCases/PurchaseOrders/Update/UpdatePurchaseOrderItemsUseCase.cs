using RDTrackR.Application.UseCases.Products;
using RDTrackR.Communication.Requests.Product;
using RDTrackR.Communication.Requests.PurchaseOrders;
using RDTrackR.Domain.Entities;
using RDTrackR.Domain.Enums;
using RDTrackR.Domain.Extensions;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.PurchaseOrders;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.PurchaseOrders.Update
{

    public class UpdatePurchaseOrderItemsUseCase : IUpdatePurchaseOrderItemsUseCase
    {
        private readonly IPurchaseOrderReadOnlyRepository _readRepository;
        private readonly IPurchaseOrderWriteOnlyRepository _writeRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePurchaseOrderItemsUseCase(
            IPurchaseOrderReadOnlyRepository readRepository,
            IPurchaseOrderWriteOnlyRepository writeRepository,
            ILoggedUser loggedUser,
            IUnitOfWork unitOfWork)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _loggedUser = loggedUser;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(long id, RequestUpdatePurchaseOrderItemsJson request)
        {
            await Validate(request);
            var loggedUser = await _loggedUser.User();
            var order = await _readRepository.GetByIdAsync(id, loggedUser)
                ?? throw new NotFoundException(ResourceMessagesException.PURCHASE_ORDER_NOT_FOUND);

            if (order.Status != PurchaseOrderStatus.DRAFT)
                throw new ErrorOnValidationException([ResourceMessagesException.PO_CANNOT_EDIT_ITEMS]);

            order.Items.Clear();

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
        private async Task Validate(RequestUpdatePurchaseOrderItemsJson request)
        {
            var validator = new PurchaseOrderUpdateValidator();
            var result = await validator.ValidateAsync(request);

            if (result.IsValid.IsFalse())
            {
                throw new ErrorOnValidationException(
                    result.Errors.Select(e => e.ErrorMessage).Distinct().ToList()
                );
            }
        }
    }

}
