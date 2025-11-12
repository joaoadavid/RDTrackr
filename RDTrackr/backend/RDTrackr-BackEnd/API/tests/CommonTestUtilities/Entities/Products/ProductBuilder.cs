using Bogus;
using RDTrackR.Domain.Entities;

namespace CommonTestUtilities.Entities.Products
{
    public static class ProductBuilder
    {
        public static Product Build(long? id = null, User? createdBy = null)
        {
            var faker = new Faker("pt_BR");

            createdBy ??= UserBuilder.Build().Item1; // reaproveita UserBuilder

            return new Product
            {
                Id = id ?? faker.Random.Long(1, 9999),
                Sku = faker.Commerce.Ean13(),
                Name = faker.Commerce.ProductName(),
                Category = faker.Commerce.Categories(1)[0],
                UoM = "UN",
                Price = faker.Random.Decimal(5, 500),

                Stock = faker.Random.Int(0, 300),
                ReorderPoint = faker.Random.Int(5, 50),

                DailyConsumption = faker.Random.Decimal(1, 20),
                LeadTimeDays = faker.Random.Int(1, 15),
                SafetyStock = faker.Random.Decimal(5, 60),
                LastPurchasePrice = faker.Random.Decimal(3, 450),

                UpdatedAt = DateTime.UtcNow,

                CreatedByUserId = createdBy.Id,
                CreatedBy = createdBy,

                Active = true,
                CreatedOn = DateTime.UtcNow
            };
        }
    }
}
