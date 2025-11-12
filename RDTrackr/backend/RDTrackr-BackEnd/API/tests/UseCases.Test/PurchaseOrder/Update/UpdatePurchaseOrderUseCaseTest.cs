using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.PurchaseOrders;
using CommonTestUtilities.Repositories.PurchaseOrders;
using CommonTestUtilities.Repositories;
using RDTrackR.Application.UseCases.PurchaseOrders.Delete;
using RDTrackR.Exceptions.ExceptionBase;
using RDTrackR.Exceptions;
using Shouldly;
using RDTrackR.Application.UseCases.PurchaseOrders.Update;
using CommonTestUtilities.Requests.PurchaseOrder;

namespace UseCases.Test.PurchaseOrder.Update
{
    public class UpdatePurchaseOrderUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestUpdatePurchaseOrderItemsJsonBuilder.Build();
            var purchaseOrder = PurchaseOrderBuilder.Build();
            purchaseOrder.CreatedByUserId = user.Id;

            var useCase = CreateUseCase(user, purchaseOrder);

            var act = async () => { await useCase.Execute(purchaseOrder.Id,request); };

            await act.ShouldNotThrowAsync();
        }


        [Fact]
        public async Task Error_PO_Items_Required()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestUpdatePurchaseOrderItemsJsonBuilder.Build();
            var purchaseOrder = PurchaseOrderBuilder.Build();
            request.Items = [];
            var useCase = CreateUseCase(user, purchaseOrder);

            Func<Task> act = async () => { await useCase.Execute(purchaseOrder.Id,request); };

            var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
            exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PO_ITEMS_REQUIRED);
        }

        private static UpdatePurchaseOrderItemsUseCase CreateUseCase(
        RDTrackR.Domain.Entities.User user,
        RDTrackR.Domain.Entities.PurchaseOrder order)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repositoryReadBuilder = new PurchaseOrderRepositoryBuilder().GetById(order, user).BuildRead();
            var repositoryWriteBuilder = new PurchaseOrderRepositoryBuilder().BuildWrite();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new UpdatePurchaseOrderItemsUseCase(repositoryReadBuilder, repositoryWriteBuilder, loggedUser, unitOfWork);
        }
    }
}
