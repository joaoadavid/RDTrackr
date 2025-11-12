using Bogus;
using RDTrackR.Communication.Requests.Supplier;

namespace CommonTestUtilities.Requests.Supplier
{
    public static class RequestRegisterSupplierJsonBuilder
    {
        public static RequestRegisterSupplierJson Build()
        {
            return new Faker<RequestRegisterSupplierJson>()
                .RuleFor(s => s.Name, f => f.Company.CompanyName())
                .RuleFor(s => s.Email, (f, s) => f.Internet.Email(s.Name))
                .RuleFor(s => s.Phone, f => f.Phone.PhoneNumber("+55 ## 9####-####"))
                .RuleFor(s => s.Contact, f => f.Person.FullName)
                .RuleFor(s => s.Address, f => $"{f.Address.StreetAddress()}, {f.Address.City()} - {f.Address.StateAbbr()}");
        }
    }
}
