using CommonTestUtilities.Entities;
using CommonTestUtilities.Tokens;
using Shouldly;
using System.Net;

namespace WebApi.Test.User.Profile
{
    public class GetUserProfileInvalideTokenTest : RDTrackRClassFixture
    {
        private readonly string METHOD = "user";
        public GetUserProfileInvalideTokenTest(CustomWebApplicationFactory factory): base(factory) { }      

        [Fact]
        public async Task Error_Token_Invalid()
        {
            var response = await DoGet(METHOD, token: "tokenInvalid");

            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        }

        [Fact]
        public async Task Error_Withou_Token()
        {
            var response = await DoGet(METHOD, token:string.Empty);

            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);


        }

        [Fact]
        public async Task Error_Token_With_User_NotFound()
        {
            (var user, _) = UserBuilder.Build();
            var token = JwtTokenGeneratorBuilder.Build().Generate(user);

            var response = await DoGet(METHOD, token);

            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);


        }
    }
}
