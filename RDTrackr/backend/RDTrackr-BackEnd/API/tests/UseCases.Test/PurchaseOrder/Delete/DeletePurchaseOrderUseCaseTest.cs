using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.PurchaseOrders;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.PurchaseOrders;
using RDTrackR.Application.UseCases.PurchaseOrders.Delete;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using Shouldly;

namespace UseCases.Test.PurchaseOrder.Delete
{
    public class DeletePurchaseOrderUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();

            var purchaseOrder = PurchaseOrderBuilder.Build();
            purchaseOrder.CreatedByUserId = user.Id;

            var useCase = CreateUseCase(user, purchaseOrder);

            var act = async () => { await useCase.Execute(purchaseOrder.Id); };

            await act.ShouldNotThrowAsync();
        }


        [Fact]
        public async Task Error_Recipe_NotFound()
        {
            (var user, _) = UserBuilder.Build();
            var purchaseOrder = PurchaseOrderBuilder.Build();
            var useCase = CreateUseCase(user, purchaseOrder);

            Func<Task> act = async () => { await useCase.Execute(id: 1000); };

            var exception = await act.ShouldThrowAsync<NotFoundException>();
            exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PURCHASE_ORDER_NOT_FOUND);
        }

        private static DeletePurchaseOrderUseCase CreateUseCase(
        RDTrackR.Domain.Entities.User user,
        RDTrackR.Domain.Entities.PurchaseOrder order)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repositoryReadBuilder = new PurchaseOrderRepositoryBuilder().GetById(order,user).BuildRead();
            var repositoryWriteBuilder = new PurchaseOrderRepositoryBuilder().BuildWrite();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeletePurchaseOrderUseCase(repositoryReadBuilder, repositoryWriteBuilder, loggedUser, unitOfWork);
        }
    }
}
