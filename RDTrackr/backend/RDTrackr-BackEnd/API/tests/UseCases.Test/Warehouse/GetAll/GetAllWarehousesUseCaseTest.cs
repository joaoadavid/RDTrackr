using CommonTestUtilities.Entities;
using CommonTestUtilities.Entities.Warehouses;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Warehouses;
using RDTrackR.Application.UseCases.Warehouses.GetAll;
using Shouldly;

namespace UseCases.Test.Warehouse.GetAll
{
    public class GetAllWarehousesUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            (var user , _)=UserBuilder.Build();
            // Arrange
            var warehouse1 = WarehouseBuilder.Build(user);
            var warehouse2 = WarehouseBuilder.Build(user);

            var warehouses = new List<RDTrackR.Domain.Entities.Warehouse>
            {
                warehouse1,
                warehouse2
            };

            var repo = new WarehouseRepositoryBuilder()
                .WithList(warehouses); 

            var mapper = MapperBuilder.Build();

            var useCase = new GetAllWarehousesUseCase(repo.BuildRead(), mapper);

            // Act
            var result = await useCase.Execute();

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
            result[0].Name.ShouldBe(warehouse1.Name);
            result[1].Name.ShouldBe(warehouse2.Name);
        }
    }
}
