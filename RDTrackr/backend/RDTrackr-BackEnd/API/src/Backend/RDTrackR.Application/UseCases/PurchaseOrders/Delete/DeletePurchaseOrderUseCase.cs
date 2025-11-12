using RDTrackR.Domain.Repositories.PurchaseOrders;
using RDTrackR.Domain.Repositories;
using RDTrackR.Exceptions.ExceptionBase;
using RDTrackR.Exceptions;
using RDTrackR.Domain.Services.LoggedUser;

namespace RDTrackR.Application.UseCases.PurchaseOrders.Delete
{
    public class DeletePurchaseOrderUseCase : IDeletePurchaseOrderUseCase
    {
        private readonly IPurchaseOrderReadOnlyRepository _readRepository;
        private readonly IPurchaseOrderWriteOnlyRepository _writeRepository;
        private readonly ILoggedUser _loggedUser;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePurchaseOrderUseCase(
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

        public async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.User();
            var order = await _readRepository.GetByIdAsync(id, loggedUser)
                ?? throw new NotFoundException(ResourceMessagesException.PURCHASE_ORDER_NOT_FOUND);

            await _writeRepository.DeleteAsync(order);
            await _unitOfWork.Commit();
        }
    }

}
