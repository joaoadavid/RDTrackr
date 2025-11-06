using Bogus;
using RDTrackR.Communication.Requests.Login;

namespace CommonTestUtilities.Requests
{
    public static class RequestLoginJsonBuilder
    {
        public static RequestLoginJson Build(int passwordLenght = 10)
        {
            return new Faker<RequestLoginJson>()
                .RuleFor(user => user.Email, (f, u) => f.Internet.Email())//U é uma instancia de RequestRegisterUserJson
                .RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLenght));
        }
    }
}
