using CommonTestUtilities.Entities;
using CommonTestUtilities.Entities.Suppliers;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Suppliers;
using RDTrackR.Application.UseCases.Suppliers.Delete;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using Shouldly;

namespace UseCases.Test.Supplier.Delete
{
    public class DeleteSupplierUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();
            var supplier = SupplierBuilder.Build(user.Id);

            var useCase = CreateUseCase(user, supplier);

            Func<Task> act = async () => await useCase.Execute(supplier.Id);

            await act.ShouldNotThrowAsync();
        }

        [Fact]
        public async Task Error_Supplier_NotFound()
        {
            (var user, _) = UserBuilder.Build();

            var useCase = CreateUseCase(user, supplier: null);

            Func<Task> act = async () => await useCase.Execute(9999);

            var exception = await act.ShouldThrowAsync<NotFoundException>();
            exception.GetErrorMessages().ShouldContain(ResourceMessagesException.SUPPLIER_NOT_FOUND);
        }

        private static DeleteSupplierUseCase CreateUseCase(
            RDTrackR.Domain.Entities.User user,
            RDTrackR.Domain.Entities.Supplier? supplier = null)
        {
            var loggedUser = LoggedUserBuilder.Build(user);
            var repo = new SupplierRepositoryBuilder()
                .GetById(user, supplier)
                .Delete();

            var unitOfWork = UnitOfWorkBuilder.Build();

            return new DeleteSupplierUseCase(repo.BuildRead(), repo.BuildWrite(), loggedUser, unitOfWork);
        }
    }

}
