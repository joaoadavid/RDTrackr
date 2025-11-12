using CommonTestUtilities.BlobStorage;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Entities.Products;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories.Products;
using RDTrackR.Application.UseCases.Products.GetById;
using RDTrackR.Exceptions;
using RDTrackR.Exceptions.ExceptionBase;
using Shouldly;

namespace UseCases.Test.Product.Get
{
    public class GetProductUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user, _) = UserBuilder.Build();

            var product = ProductBuilder.Build();
            product.CreatedByUserId = user.Id;

            var useCase = CreateUseCase(user, product);

            var result = await useCase.Execute(product.Id);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(product.Id);
            result.Name.ShouldBe(product.Name);
        }


        [Fact]
        public async Task Error_Product_NotFound()
        {
            (var user, _) = UserBuilder.Build();

            var useCase = CreateUseCase(user);

            Func<Task> act = async () => { await useCase.Execute(1000); };

            var exception = await act.ShouldThrowAsync<NotFoundException>();
            exception.GetErrorMessages().ShouldContain(ResourceMessagesException.PRODUCT_NOT_FOUND);
        }

        private static GetProductByIdUseCase CreateUseCase(
        RDTrackR.Domain.Entities.User user,
        RDTrackR.Domain.Entities.Product? product = null)
        {
            var mapper = MapperBuilder.Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            var repositoryBuilder = new ProductRepositoryBuilder();

            if (product is not null)
                repositoryBuilder.GetById(product, user);

            var repository = repositoryBuilder.BuildRead();

            return new GetProductByIdUseCase(repository, loggedUser, mapper);
        }
    }
}
