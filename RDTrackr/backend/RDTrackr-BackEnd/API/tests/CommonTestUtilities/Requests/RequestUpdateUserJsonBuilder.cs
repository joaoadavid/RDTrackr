using Bogus;
using RDTrackR.Communication.Requests.User;

namespace CommonTestUtilities.Requests
{
    public static class RequestUpdateUserJsonBuilder
    {
        public static RequestUpdateUserJson Build()
        {
            return new Faker<RequestUpdateUserJson>()
                .RuleFor(request => request.Name, f => f.Person.FirstName)
                .RuleFor(request => request.Email, (f,user) => f.Internet.Email(user.Name));                
        }
    }
}
