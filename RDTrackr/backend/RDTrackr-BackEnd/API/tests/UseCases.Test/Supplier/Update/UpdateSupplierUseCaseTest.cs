using CommonTestUtilities.Entities;
using CommonTestUtilities.Entities.Suppliers;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Suppliers;
using CommonTestUtilities.Requests.Supplier;
using RDTrackR.Application.UseCases.Suppliers.Update;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using Shouldly;

namespace UseCases.Test.Supplier.Update
{
    public class UpdateSupplierUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var supplier = SupplierBuilder.Build(user.Id);
            var request = RequestUpdateSupplierJsonBuilder.Build();

            var repo = new SupplierRepositoryBuilder()
                .GetById(user, supplier)
                .Update();

            var useCase = CreateUseCase(repo, user);

            var result = await useCase.Execute(supplier.Id, request);

            result.Name.ShouldBe(request.Name);
            result.Email.ShouldBe(request.Email);
        }

       
        [Fact]
        public async Task Error_Email_Already_Registered()
        {
            (var user, _) = UserBuilder.Build();
            var supplier = SupplierBuilder.Build(user.Id);
            var request = RequestUpdateSupplierJsonBuilder.Build();

            var repo = new SupplierRepositoryBuilder()
                .GetById(user, supplier)
                .ExistsSupplierWithEmail(request.Email!, supplier.Id);

            var useCase = CreateUseCase(repo, user);

            Func<Task> act = async () => await useCase.Execute(supplier.Id, request);

            var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
            exception.GetErrorMessages().ShouldContain(ResourceMessagesException.SUPPLIER_EMAIL_DUPLICATE);
        }



        private static UpdateSupplierUseCase CreateUseCase(SupplierRepositoryBuilder repositoryBuilder, RDTrackR.Domain.Entities.User user)
        {
            var readRepository = repositoryBuilder.BuildRead();
            var writeRepository = repositoryBuilder.BuildWrite();
            var unitOfWork = UnitOfWorkBuilder.Build();
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new UpdateSupplierUseCase(readRepository, writeRepository, loggedUser, unitOfWork, mapper);
        }
    }
}
