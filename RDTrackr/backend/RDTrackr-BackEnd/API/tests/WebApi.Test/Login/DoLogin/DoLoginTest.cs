using CommonTestUtilities.Requests;
using RDTrackR.Communication.Requests.Login;
using RDTrackR.Exceptions;
using Shouldly;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin
{
    public class DoLoginTest : MyRecipeBookClassFixture
    {
        private readonly string method = "login";

        private readonly string _email;
        private readonly string _password;
        private readonly string _name;
        public DoLoginTest(CustomWebApplicationFactory factory):base(factory)
        {
            _email = factory.GetEmail();
            _password = factory.GetPassword();
            _name = factory.GetName();
        }

        [Fact]
        public async Task Success()
        {
            var request = new RequestLoginJson
            {
                Email = _email,
                Password = _password
            };

            var response = await DoPost(method: method,request: request);

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var email = responseData.RootElement.GetProperty("name").GetString();
            email.ShouldNotBeNullOrWhiteSpace();
            email.ShouldBe(_name);

            var tokens = responseData.RootElement.GetProperty("tokens").GetProperty("accessToken").GetString();
            tokens.ShouldNotBeNullOrEmpty();
        }


        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Login_Invalid(string culture)
        {
            var request = RequestRegisterUserJsonBuilder.Build();
            
            var response = await DoPost(method: method, request: request, culture: culture);

            response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

            await using var responseBody = await response.Content.ReadAsStreamAsync();

            var responseData = await JsonDocument.ParseAsync(responseBody);

            var errors = responseData.RootElement.GetProperty("errors").EnumerateArray();

            var expectedMessage = ResourceMessagesException.ResourceManager.GetString
                ("EMAIL_OR_PASSWORD_INVALID",
                new System.Globalization.CultureInfo(culture));

            errors.ShouldHaveSingleItem();
            errors.ShouldContain(error => error.GetString()!.Equals(expectedMessage));
        }

    }
}
