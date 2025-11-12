using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories.PurchaseOrders;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests.PurchaseOrder;
using RDTrackR.Application.UseCases.PurchaseOrders.Register;
using RDTrackR.Exceptions.ExceptionBase;
using RDTrackR.Exceptions;
using Shouldly;
using RDTrackR.Application.UseCases.PurchaseOrders.GetAll;
using CommonTestUtilities.PurchaseOrders;

namespace UseCases.Test.PurchaseOrder.Get
{
    public class GetPurchaseOrderUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();

            var order = PurchaseOrderBuilder.Build(createdByUserId: user.Id);

            var useCase = CreateUseCase(user);

            var result = await useCase.Execute();

            foreach (var po in result)
            {
                po.Id.ShouldBe(order.Id);
                po.Status.ShouldBe(order.Status.ToString());
            }
        }

        private static GetPurchaseOrdersUseCase CreateUseCase(
        RDTrackR.Domain.Entities.User user)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var repositoryBuilder = new PurchaseOrderRepositoryBuilder().BuildRead();
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new GetPurchaseOrdersUseCase(repositoryBuilder,loggedUser, mapper);
        }
    }
}
