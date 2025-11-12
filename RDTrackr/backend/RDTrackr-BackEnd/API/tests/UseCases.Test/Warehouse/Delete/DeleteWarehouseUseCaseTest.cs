using CommonTestUtilities.Entities;
using CommonTestUtilities.Entities.Warehouses;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Warehouses;
using CommonTestUtilities.Requests.Warehouse;
using RDTrackR.Application.UseCases.Warehouses.Delete;
using RDTrackR.Application.UseCases.Warehouses.Update;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using Shouldly;

namespace UseCases.Test.Warehouse.Delete
{
    public class DeleteWarehouseUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var warehouse = WarehouseBuilder.Build(user);
            var useCase = CreateUseCase(user, warehouse);

            var act = async () => { await useCase.Execute(warehouse.Id); };

            await act.ShouldNotThrowAsync();
        }

        [Fact]
        public async Task Error_Not_Found()
        {
            (var user, _) = UserBuilder.Build();
            var repo = new WarehouseRepositoryBuilder().NotExists(10,user);
            var warehouse = WarehouseBuilder.Build(user);

            var unitOfWork = UnitOfWorkBuilder.Build();

            var useCase = CreateUseCase(user, warehouse);
            var act = () => useCase.Execute(10);

            var exception = await act.ShouldThrowAsync<NotFoundException>();
            exception.GetErrorMessages().ShouldContain(ResourceMessagesException.WAREHOUSE_NOT_FOUND);
        }

        private DeleteWarehouseUseCase CreateUseCase(RDTrackR.Domain.Entities.User user, RDTrackR.Domain.Entities.Warehouse warehouse)
        {
            var readRepository = new WarehouseRepositoryBuilder().GetById(warehouse, user).BuildRead();
            var writeRepository = new WarehouseRepositoryBuilder().BuildWrite();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteWarehouseUseCase(readRepository, writeRepository, loggedUser, unitOfWork);
        }
    }
}
