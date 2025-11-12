using Bogus;
using RDTrackR.Domain.Entities;

namespace CommonTestUtilities.Entities.Suppliers
{
    public static class SupplierBuilder
    {
        public static Supplier Build(long? id = null, long? createdByUserId = null)
        {
            var faker = new Faker("pt_BR");
            (var user, _) = UserBuilder.Build();

            return new Supplier
            {
                Id = id ?? faker.Random.Long(1, 9999),
                Name = faker.Company.CompanyName(),
                Contact = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Phone = faker.Phone.PhoneNumber(),
                Address = $"{faker.Address.StreetAddress()}, {faker.Address.City()} - {faker.Address.StateAbbr()}",

                CreatedByUserId = createdByUserId ?? faker.Random.Long(1, 500),
                CreatedBy = user
            };
        }      
    }
}
