using CommonTestUtilities.Entities;
using CommonTestUtilities.Entities.Warehouses;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Warehouses;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Requests.Warehouse;
using RDTrackR.Application.UseCases.Warehouses.Update;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using Shouldly;

namespace UseCases.Test.Warehouse.Update
{
    public class UpdateWarehouseUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var warehouse = WarehouseBuilder.Build(user);       
            var request = RequestUpdateWarehouseJsonBuilder.Build();
            var useCase = CreateUseCase(user, warehouse);
            
            var result = await useCase.Execute(warehouse.Id, request);

            result.Name.ShouldBe(request.Name);
        }     

        [Fact]
        public async Task Error_Name_Empty()
        {
            (var user, _) = UserBuilder.Build();
            var warehouse = WarehouseBuilder.Build(user);
            var request = RequestUpdateWarehouseJsonBuilder.Build();
            request.Name = string.Empty;

            var useCase = CreateUseCase(user, warehouse);

            Func<Task> act = async () => await useCase.Execute(warehouse.Id, request);

            var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
            exception.GetErrorMessages().Count().ShouldBe(1);
            exception.GetErrorMessages().ShouldContain(ResourceMessagesException.WAREHOUSE_NAME_REQUIRED);

            user.Name.ShouldNotBe(request.Name);
        }

        private UpdateWarehouseUseCase CreateUseCase(RDTrackR.Domain.Entities.User user, RDTrackR.Domain.Entities.Warehouse warehouse)
        {
            var readRepository = new WarehouseRepositoryBuilder().GetById(warehouse,user).BuildRead();
            var writeRepository = new WarehouseRepositoryBuilder().BuildWrite();
            var loggedUser = LoggedUserBuilder.Build(user);
            var unitOfWork = UnitOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();

            return new UpdateWarehouseUseCase(readRepository, writeRepository, loggedUser, unitOfWork, mapper);
        }

    }
}
