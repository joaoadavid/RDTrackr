using Bogus;
using RDTrackR.Communication.Requests.Password;

namespace CommonTestUtilities.ChangePassword
{
    public static class RequestChangePasswordJsonBuilder
    {
        public static RequestChangePasswordJson Build(int passwordLenght = 10)
        {
            return new Faker<RequestChangePasswordJson>()
                .RuleFor(user => user.Password, (f, u) => f.Internet.Password())//U é uma instancia de RequestRegisterUserJson
                .RuleFor(user => user.NewPassword, (f) => f.Internet.Password(passwordLenght));
        }
    }
}
