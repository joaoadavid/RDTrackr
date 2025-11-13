using CommonTestUtilities.Entities;
using CommonTestUtilities.Tokens;
using RDTrackR.Communication.Requests.Password;
using Shouldly;

namespace WebApi.Test.User.ChangePassword
{
    public class ChangePasswordInvalidTokenTest :RDTrackRClassFixture
    {
        private readonly string METHOD = "user/change-password";
        public ChangePasswordInvalidTokenTest(CustomWebApplicationFactory webApplication): base (webApplication)
        {
        }

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var request = new RequestChangePasswordJson();

            var response = await DoPut(METHOD, request, token:"tokenInvalid");

            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_Without_Token()
        {
            var request = new RequestChangePasswordJson();
            var response = await DoPut(METHOD, request, token: string.Empty);
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task Error_With_User_NotFound()
        {
            (var user, _) = UserBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(user);
            var request = new RequestChangePasswordJson();
            var response = await DoPut(METHOD, request, token);
            response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Unauthorized);
        }

    }
}
