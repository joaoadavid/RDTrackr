using CommonTestUtilities.Entities.Suppliers;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories.Suppliers;
using RDTrackR.Application.UseCases.Suppliers.GetAll;
using Shouldly;

namespace UseCases.Test.Supplier.GetAll
{
    public class GetAllSupplierUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            // Arrange
            var supplier1 = SupplierBuilder.Build();
            var supplier2 = SupplierBuilder.Build();

            var suppliers = new List<RDTrackR.Domain.Entities.Supplier>
            {
                supplier1,
                supplier2
            };

            var repository = new SupplierRepositoryBuilder().WithList(suppliers);
            var mapper = MapperBuilder.Build();

            var useCase = new GetAllSuppliersUseCase(repository.BuildRead(), mapper);

            // Act
            var result = await useCase.Execute();

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);

            result[0].Name.ShouldBe(supplier1.Name);
            result[1].Name.ShouldBe(supplier2.Name);
        }
    }
}
