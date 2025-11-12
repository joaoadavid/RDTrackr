using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Warehouses;
using CommonTestUtilities.Requests.Warehouse;
using RDTrackR.Application.UseCases.Warehouses.Register;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using Shouldly;

namespace UseCases.Test.Warehouse.Register
{
    public class RegisterWarehouseUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestRegisterWarehouseJsonBuilder.Build();
            var useCase = CreateUseCase(user);

            var result = await useCase.Execute(request);

            result.ShouldNotBeNull();
            result.Name.ShouldBe(request.Name);
        }

        [Fact]
        public async Task Error_WAREHOUENAME_REQUIRED()
        {
            (var user, _) = UserBuilder.Build();
            var request = RequestRegisterWarehouseJsonBuilder.Build();
            request.Name = string.Empty;
            var useCase = CreateUseCase(user);

            var act = () => useCase.Execute(request);
            var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
            exception.GetErrorMessages().Count().ShouldBe(1);
            exception.GetErrorMessages().ShouldContain(ResourceMessagesException.WAREHOUSE_NAME_REQUIRED);
        }

        private static RegisterWarehouseUseCase CreateUseCase(RDTrackR.Domain.Entities.User user)
        {
            var request = RequestRegisterWarehouseJsonBuilder.Build();
            var repo = new WarehouseRepositoryBuilder().BuildWrite();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);
            var mapper = MapperBuilder.Build();

            return new RegisterWarehouseUseCase(repo,unitOfWork,loggedUser,mapper);
        }
    }
}
