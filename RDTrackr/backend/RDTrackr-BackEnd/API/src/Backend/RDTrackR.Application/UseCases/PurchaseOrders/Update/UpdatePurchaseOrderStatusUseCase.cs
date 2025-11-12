using RDTrackR.Communication.Requests.PurchaseOrders;
using RDTrackR.Domain.Enums;
using RDTrackR.Domain.Repositories;
using RDTrackR.Domain.Repositories.PurchaseOrders;
using RDTrackR.Domain.Services.LoggedUser;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;

namespace RDTrackR.Application.UseCases.PurchaseOrders.Update
{
    public class UpdatePurchaseOrderStatusUseCase : IUpdatePurchaseOrderStatusUseCase
    {
        private readonly IPurchaseOrderReadOnlyRepository _readRepository;
        private readonly IPurchaseOrderWriteOnlyRepository _writeRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePurchaseOrderStatusUseCase(
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

        public async Task Execute(long id, RequestUpdatePurchaseOrderStatusJson request)
        {
            var loggedUser = await _loggedUser.User();
            var order = await _readRepository.GetByIdAsync(id,loggedUser)
                ?? throw new NotFoundException(ResourceMessagesException.PURCHASE_ORDER_NOT_FOUND);

            if (!Enum.TryParse<PurchaseOrderStatus>(request.Status, true, out var status))
                throw new ErrorOnValidationException(new List<string> { "Invalid status value." });

            order.Status = status;

            await _writeRepository.UpdateAsync(order);
            await _unitOfWork.Commit();
        }
    }
}